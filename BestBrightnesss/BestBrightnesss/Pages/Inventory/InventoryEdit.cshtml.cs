using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.ViewLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace BestBrightnesss.Pages.Inventory
{
    public class InventoryEditModel : PageModel
    {
        private readonly IInventoryLogic _inventoryLogic;

        [BindProperty]
        public InvetoryView Product { get; set; }

        public InventoryEditModel(IInventoryLogic inventoryLogic)
        {
            _inventoryLogic = inventoryLogic;
            Product = new InvetoryView();
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Product = await _inventoryLogic.InventoryByIdAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            Product.ProductId = id;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _inventoryLogic.UpdateInventoryAsync(Product);
            return RedirectToPage("/Inventory/Inventory");
        }
    }
}
