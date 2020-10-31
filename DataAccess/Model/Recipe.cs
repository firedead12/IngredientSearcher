using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IngredientSearcher.DataAccess.Model
{
    public class Recipe
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public Uri Url { get; set; }
        [Column(TypeName = "jsonb")]
        public Ingredient[] Ingredients { get; set; }
        public Provider Provider { get; set; }
    }
}