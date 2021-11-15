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
    [RoutePrefix("api/Teacher")]
    public class TeacherController : ApiController
    {
        [Route("GetTeachersByCodeInstation/{id}")]
        [HttpGet]

        public List<teacherDTO> GetTeachersByCodeInstation(int id)
        {
            teacherBL teachers = new teacherBL();
            List<teacherDTO> teacher = teachers.GetTeachersByCodeInstatition(id);
            return teacher;
        }


        [Route("AddTEacher1")]
        [HttpPost]
        public void AddTEacher1(List<teacherDTO> file)
        {
            file.ForEach(f => { Post(f); });

        }
        [Route("GetAll")]
        [HttpGet]
        public List<teacherDTO> GetAll()
        {
            teacherBL teacherListBL = new teacherBL();
            List<teacherDTO> teacher = teacherListBL.GetAllTeachers();
            return teacher;
        }

        // POST: api/Teacher
        [Route("Post/{teacher}")]
        [HttpGet]
        public void Post(teacherDTO teacher)
        {
            teacherBL teacherListBL = new teacherBL();
            teacherListBL.AddTeachers(teacher);
        }
        [Route("Post1")]
        [HttpPost]
        public void Post1()
        {
            teacherBL teacherListBL = new teacherBL();
            teacherListBL.addTeacherFromExcel();

        }

        // PUT: api/Teacher/5
        [Route("put/{teacherDTO}")]
        [HttpPut]
        public void Put(teacherDTO teacherDTO)
        {
            teacherBL teacherListBL = new teacherBL();
            teacherListBL.UpdateTeachers(teacherDTO);
        }

        // DELETE: api/Teacher/5
        [Route("delete/{teacherid}")]
        [HttpDelete]
        public void Delete(string teacherid)
        {
            teacherBL teacherListBL = new teacherBL();
            teacherListBL.DeleteTeachers(teacherid);
        }
    }
}
