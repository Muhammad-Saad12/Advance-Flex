using DatabaseProject.Data;
using DatabaseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProject.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AnnouncementController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Announcement> objCategoryList = _db.Announcements;
            return View(objCategoryList);
        }

        public IActionResult StudentAnnouncements()
        {
            var obj = _db.Announcements.FromSqlRaw("SELECT * FROM Announcements WHERE type='student' OR type='both'");
            return View(obj);
        }

        public IActionResult TeacherAnnouncements()
        {
            var obj = _db.Announcements.FromSqlRaw("SELECT * FROM Announcements WHERE type='teacher' OR type='both'");
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

        public IActionResult Create(Announcement obj)

        {


            if (obj.type == "student" || obj.type == "teacher" || obj.type == "both")
            {

                if (ModelState.IsValid)
                {
                    _db.Database.ExecuteSqlRaw("insert into Announcements (type,description) Values (@type,@description)",
                       new SqlParameter("@type", obj.type), new SqlParameter("@description", obj.description));
                  //  _db.Announcements.Add(obj);
                  //  _db.SaveChanges();
                    TempData["success"] = "Announcement made Successfully";
                    return RedirectToAction("Index");
                }
            }

            else
            {
                ModelState.AddModelError("CustomError", "The type must be teacher or student or both");

            }

            return View(obj);

        }

    }
}
