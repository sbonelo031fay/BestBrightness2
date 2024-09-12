using BestBrightnesss.BusinessLogic.LogicInterface;
using BestBrightnesss.Services;
using BestBrightnesss.ViewLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBrightnesss.Pages.Reports
{
    public class InventoryReportModel : PageModel
    {
        private readonly IReportLogic _reportLogic;
        private readonly PdfService _pdfService;

        // Bindable properties for date range using DateOnly
        [BindProperty]
        public DateOnly? StartDate { get; set; }

        [BindProperty]
        public DateOnly? EndDate { get; set; }

        [BindProperty]
        public List<InventoryReportView> Products { get; set; } = new List<InventoryReportView>();

        public InventoryReportModel(IReportLogic reportLogic, PdfService pdfService)
        {
            _reportLogic = reportLogic;
            _pdfService = pdfService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Products = await _reportLogic.GetInventoryReport();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (StartDate.HasValue && EndDate.HasValue)
            {
                if (StartDate.Value > EndDate.Value)
                {
                    ModelState.AddModelError(string.Empty, "Start date cannot be later than end date.");
                    return Page();
                }

            }
            else
            {
                Products = await _reportLogic.GetInventoryReport();
            }

            return Page();
        }

        public async Task<IActionResult> OnGetDownloadAsync()
        {
            Products = await _reportLogic.GetInventoryReport();

            var htmlContent = GenerateHtmlReport();
            var pdfBytes = _pdfService.GeneratePdf(htmlContent);

            return File(pdfBytes, "application/pdf", "InventoryReport.pdf");
        }

        private string GenerateHtmlReport()
        {
            var sb = new StringBuilder();
            sb.Append("<html lang='en'><head>");
            sb.Append("<meta charset='utf-8' />");
            sb.Append("<meta name='viewport' content='width=device-width, initial-scale=1.0' />");
            sb.Append("<title>Inventory Report - Best Brightness</title>");
            sb.Append("<style>");
            sb.Append("body { font-family: 'Arial', sans-serif; background: #f8f9fa; margin: 0; padding: 0; }");
            sb.Append(".report-container { background-color: #fff; border-radius: 10px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); padding: 40px; max-width: 900px; margin: 20px auto; }");
            sb.Append(".report-container img { max-width: 150px; display: block; margin: 0 auto; }");
            sb.Append(".report-container h1 { text-align: center; margin-bottom: 20px; font-size: 2rem; }");
            sb.Append(".report-container p { text-align: center; font-size: 1.1rem; color: #555; margin-bottom: 20px; }");
            sb.Append(".report-container .table { margin-top: 20px; width: 100%; border-collapse: collapse; }");
            sb.Append(".report-container .table th, .report-container .table td { border: 1px solid #ddd; padding: 8px; }");
            sb.Append(".report-container .table th { background-color: #f2f2f2; }");
            sb.Append(".footer { position: fixed; bottom: 0; width: 100%; text-align: center; font-size: 0.9rem; color: #888; padding: 10px; background-color: #f8f9fa; }");
            sb.Append("section { display: flex; justify-content: center; align-items: center; width: 100%; height: 100%; }");
            sb.Append("</style>");
            sb.Append("</head><body>");
            sb.Append("<section>");
            sb.Append("<div class='report-container'>");

            // Company Logo
            sb.Append("<img src='https://localhost:7147/Images/logo1.jpg' alt='Company Logo' />");

            // Report Title and Description
            sb.Append("<h1>Inventory Report</h1>");
            sb.Append("<p>Best Brightness Company<br />");
            sb.Append("Address: 1234 Brightness Ave, Light City, LC 56789<br />");
            sb.Append("Report Description: This report provides a detailed overview of inventory status.</p>");

            // Report Table
            sb.Append("<table class='table'>");
            sb.Append("<thead><tr><th>Product Name</th><th>Stock Level</th><th>Threshold</th><th>Needs Restocking</th></tr></thead>");
            sb.Append("<tbody>");

            foreach (var product in Products)
            {
                sb.Append($"<tr><td>{product.Name}</td>");
                sb.Append($"<td>{product.StockLevel}</td>");
                sb.Append($"<td>{product.RestockThreshold}</td>");
                sb.Append($"<td>{(product.StockLevel < product.RestockThreshold ? "Yes" : "No")}</td></tr>");
            }

            if (!Products.Any())
            {
                sb.Append("<tr><td colspan='4' style='text-align: center;'>No data available</td></tr>");
            }

            sb.Append("</tbody></table>");
            sb.Append("</div></section>");

            // Footer
            sb.Append("<div class='footer'>");
            sb.Append("<p class='text-muted'>&copy; 2024 - Best Brightness</p>");
            sb.Append("</div>");

            sb.Append("</body></html>");

            return sb.ToString();
        }
    }
}
