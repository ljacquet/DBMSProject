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
        public double amount { get; set; }
        public string amountUnit { get; set; }
        public double? price { get; set; }
    }

    public class UpdateIngredientRoomateModel
    {
        public double amount { get; set; }
        public string amountUnit { get; set; }
        public double? price { get; set; }
    }
}
