using IngredientSearcher.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace IngredientSearcher.DataAccess
{
    public class RecipeContext : DbContext
    {
        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
            
        }
        
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Provider> Providers { get; set; }
    }
}