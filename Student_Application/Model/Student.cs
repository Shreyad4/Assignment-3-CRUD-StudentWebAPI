using System.ComponentModel.DataAnnotations;

namespace Student_Application.Model
{
    public class Student
    {
        public int StudID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Class { get; set; }
        [Required]

        public string Address { get; set; }

    }
}
