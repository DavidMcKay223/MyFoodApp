using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.JSInterop;
using MyFoodApp.Application.Interfaces;
using MyFoodApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.UseCases
{
    public class GeneratorPdf : IGeneratorPdf
    {
        private readonly IJSRuntime JSRuntime;
        private readonly IGeneratorPdfRepository _repository;

        public GeneratorPdf(IJSRuntime JSRuntime, IGeneratorPdfRepository repository)
        {
            this.JSRuntime = JSRuntime;
            _repository = repository;
        }

        public async Task GenerateAndDownloadPdfAsync(string fileName, string content)
        {
            try
            {
                using (var stream = new MemoryStream())
                using (var writer = new PdfWriter(stream))
                using (var pdfDoc = new PdfDocument(writer))
                using (var doc = new Document(pdfDoc))
                {
                    doc.Add(new Paragraph(content));

                    pdfDoc.Close();

                    var pdfBytes = stream.ToArray();
                    var pdfBase64 = Convert.ToBase64String(pdfBytes);

                    await JSRuntime.InvokeVoidAsync("downloadFile", fileName, "application/pdf", pdfBase64);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating PDF: {ex.Message} - {ex.InnerException?.Message}");
            }
        }

        public async Task RecipeListDownloadPdfAsync(string fileName, List<int> recipeIdList)
        {
            try
            {
                using (var stream = new MemoryStream())
                using (var writer = new PdfWriter(stream))
                using (var pdfDoc = new PdfDocument(writer))
                using (var doc = new Document(pdfDoc))
                {
                    List myList = new List();
                    recipeIdList.ForEach(x => myList.Add(x.ToString()));

                    doc.Add(new Paragraph("Recipe List:"));
                    doc.Add(myList);

                    pdfDoc.Close();

                    var pdfBytes = stream.ToArray();
                    var pdfBase64 = Convert.ToBase64String(pdfBytes);

                    await JSRuntime.InvokeVoidAsync("downloadFile", fileName, "application/pdf", pdfBase64);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating PDF: {ex.Message} - {ex.InnerException?.Message}");
            }
        }
    }
}
