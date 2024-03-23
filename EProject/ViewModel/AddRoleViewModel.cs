using System.ComponentModel.DataAnnotations;

namespace EProject.ViewModel
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name ="Role")]
        public string RoleName { get; set; }
    }
}
