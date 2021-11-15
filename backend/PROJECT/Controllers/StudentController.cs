using BL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace PROJECT.Controllers
{
    [RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
        [Route("GetAll")]
        [HttpGet]
        public List<studentDTO> GetAll()
        {
            studentBL studentListBL = new studentBL();
            List<studentDTO> student = studentListBL.GetAllStudent();
            return student;
        }
        [Route("GetStudentByCodeInstation/{id}")]
        [HttpGet]
        public List<studentDTO> GetStudentByCodeInstation(int id)
        {
            studentBL student = new studentBL();
            List<studentDTO> s = student.GetStudentsByCodeInstatition(id);
            return s;
        }

        // POST: api/Student
        [Route("addStudent/")]
        [HttpPost]
        public void addStudent(studentDTO a)
        {
            studentBL studentListBL = new studentBL();
            studentListBL.AddStudent(a);
        }
        [Route("AddStudent1")]
        [HttpPost]
        public void AddStudent1(List<studentDTO> file)
        {
            file.ForEach(f => { addStudent(f); });

        }
        // PUT: api/Student/5
        [Route("put/{studentDTO}")]
        [HttpPut]
        public void Put(studentDTO student)
        {
            studentBL studentListBL = new studentBL();
            studentListBL.UpdateStudens(student);
        }

        // DELETE: api/Student/5
        [Route("delete/{studentID}")]
        [HttpDelete]
        public void Delete(string studentID)
        {
            studentBL studentListBL = new studentBL();
            studentListBL.DeleteStudent(studentID);
        }
        //שליפת ילדים לאמא
        [Route("get_kids/{tz}")]
        [HttpGet]
        public List<studentDTO> get_kids(string tz)
        {
            studentBL studentBL = new studentBL();
            return studentBL.get_kids(tz);
        }
        //[HttpPost]
        //[Route("api/dashboard/UploadImage")]
        //public HttpResponseMessage UploadImage()
        //{
        //    string imageName = null;
        //    var httpRequest = HttpContext.Current.Request;
        //    //Upload Image
        //    var postedFile = httpRequest.Files["Image"];
        //    //Create custom filename
        //    if (postedFile != null)
        //    {
        //        imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
        //        imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
        //        var filePath = HttpContext.Current.Server.MapPath("~/Images/" + imageName);
        //        postedFile.SaveAs(filePath);
        //    }
    }
    }

