using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.ViewLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BestBrightnesss.Pages.Inventory
{
    [Authorize]
    public class InventoryModel : PageModel
    {
        private readonly IInventoryLogic _Invetory;

        [BindProperty]
        public List<InvetoryView> Invetory { get; set; }
        public InventoryModel(IInventoryLogic inventory)
        {
            _Invetory = inventory;
            Invetory = new List<InvetoryView>();
        }
        public async Task<IActionResult> OnGet()  
        {
            Invetory = await _Invetory.GetAllInvetory();
            return Page();
        }
    }
}
