using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace Demo.Presentaion.ViewModels
{
    public class DepartmentView
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Code IS Required")]
        public string Code { get; set; }
        [Required]
        [DisplayName("Creation Of Department")]
        public DateTime CreationDate { get; set; }
    }
}
