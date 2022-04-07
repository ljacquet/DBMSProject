using System.ComponentModel.DataAnnotations;

namespace DBMSApi.Models
{
    public class RecipeIngredient
    {
        public int recipeId { get; set; }
        public int ingredientId { get; set; }
        public double ingredientAmount { get; set; }
        public string ingredientUnit { get; set; }

        public virtual Recipe recipe { get; set; }
        public virtual Ingredient ingredient { get; set; }
    }
}
