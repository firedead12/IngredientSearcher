using System;
using System.ComponentModel.DataAnnotations;

namespace IngredientSearcher.DataAccess.Model
{
    public class Provider
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public Uri Api { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}