namespace RecipeFinder.Models
{
    public enum Role
    {
        Organizer, Housemate
    }

    public class Roomate
    {
        public int roomateId { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public Role role { get; set; }

        public int houseId { get; set; }
        public virtual House house { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
