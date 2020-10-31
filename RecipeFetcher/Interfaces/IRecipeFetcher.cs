using System;
using System.Threading.Tasks;
using IngredientSearcher.DataAccess.Model;

namespace IngredientSearcher.RecipeFetcher.Interfaces
{
    public interface IRecipeFetcher
    {
        Task FetchRecipes(Uri url);
    }
}