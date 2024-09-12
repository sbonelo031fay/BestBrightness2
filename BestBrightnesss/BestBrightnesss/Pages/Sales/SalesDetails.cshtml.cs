using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.DataLogic.Reports;
using BestBrightnesss.ViewLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BestBrightnesss.Pages.Sales
{
    public class SaleDetailsModel : PageModel
    {
        private readonly ISalesLogic _sales;
        private readonly IProfileLogic _profile;
        private readonly IInventoryLogic _inventory;

        [BindProperty]
        public List<DailySalesReportView> DailySalesReports { get; set; } = new List<DailySalesReportView>();

        [BindProperty]
        public List<SalesView.ProductSaleItem> Inventory { get; set; } // Use SalesView.ProductSaleItem

        public string Email { get; set; } = string.Empty;

        public SaleDetailsModel(IInventoryLogic inventory, IProfileLogic profile, ISalesLogic sales)
        {
            _sales = sales;
            _profile = profile;
            _inventory = inventory;
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
            if (isAdmin == false)
            {
                return RedirectToPage("/Profile/Register");
            }
            DailySalesReports = await _inventory.GetSales(userEmail);
            return Page();
        }
    }
}
