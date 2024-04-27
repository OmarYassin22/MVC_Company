using System.ComponentModel.DataAnnotations;

namespace Demo.Presentaion.ViewModels
{
    public class SingInViewModel
    {

        [Required(ErrorMessage ="UserName Is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password Is Required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
