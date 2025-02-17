using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MyFoodApp.Application.Interfaces;
using MyFoodApp.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.Application.UseCases
{
    public class GeneratorPdf : IGeneratorPdf
    {
        private readonly IGeneratorPdfRepository _repository;

        public GeneratorPdf(IGeneratorPdfRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> GenerateRecipeListPdfAsync(List<int> recipeIdList)
        {
            using (var stream = new MemoryStream())
            using (var writer = new PdfWriter(stream))
            using (var pdfDoc = new PdfDocument(writer))
            using (var doc = new Document(pdfDoc))
            {
                var recipeList = await _repository.GetAllRecipesByIdListAsync(recipeIdList);

                foreach (var recipe in recipeList)
                {
                    if (recipe != null)
                    {
                        if (doc.GetPdfDocument().GetNumberOfPages() > 0)
                        {
                            doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                        }

                        doc.Add(new Paragraph($"{recipe.Title}")
                            .SetFontSize(14));

                        if (recipe.Steps.Any())
                        {
                            doc.Add(new Paragraph("Steps:").SetFontSize(12));
                            List stepList = new List(ListNumberingType.DECIMAL);
                            foreach (var step in recipe.Steps.Where(s => s != null))
                            {
                                stepList.Add(new ListItem(step.Instruction));
                            }
                            doc.Add(stepList);
                        }

                        if (recipe.Ingredients.Any())
                        {
                            doc.Add(new Paragraph("Ingredients:").SetFontSize(12));
                            List ingredientList = new List();
                            foreach (var ingredient in recipe.Ingredients.Where(i => i?.FoodItem != null))
                            {
                                var sb = new StringBuilder()
                                    .Append($"{ingredient.Quantity} {ingredient.Unit} {ingredient.FoodItem.Name}");

                                var priceHistory = ingredient.FoodItem.PriceHistories.FirstOrDefault();
                                if (priceHistory != null)
                                {
                                    sb.Append($" - Price: {priceHistory.Price:C}");
                                }

                                sb.Append($" - Section: {ingredient?.FoodItem?.FoodCategory?.Name}");

                                ingredientList.Add(sb.ToString());
                            }

                            doc.Add(ingredientList);
                        }

                        doc.Add(new Paragraph("\n"));
                    }
                }

                if (recipeIdList.Count > 1)
                {
                    if (doc.GetPdfDocument().GetNumberOfPages() > 0)
                    {
                        doc.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                    }

                    doc.Add(new Paragraph("Grocery List:").SetFontSize(14));

                    var allIngredients = recipeList
                        .SelectMany(r => r.Ingredients)
                        .Where(i => i?.FoodItem != null)
                        .OrderBy(x => x.FoodItem.FoodCategory?.Name)
                        .ThenBy(x => x.FoodItem.Name)
                        .ToList();

                    var groceryListByCategory = allIngredients
                        .GroupBy(i => i.FoodItem.FoodCategory?.Name ?? "Unknown")
                        .OrderBy(g => g.Key)
                        .ToList();

                    foreach (var categoryGroup in groceryListByCategory)
                    {
                        doc.Add(new Paragraph($"{categoryGroup.Key}")
                            .SetFontSize(12));

                        var ingredientsInCategory = categoryGroup
                            .GroupBy(i => new { i.FoodItem.Name, i.Unit })
                            .Select(g => new
                            {
                                FoodItemName = g.Key.Name,
                                Unit = g.Key.Unit,
                                TotalQuantity = g.Sum(i => i.Quantity),
                                TotalPrice = g.Sum(i => i.FoodItem.PriceHistories.FirstOrDefault()?.Price ?? 0)
                            })
                            .OrderBy(x => x.FoodItemName)
                            .ToList();

                        List groceryItemList = new List().SetMarginLeft(20);

                        foreach (var item in ingredientsInCategory)
                        {
                            var sb = new StringBuilder().Append($"{item.FoodItemName}: {item.TotalQuantity:0.00} {item.Unit}");

                            if (item.TotalPrice > 0)
                            {
                                sb.Append($" - Price: {item.TotalPrice:C}");
                            }

                            groceryItemList.Add(sb.ToString());
                        }

                        doc.Add(groceryItemList);
                    }

                    var totalPrice = allIngredients.Sum(i => i.FoodItem.PriceHistories.FirstOrDefault()?.Price ?? 0 * i.Quantity);
                    if (totalPrice > 0)
                    {
                        doc.Add(new Paragraph($"Total Price for All Items: {totalPrice:C}")
                            .SetFontSize(12));
                    }
                }

                pdfDoc.Close();
                return stream.ToArray();
            }
        }
    }
}
