using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseProject.Models
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Add Type (Student , Teacher or Both)")]
        public string type { get; set; }
        [DisplayName("Description")]
        public string description { get; set; }
    }
}
