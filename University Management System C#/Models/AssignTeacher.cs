using System.ComponentModel.DataAnnotations;

namespace DatabaseProject.Models
{
    public class AssignTeacher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TeacherId { get; set; }
        //  [DisplayName("Display Order")]
        [Required]
        public string CourseId { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;
    }
}
