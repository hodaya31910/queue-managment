using BL;

using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;

namespace PROJECT.Controllers
{
    
    [RoutePrefix("api/Classes")]
    public class ClassesController : ApiController
    {
        public ClassesController()
        {
            Console.WriteLine("hi");
        }
        [Route("GetClasesByCodeInstation/{id}")]
        [HttpGet]

        public List<ClassesDTO> GetClasesByCodeInstation(int id)
        {
            ClassesBL classes = new ClassesBL();
            List<ClassesDTO> class_ = classes.GetClasesByCodeInstatition(id);
            return class_;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<ClassesDTO> GetAll()
        {
            ClassesBL ClassesListBL = new ClassesBL();
            List<ClassesDTO> Classes = ClassesListBL.GetAllClasses();
            return Classes;
        }

        // POST: api/Classes
        //הוספה
        [Route("AddClass")]
        [HttpPost]
        public void AddClass(ClassesDTO classes)
        {
            ClassesBL ClassesListBL = new ClassesBL();
            ClassesListBL.AddClasses(classes);
        }
        [Route("AddClass1")]
        [HttpPost]
        public  void AddClass1(List<ClassesDTO> file)
        {
            file.ForEach(f => { AddClass(f); });
            
        }

        // PUT: api/Classes/5
        [Route("put")]
        [HttpPut]
        public void Put(ClassesDTO ClassesDTO)
        {
            ClassesBL ClassBL = new ClassesBL();
            ClassBL.UpdateClasses(ClassesDTO);

        }

        // DELETE: api/Classes/5
        [Route("delete/{ClassesID}")]
        [HttpDelete]
        public void Delete(int ClassesID)
        {
            ClassesBL ClassBL = new ClassesBL();
            ClassBL.DeleteClasses(ClassesID);
        }
    }
}
