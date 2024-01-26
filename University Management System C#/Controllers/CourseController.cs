using DatabaseProject.Data;
using DatabaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CourseController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var obj = _db.Courses.FromSqlRaw("SELECT * FROM Courses");
            return View(obj);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Course obj)

        {

            if (obj.Id == null)
            {
                ModelState.AddModelError("CustomError", "ID can not be null");
            }
            if (obj.Name == null)
            {
                ModelState.AddModelError("CustomError", "Course Name can not be empty");
            }

            if (obj.CreditHour == null)
            {
                ModelState.AddModelError("CustomError", "Course must be having credit hour between 1-4");
            }

            var doo = _db.Courses.FirstOrDefault(u => u.Id == obj.Id);

            if (doo == null)
            {
                if (ModelState.IsValid)
                {
                  _db.Database.ExecuteSqlRaw("insert into Courses (Id,Name,CreditHour,GradingPolicy) Values (@Id,@Name,@CreditHour,@GradingPolicy)", 
                      new SqlParameter("@Id", obj.Id), new SqlParameter("@Name", obj.Name), new SqlParameter("@CreditHour", obj.CreditHour),new SqlParameter("@GradingPolicy",obj.GradingPolicy));
                    // _db.Courses.Add(obj);
                    // _db.SaveChanges();
                    TempData["success"] = "Course Added Successfully";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("CustomError", "Course Id can not be same");
            }
            return View(obj);

        }

        public IActionResult Edit(string? id)
        {
            if (id == null || id == "0")
            {
                return NotFound();
            }

            var courseFromDb = _db.Courses.Find(id);

            if (courseFromDb == null)
            {
                return NotFound();
            }
            return View(courseFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Course obj)

        {

            _db.Courses.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Course Edited Successfully";
            return RedirectToAction("Index");

            // return View(obj);

        }

        public IActionResult Delete(string? id)
        {
            if (id == null || id == "0")
            {
                return NotFound();
            }

            var courseFromDb = _db.Courses.Find(id);

            if (courseFromDb == null)
            {
                return NotFound();
            }
            return View(courseFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePOST(string? id)

        {
            var obj = _db.Courses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Courses.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Course deleted successfully";
            return RedirectToAction("Index");


        }
    }
    }
