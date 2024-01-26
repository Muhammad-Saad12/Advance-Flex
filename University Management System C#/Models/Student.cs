using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseProject.Models
{
    public class Student
    {
        [Key]
        public string Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Department { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Date Created")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [DisplayName("User Name")]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
