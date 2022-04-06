namespace RecipeFinder.Models
{
    public class Ingredient
    {
        public int ingredientId { get; set; }
        public string ingredientName { get; set; }
        public string? substituteNames { get; set; }
        public double quantity { get; set; }
        public string quantityUnit { get; set; }
        public double price { get; set; }
        public string priceUnit { get; set; }
        public DateTime expiredDate { get; set; }
    }
}
