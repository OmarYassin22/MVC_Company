using AutoMapper;
using Demo.Busiess.Interfaces;
using Demo.Busiess.Repositories;
using Demo.DataAccess.Models;
using Demo.Presentaion.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Presentaion.Controllers
{
    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository department;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(/*IDepartmentRepository _deprtment*/ IUnitOfWork unitOfWork, IMapper mapper)
        {
            //department = _deprtment;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string Search)
            {
            var dept =await unitOfWork.DepartmentRepository.GetAll();
            if (!string.IsNullOrEmpty(Search))
            {
                dept=await unitOfWork.DepartmentRepository.Search(Search);
            }
            TempData["name"]=Search;
            var deptView = mapper.Map<IEnumerable<DepartmentView>>(dept);
            return View(deptView);
        }
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {

            if (id is null) return BadRequest();
            var dept =await unitOfWork.DepartmentRepository.Get(id.Value);
            if (dept is null) return NotFound();
            var deptview= mapper.Map<DepartmentView>(dept);
            return View(ViewName, deptview);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int? id, Department dept)
        {
            if (id != dept.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                unitOfWork.DepartmentRepository.Delete(dept);
                var count =await unitOfWork.DepartmentRepository.Commit();

                TempData["Deleted"] = $"{dept.Name} Have Been Deleted";
                if (count > 0)
                {
                    TempData["Message"] = $"Department {dept.Name} Is Deleted";
                    return RedirectToAction("Index");
                }
            }
            return View(dept);

        }
        [HttpGet]
        public async Task<IActionResult> Update([FromRoute] int? id)
        {
            return await Details(id, "Update");


        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Update([FromRoute] int? id, Department dept)
        {
            if (id != dept.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                unitOfWork.DepartmentRepository.Update(dept);
                var count = unitOfWork.DepartmentRepository.Commit().Result;
                if (count > 0)
                {
                    TempData["Updated"] = $"{dept.Name} Updated";
                    return RedirectToAction("Index");
                }
            }
            return View(dept);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department _department)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.DepartmentRepository.Create(_department);
                var Count = unitOfWork.DepartmentRepository.Commit().Result;
                if (Count > 0)
                {
                    TempData["Created"] = $"{_department.Name} Has Been Added";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
    }
}
