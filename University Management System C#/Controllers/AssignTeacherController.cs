using DatabaseProject.Data;
using DatabaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProject.Controllers
{
    public class AssignTeacherController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AssignTeacherController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var obj1 = _db.AssignTeacher.FromSqlRaw("Select * From AssignTeacher");

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

        public IActionResult Create(AssignTeacher obj)

        {
            if (obj.TeacherId == obj.CourseId.ToString())
            {
                ModelState.AddModelError("CustomError", "Display order and TeacherId can not be same");
            }
            if (ModelState.IsValid)
            {




                _db.Database.ExecuteSqlRaw("insert into AssignTeacher (TeacherId,CourseId,AssignedDate) Values (@TeacherId,@CourseId,@AssignedDate)"
, new SqlParameter("@TeacherId", obj.TeacherId), new SqlParameter("@CourseId", obj.CourseId), new SqlParameter("@AssignedDate", obj.AssignedDate));

                //_db.AssignTeacher.Add(obj);
                //  _db.SaveChanges();
                TempData["success"] = "Teacher Assigned successfully";
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

            var categoryFromDb = _db.AssignTeacher.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Edit(AssignTeacher obj)

        {
            if (obj.TeacherId == obj.CourseId.ToString())
            {
                ModelState.AddModelError("CustomError", "CourseId  and TeacherId can not be same");
            }
            if (ModelState.IsValid)
            {
                _db.AssignTeacher.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Allocation edited successfully";
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

            var categoryFromDb = _db.AssignTeacher.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult DeletePOST(int? id, AssignTeacher obj)

        {
            var obj1 = _db.AssignTeacher.Find(id);
            if (obj1 == null)
            {
                return NotFound();
            }

            _db.Database.ExecuteSqlRaw("Delete FROM AssignTeacher WHERE Id = @Id"
              , new SqlParameter("@Id", obj.Id));
            // _db.AssignTeacher.Remove(obj);
            // _db.SaveChanges();
            TempData["success"] = "Teacher unassigned successfully";
            return RedirectToAction("Index");


        }
    }
}
