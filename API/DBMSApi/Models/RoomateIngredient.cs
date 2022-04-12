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
        public string? priceUnit { get; set; }
        public DateTime expiredDate { get; set; }

        public virtual Ingredient ingredient { get; set; }
        public virtual Roomate roomate { get; set; }
    }
}
