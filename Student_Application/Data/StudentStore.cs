using Student_Application.Model;

namespace Student_Application.Data
{
    public class StudentStore
    {
        public static List<StudentDTO> StudentList =new List<StudentDTO>
            {
                new StudentDTO{ StudID=1,Name="Shreya",Class=12,Address="Bangalore"},
                new StudentDTO{ StudID=2,Name="Sirii",Class=12,Address="Mangalore"}

            };

    }
}
