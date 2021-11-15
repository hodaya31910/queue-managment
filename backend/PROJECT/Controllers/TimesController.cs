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
    [RoutePrefix("api/Times")]
    public class TimesController : ApiController
    {
       
        [Route("GetAll")]
        [HttpGet]
        public List<timesDTO> GetAll()
        {
            timesBL timesListBL = new timesBL();
            List<timesDTO> times = timesListBL.GetAllTimes();
            return times; 
        }
        [Route("GetTimesByCodeInstation/{id}")]
        [HttpGet]
        public List<timesDTO> GetTimesByCodeInstation(int id)
        {
            timesBL time = new timesBL();
            List<timesDTO> t = time.GetTimesByCodeInstatition(id);
            return t;
        }
        [Route("AddTime1")]
        [HttpPost]
        public void AddTime1(List<timesDTO> file)
        {
            file.ForEach(f => { Post(f); });

        }

        [Route("GetTimesForMother/{id}")]
        [HttpGet]
        public timesDTO GetTimesForMother(string id)
        {
            timesBL time = new timesBL();
            timesDTO t = time.GetTimeforMother(id);
            return t;
        }
        // POST: api/Times
        [Route("Post/{timesDTO}")]
        [HttpPost]
        public void Post(timesDTO timesDTO)
        {
            timesBL timesBL = new timesBL();
            timesBL.AddTimes(timesDTO);
        }

        // PUT: api/Times/5
        [Route("put/{timesDTO}")]
        [HttpPut]
        public void Put(timesDTO timesDTO)
        {
            timesBL timesBL = new timesBL();
            timesBL.UpdateTimes(timesDTO);
        }

        
        [Route("delete/{timesid}")]
        [HttpDelete]
        public void Delete(string timesid)
        {
            timesBL timesListBL = new timesBL();
            timesListBL.DeleteTimes(timesid);
        }
    }
}
