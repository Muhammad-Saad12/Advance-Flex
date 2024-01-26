using DatabaseProject.Data;
using DatabaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public StudentController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var obj = _db.Students.FromSqlRaw("SELECT * FROM Students");
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

        public IActionResult Create(Student obj)

        {

            if (obj.Id == null)
            {
                ModelState.AddModelError("CustomError", "ID can not be null");
            }
            if (obj.FirstName == null)
            {
                ModelState.AddModelError("CustomError", "First Name can not be empty");
            }

            if (obj.Department == null)
            {
                ModelState.AddModelError("CustomError", "Student must be enrolled in a department");
            }

            var foo = _db.Students.FirstOrDefault(w => w.UserName == obj.UserName);
            var doo = _db.Students.FirstOrDefault(u => u.Id == obj.Id);
            if (foo == null && doo == null)
            {
                if (ModelState.IsValid)
                {
                    _db.Database.ExecuteSqlRaw("insert into Students (Id,FirstName,LastName,Department,PhoneNumber,CreatedDate,UserName,Password) Values (@Id,@FirstName,@LastName,@Department,@PhoneNumber,@CreatedDate,@UserName,@Password)",
                     new SqlParameter("@Id", obj.Id), new SqlParameter("@FirstName", obj.FirstName), new SqlParameter("@LastName", obj.LastName), new SqlParameter("@Department", obj.Department), new SqlParameter("@PhoneNumber", obj.PhoneNumber),new SqlParameter("@CreatedDate",obj.CreatedDate), new SqlParameter("@UserName", obj.UserName), new SqlParameter("@Password", obj.Password));
                    //_db.Students.Add(obj);
                    //_db.SaveChanges();
                    TempData["success"] = "Student Added Successfully";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("CustomError", "Chnage your User ID or User Name");

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

            var studentFromDb = _db.Students.Find(id);

            if (studentFromDb == null)
            {
                return NotFound();
            }
            return View(studentFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(Student obj)

        {


            _db.Students.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Student Edited Successfully";
            return RedirectToAction("Index");


            // return View(obj);

        }

        public IActionResult Delete(string? id)
        {
            if (id == null || id == "0")
            {
                return NotFound();
            }

            var categoryFromDb = _db.Students.Find(id);

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
            var obj = _db.Students.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Students.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Student deleted successfully";
            return RedirectToAction("Index");


        }

    }
}

