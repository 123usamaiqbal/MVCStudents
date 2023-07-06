using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCStudents.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Fname { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        //public byte[]? ImageData { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
