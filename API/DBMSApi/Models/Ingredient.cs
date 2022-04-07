namespace DBMSApi.Models
{
    public class Ingredient
    {
        public int ingredientId { get; set; }
        public string ingredientName { get; set; }
        public string? substituteNames { get; set; }
        public double? estimatedPrice { get; set; }
    }
}
