namespace EProject.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Books Book { get; set; }
        public int Quantity { get; set; }
        public string CartId { get; set; }
    }
}
