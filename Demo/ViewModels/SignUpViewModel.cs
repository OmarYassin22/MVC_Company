using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Demo.Presentaion.ViewModels
{
    public class SignUpViewModel
    {
        [EmailAddress(ErrorMessage = "Email Address Is't Right")]
        [Required(ErrorMessage = "Email Address Is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName Is Reqeurd")]
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [MinLength(5, ErrorMessage = "Min Length is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Not Matches")]
        [MinLength(5, ErrorMessage = "Min Length is 5")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool IAccept { get; set; }

        public IFormFile Image { get; set; }
    } 

}
