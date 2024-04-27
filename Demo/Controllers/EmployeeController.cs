using AutoMapper;
using Demo.Busiess.Interfaces;
using Demo.DataAccess.Models;
using Demo.Presentaion.Helpers;
using Demo.Presentaion.Mappers;
using Demo.Presentaion.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Presentaion.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        //private readonly IEmployeeRepository employeeRepository;

        /*private readonly IDepartmentRepository departmentRepository;*/

        public EmployeeController(/*IEmployeeRepository employeeRepository*/IUnitOfWork unitOfWork,IMapper mapper/*,IDepartmentRepository departmentRepository*/)
        {
            this.unitOfWork = unitOfWork;
            //_employeeRepository = employeeRepository;
            //this.departmentRepository = departmentRepository;


            this.mapper = mapper;

        }

        public async Task<IActionResult> Index(string name)
            {
            if (name == null)
            {
                var emp = await unitOfWork.EmployeeRepository.GetAll();
                
                var viewEmp = mapper.Map<IEnumerable<EmployeeView>>(emp);
                return View(viewEmp);
            }
            else
            { var emp = await unitOfWork.EmployeeRepository.Search(name);
                ViewData["Name"]=name;
                var viewEmp = mapper.Map<EmployeeView>(emp);

                return View(viewEmp);
            }
        }
        public async Task<IActionResult> Details(int? id,string ViewName="Details")
        {
            if (id is null) return BadRequest();
            var res = mapper.Map<EmployeeView>(await unitOfWork.EmployeeRepository.Get(id.Value));

            return View(ViewName, res);
        }
        public IActionResult Create()
        {
            //ViewData["Departments"] = departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeView employee)  
        {
            /*if (ModelState.IsValid)
            {*/

                var _emplyee = mapper.Map<Employee>(employee);
            if (employee.ImageName is not null)
                _emplyee.ImageName = DocumentSettings.Upload(employee.Image, "images");
            else
                _emplyee.ImageName = "9deff597-4213-49e0-9277-1e94882115e7Screenshot 2024-03-19 035806.png";

            unitOfWork.EmployeeRepository.Create(_emplyee);

                if ( await unitOfWork.DepartmentRepository.Commit() > 0)
                {
                    TempData["Created"] = $"{employee.Name} has benn created";
                    return RedirectToAction("Index");
                }
                return View(employee);
           /* }
            return View(employee);*/
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id) {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeView employee)
        {
            /*if (ModelState.IsValid)
            {*/
            unitOfWork.EmployeeRepository.Delete(mapper.Map<Employee>(employee));
            var count = await unitOfWork.DepartmentRepository.Commit();
            if (count > 0)
            {
            DocumentSettings.DeleteFile(employee.ImageName, "images");
                TempData["Deleted"] = $"{employee} has benn deleted";
                return RedirectToAction("Index");
            
            }
            return View(employee);
        /*    }
            return View(employee);*/
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            //ViewData["Departments"] = departmentRepository.GetAll();
            return await Details(id, "Update");

        }
        [HttpPost]
        public IActionResult Update(EmployeeView employee)
        {
            if(ModelState.IsValid)
            {
                var emp= mapper.Map<Employee>(employee);

                if(employee.ImageName is not null) 
                    DocumentSettings.DeleteFile(employee.ImageName,"images");
                emp.ImageName = DocumentSettings.Upload(employee.Image, "images");
                unitOfWork.EmployeeRepository.Update(emp);
                if (unitOfWork.DepartmentRepository.Commit().Result >0)
                {
                    TempData["Updated"]=$"{emp.Name} has been updated ";
                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }

    }
}
