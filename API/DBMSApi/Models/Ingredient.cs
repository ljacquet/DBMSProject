namespace DBMSApi.Models
{
    public class Ingredient
    {
        public int ingredientId { get; set; }

        // Set up roomate ingredient connection
        public ICollection<Roomate> roomates { get; set; }
        public List<RoomateIngredient> roomateIngredients { get; set; }

        // roomate ingredient many to many connection
        public ICollection<Recipe> recipes { get; set; }
        public List<RecipeIngredient> recipeIngredients { get; set; }

        public string ingredientName { get; set; }
    }
}
