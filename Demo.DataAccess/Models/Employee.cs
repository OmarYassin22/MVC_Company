using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Models
{
    public class Employee : BaseEntity
    {




        public string Address { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime HireDate { get; set; }
        public float Salary { get; set; }
        public bool Active { get; set; }

        public int? departmentId { get; set; }
        public Department department { get; set; }

        public string ImageName { get; set; }
       

    }
}
