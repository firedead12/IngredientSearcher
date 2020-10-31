using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Text;
using IngredientSearcher.DataAccess;
using IngredientSearcher.DataAccess.Model;
using IngredientSearcher.RecipeFetcher.Fetchers.InitialFetcher.Models;
using IngredientSearcher.RecipeFetcher.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Recipe = IngredientSearcher.DataAccess.Model.Recipe;

namespace IngredientSearcher.RecipeFetcher.Fetchers.InitialFetcher
{
    public class InitialFetcher: IRecipeFetcher
    {
        private IServiceProvider _serviceProvider;

        public InitialFetcher(IServiceProvider provider)
        {
            _serviceProvider = provider;
        }
        public async Task FetchRecipes(Uri url)
        {
            var fetchCount = 0;
            using var client = new HttpClient();
            var jsonResponse = await client.GetFromJsonAsync<InitialFetcherJsonResponse>(url);
            while (jsonResponse.recipes.Length > 0)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<RecipeContext>();
                    foreach (var recipe in jsonResponse.recipes)
                    {
                        if (context.Recipes.Any(x => x.Title == recipe.Name))
                            break;
                        var ingredients = await FetchIngredients(recipe.Url);
                        context.Recipes.Add(new Recipe
                        {
                            Ingredients = ingredients.ToArray(),
                            Provider = context.Providers.First(x => x.Api == url),
                            Title = recipe.Name,
                            Url = recipe.Url
                        });
                    }

                    await context.SaveChangesAsync();
                }
                fetchCount += jsonResponse.recipes.Length;
                var uriBuilder = new UriBuilder(url);
                uriBuilder.Query += $"&skip={fetchCount}";
                jsonResponse = await client.GetFromJsonAsync<InitialFetcherJsonResponse>(uriBuilder.Uri);
            }
            throw new NotImplementedException();
        }

        private async Task<List<Ingredient>> FetchIngredients(Uri url)
        {
            var resultIngredientList = new List<Ingredient>();
            var browsingContext = BrowsingContext.New();
            var htmlDocument = await browsingContext.OpenAsync(url.ToString());
            var ingredientList = htmlDocument.QuerySelectorAll(".u-mt--m > table tr");
            foreach (var ingredient in ingredientList)
            {
                var splitIngredients = ingredient.InnerHtml.Split("\n\t");
                resultIngredientList.Add(new Ingredient
                {
                    Title = splitIngredients[0],
                    Amount = splitIngredients[1].SplitSpaces()[0],
                    Annotation = splitIngredients[1].SplitSpaces()[1]
                });
            }

            return resultIngredientList;
        }
    }
}
