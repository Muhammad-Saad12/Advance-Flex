using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseProject.Models
{
    public class Course
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Credit Hour")]
        public string CreditHour { get; set; }
        [DisplayName("Grading Policy")]
        public string GradingPolicy { get; set; }
    }
}
