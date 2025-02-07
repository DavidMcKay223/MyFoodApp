using MyFoodApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodApp.ConsoleApp.Utility
{
    public class SeedData
    {
        public List<FoodCategory> FoodCategories { get; set; }
        public List<StoreSection> StoreSections { get; set; }
        public List<FoodItem> FoodItems { get; set; }
        public List<FoodItemStoreSection> FoodItemStoreSections { get; set; }
        public List<PriceHistory> PriceHistories { get; set; }
        public List<MealSuggestion> MealSuggestions { get; set; }
        public List<MealSuggestionTag> MealSuggestionTags { get; set; }
    }
}
