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
    [RoutePrefix("api/Perents")]
    public class PerentsController : ApiController
    {
        [Route("GetParentsByCodeInstation/{id}")]
        [HttpGet]

        public List<parentsDTO> GetParentsByCodeInstation(int id)
        {
            perentsBL parents = new perentsBL();
            List<parentsDTO> parent = parents.GetParentByCodeInstatition(id);
            return parent;
        }
        [Route("GetShibutsForParents/{id}")]
        [HttpGet]

        public List<schedulingDTO> GetShibutsForParents(string id)
        {
            perentsBL parents = new perentsBL();
            List<schedulingDTO> parent = parents.GetShibutsForParents(id);
            return parent;
        }
        [Route("GetParentsById/{id}")]
        [HttpGet]

        public parentsDTO GetParentsById(string id)
        {
            perentsBL parents = new perentsBL();
           parentsDTO parent = parents.GetParentById(id);
            return parent;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<parentsDTO> GetAll()
        {
            perentsBL perentsListBL = new perentsBL();
            List<parentsDTO> perents = perentsListBL.GetAllParents();
            return perents;
        }
        // POST: api/Perents
        //הוספת הורה
        [Route("Post")]
        [HttpPost]
        public void Post(parentsDTO parentsDTO)
        {
            perentsBL perentBL = new perentsBL();
            perentBL.AddParents(parentsDTO);
        }
        [Route("Post1")]
        [HttpPost]
        public void Post1()
        {
          //  addTable a = new addTable();
         //   a.addparents();

        }
        // PUT: api/Perents/5
        [Route("put")]
        [HttpPut]
        public void Put(parentsDTO parentsDTO)
        {
            perentsBL perentBL = new perentsBL();
            perentBL.UpdateParents(parentsDTO);
        }

        // DELETE: api/Perents/5
        [Route("delete/{parentsID}")]
        [HttpDelete]
        public void Delete(string parentsID)
        {
            perentsBL perentBL = new perentsBL();
            perentBL.DeleteParents(parentsID);
        }
        [Route("AddParents1")]
        [HttpPost]
        public void AddParents1(List<parentsDTO> file)
        {
            file.ForEach(f => { Post(f); });

        }
        //שליפת שעות לאמא
        [Route("getHours/{tz}")]
        [HttpGet]
        public List<TimeSpan> getHours(string tz)
        {
            perentsBL perentBL = new perentsBL();
            return perentBL.getAllH(tz);
        }
    }
}
