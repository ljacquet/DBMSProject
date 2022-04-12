namespace DBMSApi.Controllers.Viewmodels
{
    public class CreateRecipeViewModel
    {
        public string name { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public List<IngredientInstructions> ingredientIds { get; set; }
    }

    public class IngredientInstructions
    {
        public int ingredientId { get; set; }
        public double amount { get; set; }
        public string ingredientUnit { get; set; }
    }

    public class UpdateRecipeMetadata
    {
        public int recipeId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? link { get; set; }
    }

    public class AddIngredientToRecipeViewModel
    {
        public int recipeId { get; set; }
        public int ingredientId { get; set; }
        public double amount { get; set; }
        public string unit { get; set; }
    }
}
