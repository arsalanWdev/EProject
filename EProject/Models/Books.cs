using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EProject.Models
{
    public class Books
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        public string Language { get; set; }

        [MaxLength(13)]
        public string ISBN { get; set; }

        [Required,
            DataType(DataType.Date),
            Display(Name ="Date Published")]

        public DateTime DatePublished { get; set; }

        [Required,
            DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        public string Author{ get; set;}

        [Required]
        public string Publication {  get; set;}

        [Display(Name ="Image Url")]
        public string ImageUrl {  get; set;}

        [ForeignKey("Categories")]
        public int CategoryId {  get; set; }

        public virtual Category Categories { get; set; }
    }
}
