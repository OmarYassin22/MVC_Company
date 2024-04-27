using AutoMapper;
using Demo.DataAccess.Models;
using Demo.Presentaion.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.Presentaion.Mappers
{
    public class MainMapper:Profile
    {
        public MainMapper()
        {
            CreateMap<EmployeeView,Employee>().ReverseMap();
            CreateMap<DepartmentView,Department>().ReverseMap();
            CreateMap<AppUser,UserViewModel>().ReverseMap();    
            CreateMap<RoleViewModel,IdentityRole>().ReverseMap();   
            //CreateMap<Employee,EmployeeView>();
        }
    }
}
