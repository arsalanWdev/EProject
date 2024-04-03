namespace EProject.Models
{
    public class suggest
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AdminResponse { get; set; }
        public DateTime? AdminResponseDate { get; set; }
    }
}
