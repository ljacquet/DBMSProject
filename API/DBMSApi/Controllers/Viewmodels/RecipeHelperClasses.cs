using DBMSApi.Models;

namespace DBMSApi.Controllers.Viewmodels
{
    public class CreateRecipeViewModel
    {
        public string name { get; set; }
        public string description { get; set; }
    }

    public class IngredientInstructions
    {
        public int ingredientId { get; set; }
        public double amount { get; set; }
        public string ingredientUnit { get; set; }
    }

    public class UpdateRecipeMetadata
    {
        public string? name { get; set; }
        public string? description { get; set; }
    }

    public class AddIngredientToRecipeViewModel
    {
        public int ingredientId { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
    }

    public class RecipeIngredientResponseModel
    {
        public string ingredientName { get; set; }
        public int ingredientId { get; set; }
        public double ingredientAmount { get; set; }
        public string ingredientUnit { get; set; }
    }

    public class RecipeResponseModel
    {
        public int recipeId { get; set; }
        public string recipeName { get; set; }
        public string description { get; set; }

        public List<RecipeIngredientResponseModel> ingredients { get; set; }

        public static RecipeResponseModel recipeResponseBuilder(Recipe r)
        {
            var ingredients = new List<RecipeIngredientResponseModel>();

            foreach (var ringredient in r.recipeIngredients)
            {
                ingredients.Add(new RecipeIngredientResponseModel()
                {
                    ingredientId = ringredient.ingredientId,
                    ingredientAmount = ringredient.ingredientAmount,
                    ingredientUnit = ringredient.ingredientUnit,
                    ingredientName = ringredient.ingredient.ingredientName
                });
            }

            return new RecipeResponseModel()
            {
                recipeId = r.recipeId,
                recipeName = r.recipeName,
                description = r.description,
                ingredients = ingredients
            };
        }
    }
}
