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
    [RoutePrefix("api/Scheduling")]
    public class SchedulingController : ApiController
    {
        // GET: api/Scheduling
        //שליפה
        [Route("GetAll")]
        [HttpGet]
        public List<schedulingDTO> GetAll()
        {
            schedulingBL schedulingListBL = new schedulingBL();
            List<schedulingDTO> scheduling = schedulingListBL.GetAllScheduling();
            return scheduling;
        }
        //קריאה מהקונטרולר לפונקצית השיבוץ
        [Route("GetShibutzForInstatition/{id}")]
        [HttpGet]

        public Boolean GetShibutzForInstatition(int id)
        {
            schedulingBL Shibutz = new schedulingBL();
            return Shibutz.ShibutzAndMark(id);
        }
        [Route("GetSchedulingByCodeInstation/{id}")]
        [HttpGet]
        public List<schedulingDTO> GetSchedulingByCodeInstation(int id)
        {
            schedulingBL scheduling = new schedulingBL();
            List<schedulingDTO> s = scheduling.GetSchedulingByCodeInstatition(id);
            return s;
        }
        [Route("GetSchedulingByCodeClass/{id}")]
        [HttpGet]
        public List<schedulingDTO> GetSchedulingByCodeClass(int id)
        {
            schedulingBL scheduling = new schedulingBL();
            List<schedulingDTO> s = scheduling.GetSchedulingByCodeClass(id);
            return s;
        }
        [Route("Post")]
        [HttpPost]
        // POST: api/Scheduling
        public void Post(schedulingDTO scheduling)
        {
            schedulingBL schedulingListBL = new schedulingBL();
            schedulingListBL.AddScheduling(scheduling);
        }

        // PUT: api/Scheduling/5
        [Route("put")]
        [HttpPut]
        public void Put(schedulingDTO scheduling)
        {
            schedulingBL schedulingListBL = new schedulingBL();
            schedulingListBL.UpdateScheduling(scheduling);
        }
        [Route("updateEnter/{id}")]
        [HttpGet]
        public List<schedulingDTO> updateEnter(string id)
        {
            schedulingBL schedulingListBL = new schedulingBL();
            return schedulingListBL.updateEnter(id);
        }
        //עדכון יציאה
        [Route("updateExit/{id}")]
        [HttpGet]
        public  List<schedulingDTO> updateExit(string id)
        {
            schedulingBL schedulingListBL = new schedulingBL();
            return schedulingListBL.updateExit(id);
        }
        [Route("FuncRealTime/{id}")]
        [HttpGet]
        public schedulingDTO FuncRealTime(string id)
        {
            schedulingBL schedulingListBL = new schedulingBL();
            return schedulingListBL.FuncRealTime(id);
        }
        // DELETE: api/Scheduling/5
        [Route("delete/{schedulingID}")]
        [HttpDelete]
        public void Delete(int schedulingID)
        {
            schedulingBL schedulingListBL = new schedulingBL();
            schedulingListBL.DeleteScheduling(schedulingID);
        }
        [Route("Getparentsthatwaittoclass/{code}")]
        [HttpGet]
        public List<schedulingDTO> Getparentsthatwaittoclass(int code)
        {
            schedulingBL scheduling = new schedulingBL();
            List<schedulingDTO> s = scheduling.Get_parents_that_wait_to_class(code);
            return s;
        }
        [Route("DeleteByCodeInstation/{codeinstation}")]
        [HttpDelete]
        public void DeleteByCodeInstation(int codeinstation)
        {
            schedulingBL schedulingListBL = new schedulingBL();
            schedulingListBL.DeleteSchedulingByCodeInstation(codeinstation);
        }
    }
}
