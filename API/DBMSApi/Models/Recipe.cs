namespace DBMSApi.Models
{
    public class Recipe
    {
        public int recipeId { get; set; }
        public string recipeName { get; set; }
        public string description { get; set; }

        public virtual ICollection<Ingredient> ingredients { get; set; }
        public virtual List<RecipeIngredient> recipeIngredients { get; set; }
    }
}
