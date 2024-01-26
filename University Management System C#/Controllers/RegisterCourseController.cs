using DatabaseProject.Data;
using DatabaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProject.Controllers
{
    public class RegisterCourseController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RegisterCourseController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var obj1 = _db.RegisterCourses.FromSqlRaw("Select * From RegisterCourses");
            return View(obj1);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(RegisterCourse obj)

        {
            if (obj.StudentId == obj.CourseId.ToString())
            {
                ModelState.AddModelError("CustomError", "Display order and StudentId can not be same");
            }
            if (ModelState.IsValid)
            {




                _db.Database.ExecuteSqlRaw("insert into RegisterCourses (StudentId,CourseId,CreatedDate) Values (@StudentId,@CourseId,@CreatedDate)"
, new SqlParameter("@StudentId", obj.StudentId), new SqlParameter("@CourseId", obj.CourseId), new SqlParameter("@CreatedDate", obj.CreatedDate));

                //_db.RegisterCourses.Add(obj);
                //  _db.SaveChanges();
                TempData["success"] = "Registeration Successfull.";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.RegisterCourses.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(RegisterCourse obj)

        {
            if (obj.StudentId == obj.CourseId.ToString())
            {
                ModelState.AddModelError("CustomError", "Display order and StudentId can not be same");
            }
            if (ModelState.IsValid)
            {



                  _db.RegisterCourses.Update(obj);
                 _db.SaveChanges();
                TempData["success"] = "Category edited successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFromDb = _db.RegisterCourses.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePOST(int? id, RegisterCourse obj)

        {
            var obj1 = _db.RegisterCourses.Find(id);
            if (obj1 == null)
            {
                return NotFound();
            }

           // _db.Database.ExecuteSqlRaw("Delete FROM Students WHERE Id = @Id"
           //   , new SqlParameter("@Id", obj.Id));
             _db.RegisterCourses.Remove(obj);
             _db.SaveChanges();
            TempData["success"] = "Registration cancelled  successfully";
            return RedirectToAction("Index");


        }
    }
}
