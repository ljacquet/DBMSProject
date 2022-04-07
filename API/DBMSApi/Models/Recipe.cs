namespace DBMSApi.Models
{
    public class Recipe
    {
        public int recipeId { get; set; }
        public string recipeName { get; set; }
        public string link { get; set; }
        public string description { get; set; }

        public virtual ICollection<RecipeIngredient> ingredients { get; set; }
    }
}
