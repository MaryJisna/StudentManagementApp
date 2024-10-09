using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementApp.Data;
using StudentManagementApp.Models;
using System.Diagnostics;

namespace StudentManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.Courses = new List<SelectListItem>
            {
                new SelectListItem { Value = "BCA", Text = "BCA" },
                new SelectListItem { Value = "BBA", Text = "BBA" },
                new SelectListItem { Value = "B-TECH", Text = "B-TECH" },
                new SelectListItem { Value = "BCOM", Text = "BCOM" }
            };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Course,DateOfBirth,PhoneNumber,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                //return Json(new { success = true, message = "Student added successfully" });

                TempData["SuccessMessage"] = "Student record created successfully!";
                return RedirectToAction(nameof(ListData));
            }
            ViewBag.Courses = new List<SelectListItem>
            {
                new SelectListItem { Value = "BCA", Text = "BCA" },
                new SelectListItem { Value = "BBA", Text = "BBA" },
                new SelectListItem { Value = "B-TECH", Text = "B-TECH" },
                new SelectListItem { Value = "BCOM", Text = "BCOM" }
            };
            return View(student);
        }

        public async Task<IActionResult> ListData()
        {
            var students = await _context.Students.ToListAsync();
            return View(students);
        }
    }
    
}
