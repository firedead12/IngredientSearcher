using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngredientSearcher.RecipeFetcher.Fetchers.InitialFetcher.Models
{
    public class InitialFetcherJsonResponse
    {
        public Recipe[] recipes { get; set; }
        public int totalCount { get; set; }
        public int totalMatchedRecipes { get; set; }
    }

    public class Recipe
    {
        public string Name { get; set; }
        public string PreparationTotalTime { get; set; }
        public Uri Url { get; set; }
    }

}


