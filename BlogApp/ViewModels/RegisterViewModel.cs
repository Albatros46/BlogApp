using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }
        //    public string? Image { get; set; }
        [Required]
        [Display(Name = "Ad Soyad")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "E-Mail adresi hatali?")]
        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]//text deilde parola olarak isaretleyecektir.
        [StringLength(10, ErrorMessage = "{0} alani en az {2} karakter olmak zorunda!", MinimumLength = 5)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]//text deilde parola olarak isaretleyecektir.
        [Compare(nameof(Password),ErrorMessage ="Parolaniz eslesmiyor!")]
        [Display(Name = "Password Again")]
        public string? ConfirmPassword { get; set; }
    }
}
