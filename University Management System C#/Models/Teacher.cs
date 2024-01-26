using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseProject.Models
{
    public class Teacher
    {
        [Key]
        public string Id { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Expertise { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [DisplayName("Date Created")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string Department { get; set; }


        public string UserName { get; set; }
        public string Password { get; set; }

    }
}
