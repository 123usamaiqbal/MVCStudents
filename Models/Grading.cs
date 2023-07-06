using System.ComponentModel.DataAnnotations;

namespace MVCStudents.Models
{
    public class Grading
    {
        [Required]
        public int Id { get; set; }
        public int GradingId { get; set; }
        public string Computer { get; set; }
        public string Physics { get; set; }
        public string Science { get; set; }
        


    }
}
