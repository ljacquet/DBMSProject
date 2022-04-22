namespace DBMSApi.Models
{
    public class Roomate
    {
        public int roomateId { get; set; }
        public string username { get; set; }
        public bool isOwner { get; set; }

        public int? houseId { get; set; }
        public virtual House house { get; set; }

        // roomate ingredient connection
        public virtual ICollection<Ingredient> ingredients { get; set; }
        public virtual ICollection<RoomateIngredient> roomateIngredients { get; set; }
    }
}
