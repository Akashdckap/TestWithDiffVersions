using Assess2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Assess2.Logg;
using System.IO;

namespace Assess2.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DirectorController: ControllerBase
    {
        public DbConnection Connection;
        public DbEntity DbEntity;
        public LogFile logFile = new LogFile();
        public DirectorController(DbConnection context, DbEntity dbEntity)
        {
            Connection = context;
            DbEntity = dbEntity;
        }
        [HttpGet]
        [Route("Directors")]
        public IActionResult GetDirectors()
        {
            /*
            var directors = Connection.GetDirectors();
            return new JsonResult(directors);
            */
            var directors = DbEntity.Directors.ToList();
            return Ok(directors);
        }
        [HttpGet]
        [Route("Directors/{id}")]
        public IActionResult GetDirectors(int id)
        {
        
            var director = DbEntity.Directors.Find(id);
            if (director == null)
            {
                return NotFound("No id exists");
            }
            return Ok(director);
        }

        [HttpPost]
        [Route("AddStudent")]
        public IActionResult AddDirector([FromBody] Directors directors)
        {
            /*
            if (directors == null) {
                return BadRequest("No data found");
            }
            Connection.AddStudents(directors);
            return Ok("Successfully instered");
            */
            DbEntity.Directors.Add(directors);
            logFile.GetLog(directors.ToString());
            DbEntity.SaveChanges();
            return Ok("SuccessFully inserted");
        }

        [HttpPut]
        [Route("UpdateStudent/{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Directors directors)
        {
            /*
            if(id != directors.director_id || id == null)
            {
                return BadRequest("Student id not exists");
            }
            Connection.UpdateDirector(id, directors);
            return Ok("Successfully Updated");
            */
            var ExistingDirector = DbEntity.Directors.Find(id);
            if (ExistingDirector != null)
            {
                ExistingDirector.director_name = directors.director_name;
                ExistingDirector.date_of_birth = directors.date_of_birth;
                ExistingDirector.no_of_movies = directors.no_of_movies;
                ExistingDirector.response = directors.response;
                DbEntity.SaveChanges();
                return Ok("Updated Successfully");
            }
            return BadRequest("Student does not exist");
        }

        [HttpDelete]
        [Route("DeleteDirector/{id}")]
        public IActionResult DeleteDirector(int id)
        {
            /*
            if (id != null)
            {
                Connection.DeleteDirector(id);
                return Ok("Deleted Successfully");
            }
            return BadRequest("Please mention specific id to delete");
            */
            var director = DbEntity.Directors.Find(id);
            if(director != null)
            {
                DbEntity.Directors.Remove(director);
                DbEntity.SaveChanges();
                return Ok("Successfully Deleted");
            }
            return NotFound("Id does not found");
        }
    }
}
