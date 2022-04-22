namespace DBMSApi.Controllers.Viewmodels
{
    public class CreateRoomateViewModel
    {
        public string username { get; set; }
        public int? houseId { get; set; }
    }

    public class AddIngredientRoomateViewModel
    {
        public int ingredientId { get; set; }
        public int amount { get; set; }
        public string amountUnit { get; set; }
        public double? price { get; set; }
        public string? priceUnit { get; set; }
        public DateTime expiredDate { get; set; }
    }
}
