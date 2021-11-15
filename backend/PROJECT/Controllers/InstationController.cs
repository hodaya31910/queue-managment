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
    [RoutePrefix("api/Instation")]
    public class InstationController : ApiController
    {
        [Route("chekIfIsInstatitions")]
        [HttpPost]

        public instationDTO chekIfIsInstatitions(instationDTO i)
        {
            instationBL instation = new instationBL();
            instationDTO instations = instation.check(i);
            return instations;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<instationDTO> GetAll()
        {
            instationBL instationBL = new instationBL();
            List<instationDTO> instation = instationBL.GetAllInstatition();
            return instation;
        }
        [Route("AddInstation")]
        [HttpPost]
        public void AddInstation(instationDTO instation)
        {
            instationBL instationListBL = new instationBL();
            instationListBL.AddInstation(instation);
        }
        [Route("AddInstation1")]
        [HttpPost]
        public void AddInstation1(List<instationDTO> file)
        {
            file.ForEach(f => { AddInstation(f); });

        }

        [Route("Post")]
        [HttpPost]
        public void Post(instationDTO instation)
        {
            instationBL instationBL = new instationBL();
            instationBL.AddInstation(instation);

        }

        // PUT: api/Instation/5
        [Route("put")]
        [HttpPut]
        public void Put(instationDTO instation)
        {
            instationBL instationBL = new instationBL();
            instationBL.Updateinstation(instation);
        }

        // DELETE: api/Instation/5
        [Route("delete/{instationId}")]
        [HttpDelete]
        public void Delete(int instationId)
        {
            instationBL instationBL = new instationBL();
            instationBL.DeleteInstation(instationId);
        }
    }
}
