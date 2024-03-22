using System.ComponentModel.DataAnnotations;

namespace EProject.Models
{
    public class Category
    {
        [Key]
        public int CId {  get; set; }
        [Required]
        public string CName { get; set; }
    }
}
