using System.ComponentModel.DataAnnotations;

namespace Student_Application.Model
{
    public class StudentDTO
    {
        public static IEnumerable<StudentDTO> StudentList { get; internal set; }
        public int StudID { get; set; }
        [Required]
        [MaxLength (50)]
        public string Name { get; set; }
        [Required]
        public int Class { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
