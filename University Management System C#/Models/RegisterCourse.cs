using System.ComponentModel.DataAnnotations;

namespace DatabaseProject.Models
{
    public class RegisterCourse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string StudentId { get; set; }
        //  [DisplayName("Display Order")]
        [Required]
        public string CourseId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
