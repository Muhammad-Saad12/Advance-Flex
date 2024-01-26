using DatabaseProject.Data;
using DatabaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TeacherController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var obj = _db.Teachers.FromSqlRaw("SELECT * FROM Teachers");
            return View(obj);
        }

        public IActionResult TeacherIndex()
        {
            IEnumerable<Teacher> objCategoryList = _db.Teachers;
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {

            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Teacher obj)

        {

            if (obj.Id == null)
            {
                ModelState.AddModelError("CustomError", "ID can not be null");
            }
            if (obj.FirstName == null)
            {
                ModelState.AddModelError("CustomError", "First Name can not be empty");
            }

            if (obj.Expertise == null)
            {
                ModelState.AddModelError("CustomError", "Teacher must be having any expertise");
            }

            var foo = _db.Teachers.FirstOrDefault(w => w.UserName == obj.UserName);
            var doo = _db.Teachers.FirstOrDefault(u => u.Id == obj.Id);
            if (foo == null && doo == null)
            {
                if (ModelState.IsValid)
                {
                    _db.Database.ExecuteSqlRaw("insert into Teachers (Id,FirstName,LastName,Expertise,PhoneNumber,CreatedDate,Department,UserName,Password) Values (@Id,@FirstName,@LastName,@Expertise,@PhoneNumber,@CreatedDate,@Department,@UserName,@Password)",
                    new SqlParameter("@Id", obj.Id), new SqlParameter("@FirstName", obj.FirstName), new SqlParameter("@LastName", obj.LastName), new SqlParameter("@Expertise", obj.Expertise), new SqlParameter("@PhoneNumber", obj.PhoneNumber), new SqlParameter("@CreatedDate", obj.CreatedDate), new SqlParameter("@Department", obj.Department), new SqlParameter("@UserName", obj.UserName), new SqlParameter("@Password", obj.Password));
                    // _db.Teachers.Add(obj);
                    // _db.SaveChanges();
                    TempData["success"] = "Teacher Added Successfully";
                    return RedirectToAction("TeacherIndex");
                }
            }
            else
            {
                ModelState.AddModelError("CustomError", "Change your User ID or User Name");

            }

            //if (ModelState.IsValid)
            //{
            //    _db.Teachers.Add(obj);
            //    _db.SaveChanges();
            //    TempData["success"] = "Teacher Added Successfully";
            //    return RedirectToAction("Index");
            //}
            return View(obj);

        }

        public IActionResult Edit(string? id)
        {
            if (id == null || id == "0")
            {
                return NotFound();
            }

            var teacherFromDb = _db.Teachers.Find(id);

            if (teacherFromDb == null)
            {
                return NotFound();
            }
            return View(teacherFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Teacher obj)

        {


            _db.Teachers.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Teacher Edited Successfully";
            return RedirectToAction("TeacherIndex");


            // return View(obj);

        }

        public IActionResult Delete(string? id)
        {
            if (id == null || id == "0")
            {
                return NotFound();
            }

            var categoryFromDb = _db.Teachers.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePOST(string? id)

        {
            var obj = _db.Teachers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Teachers.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Teacher deleted successfully";
            return RedirectToAction("TeacherIndex");


        }

    }
}
