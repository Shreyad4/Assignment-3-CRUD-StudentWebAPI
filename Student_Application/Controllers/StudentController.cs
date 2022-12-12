using Microsoft.AspNetCore.Mvc;
using Student_Application.Model;
using Student_Application.Data;
using Microsoft.AspNetCore.JsonPatch;

namespace Student_Application.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //GetAllData
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            return Ok(StudentStore.StudentList);

        }
        
        //GET
        [HttpGet("{id:int}",Name="GetStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        public ActionResult<StudentDTO> GetStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
          var student = StudentStore.StudentList.FirstOrDefault(u => u.StudID == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);


        }

        //CREATE
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudentData([FromBody]StudentDTO studentDTO)
        {
            //if(!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if(StudentStore.StudentList.FirstOrDefault(u=>u.Name.ToLower()==studentDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError","Already Exits!!");
                return BadRequest(ModelState);
            }
            if(studentDTO == null)
            {
                return BadRequest(studentDTO);

            }
            if(studentDTO.StudID > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            studentDTO.StudID = StudentStore.StudentList.OrderByDescending(u => u.StudID).FirstOrDefault().StudID+1;
            StudentStore.StudentList.Add(studentDTO);

            return CreatedAtRoute("GetStudent",new { id = studentDTO.StudID},studentDTO);
        }

        //DELETE
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteStudent")]

        public IActionResult DeleteStudent(int id)
        {
            if (id == 0)
            {
                return BadRequest();

            }
            var student = StudentStore.StudentList.FirstOrDefault(u=>u.StudID==id);
            if (student == null)
            {
                return NotFound();
            }
            StudentStore.StudentList.Remove(student);
            return NoContent();
        }

        //UPDATE-PUT
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}",Name="UpdateStudent")]
        public IActionResult UpdateStudent(int id, [FromBody]StudentDTO studentDTO)
        {
            if(studentDTO == null || id!=studentDTO.StudID)
            {
                return BadRequest();
            }
            var student = StudentStore.StudentList.FirstOrDefault(u => u.StudID == id);
            student.Name = studentDTO.Name;
            student.Class= studentDTO.Class;
            student.Address= studentDTO.Address;

            return NoContent();

        }

        //UPDATE-PATCH
        [HttpPatch("{id:int}", Name = "UpdatePatchStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePatch(int id , JsonPatchDocument<StudentDTO> patchDTO)
        {
            if(patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var student = StudentStore.StudentList.FirstOrDefault(u=>u.StudID==id);
            if(student == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(student, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
           
        }

    }
}
