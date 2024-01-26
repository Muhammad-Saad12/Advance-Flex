using DatabaseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AssignTeacher> AssignTeacher { get; set; }
        public DbSet<RegisterCourse> RegisterCourses { get; set; }
    }
}
