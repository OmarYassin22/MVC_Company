using System.ComponentModel.DataAnnotations;

namespace Demo.Presentaion.ViewModels
{
	public class ResetPasswordViewModel
	{
        [Required]
        [MinLength(5)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [MinLength(5)]
        [Compare(nameof(NewPassword))]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
