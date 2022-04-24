using System.ComponentModel.DataAnnotations;

namespace DBMSApi.Models
{
    public class RecipeIngredient
    {
        // Recipe Ingredient uses a compound key of recipeId ingredientId.
        // This is done in a separate file.
        public int recipeId { get; set; }
        public int ingredientId { get; set; }
        public double ingredientAmount { get; set; }
        public string ingredientUnit { get; set; }

        public Recipe recipe { get; set; }
        public Ingredient ingredient { get; set; }
    }
}
