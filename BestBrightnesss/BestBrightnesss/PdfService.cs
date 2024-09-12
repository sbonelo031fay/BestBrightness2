using DinkToPdf;
using DinkToPdf.Contracts;

namespace BestBrightnesss.Services
{
    public class PdfService
    {
        private readonly IConverter _converter;

        public PdfService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GeneratePdf(string htmlContent)
        {
            var document = new HtmlToPdfDocument
            {
                GlobalSettings = {
                    DocumentTitle = "Daily Sales Report",
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait
                    // Set other global settings if needed
                },
                Objects = {
                    new ObjectSettings {
                        HtmlContent = htmlContent,
                        WebSettings = {
                            DefaultEncoding = "utf-8",
                            LoadImages = true // Ensure that images are loaded
                            // Set other web settings if needed
                        }
                    }
                }
            };

            return _converter.Convert(document);
        }
    }
}
