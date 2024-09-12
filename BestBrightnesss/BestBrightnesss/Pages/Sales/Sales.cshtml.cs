using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.ViewLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestBrightnesss.Pages.Sales
{
    [Authorize]
    public class SalesModel : PageModel
    {
        private readonly ISalesLogic _sales;
        private readonly IProfileLogic _profile;
        private readonly IInventoryLogic _inventory;

        [BindProperty]
        public SalesView Sale { get; set; }

        [BindProperty]
        public List<SalesView.ProductSaleItem> Inventory { get; set; } // Use SalesView.ProductSaleItem

        public string Email { get; set; } = string.Empty;

        public SalesModel(IInventoryLogic inventory, IProfileLogic profile, ISalesLogic sales)
        {
            _sales = sales;
            _profile = profile;
            _inventory = inventory;
            Sale = new SalesView();
            Inventory = new List<SalesView.ProductSaleItem>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userEmail = User.Identity?.Name;
            
            if (userEmail == null)
            {
                return RedirectToPage("/MicrosoftIdentity/Account/SignIn");
                
            }
            var isAdmin = await _profile.IsUser(userEmail);
            if(isAdmin ==false) 
            {
                return RedirectToPage("/Profile/Register");
            }
            // Populate the inventory with available products
            var inventoryItems = await _inventory.GetAllInvetory();

            Inventory = inventoryItems
                .Where(p => p.ProductId.HasValue)  
                .Select(p => new SalesView.ProductSaleItem
                {
                    ProductId = p.ProductId.Value, 
                    ProductName = p.Name,
                    Price = p.Price,
                    Quantity = 0 
                }).ToList();

            Email = userEmail;
            Sale.SalespersonId = Email;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Retrieve products from hidden fields
            var cartProducts = Request.Form
                .Where(f => f.Key.StartsWith("Sale.Products["))
                .GroupBy(f => int.Parse(f.Key.Split('[')[1].Split(']')[0]))
                .Select(g =>
                {
                    var productId = g.FirstOrDefault(p => p.Key.EndsWith("ProductId")).Value.FirstOrDefault();
                    var productName = g.FirstOrDefault(p => p.Key.EndsWith("ProductName")).Value.FirstOrDefault();
                    var price = g.FirstOrDefault(p => p.Key.EndsWith("Price")).Value.FirstOrDefault();
                    var quantity = g.FirstOrDefault(p => p.Key.EndsWith("Quantity")).Value.FirstOrDefault();

                    return new SalesView.ProductSaleItem
                    {
                        ProductId = Guid.TryParse(productId, out var id) ? id : Guid.Empty,
                        ProductName = productName,
                        Price = decimal.TryParse(price, out var parsedPrice) ? parsedPrice : 0,
                        Quantity = int.TryParse(quantity, out var parsedQuantity) ? parsedQuantity : 0
                    };
                })
                .Where(p => p.Quantity > 0 && p.ProductName != null && p.ProductId != Guid.Empty)
                .ToList();

            // Validate selected products
            if (!cartProducts.Any())
            {
                ModelState.AddModelError(string.Empty, "Please select at least one product with a valid quantity.");
                await OnGetAsync();
                return Page();
            }

            // Set sales properties and record sale
            Email = User.Identity?.Name ?? string.Empty;
            Sale.SalespersonId = Email;
            Sale.DateOfSale = DateTime.Now;
            Sale.Products = cartProducts;

            await _sales.RecordSales(Sale);
            return RedirectToPage("/Sales/SalesDetails");
        }
    }
}
