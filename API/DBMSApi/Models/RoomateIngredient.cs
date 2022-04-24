using System.ComponentModel.DataAnnotations;

namespace DBMSApi.Models
{
    public class RoomateIngredient
    {
        public int ingredientId { get; set; }
        public int roomateId { get; set; }
        public double quantity { get; set; }
        public string quantityUnit { get; set; }
        public double? price { get; set; }

        public Ingredient ingredient { get; set; }
        public Roomate roomate { get; set; }
    }
}
