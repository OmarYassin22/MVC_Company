using Demo.DataAccess.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.Presentaion.ViewModels
{
    public class EmployeeView
    {/*
        [RegularExpression(@"[0-9]{1-3}-[a-zA-Z]{5-10}-[a-zA-Z]{5-10}$",
            ErrorMessage = "Address must be like 123-street-city-country")]*/
        public int Id { get; set; }
        public string  Name { get; set; }
        public string Address { get; set; }
        [Range(22, 60, ErrorMessage = "Age must be between 22 and 60")]
        public int Age { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HireDate { get; set; }
        public float Salary { get; set; }
        public bool Active { get; set; }

        public int? departmentId { get; set; }
        public Department department { get; set; }

        public string ImageName { get; set; }
        public IFormFile Image { get; set; }



    }
}
