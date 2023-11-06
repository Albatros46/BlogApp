using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels
{
    public class LoginViewModel
    {
        

        [Required]
        [EmailAddress(ErrorMessage ="E-Mail adresi hatali?")]
        [Display(Name ="E-Mail")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10,ErrorMessage ="{0} alani en az {2} karakter olmak zorunda!",MinimumLength=5)]
        [DataType(DataType.Password)]//text deilde parola olarak isaretleyecektir.
        [Display (Name ="Password")]
        public string? Password { get; set; }

    }
}
