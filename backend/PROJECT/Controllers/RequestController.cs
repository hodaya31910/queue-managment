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
    [RoutePrefix("api/Request")]
    public class RequestController : ApiController
    {
        [Route("GetRequestByCodeInstation/{id}")]
        [HttpGet]

        public List<requestDTO> GetRequestByCodeInstation(int id)
        {
            requestBL requests = new requestBL();
            List<requestDTO> request = requests.GetRequestByCodeInstatition(id);
            return request;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<requestDTO> GetAll()
        {
            requestBL requestListBL = new requestBL();
            List<requestDTO> request = requestListBL.GetAllRequest();
            return request;
        }
        [Route("AddRequest1")]
        [HttpPost]
        public void AddRequest1(List<requestDTO> file)
        {
            file.ForEach(f => { Post(f); });

        }
        [Route("Post")]
        [HttpPost]
        public void Post(requestDTO request)
        {
            requestBL requestListBL = new requestBL();
            requestListBL.AddRequest(request);
        }
        [Route("check")]
        [HttpPost]
        public Boolean check(requestDTO request)
        {
            requestBL requestListBL = new requestBL();
          return  requestListBL.check(request);
        }
        [Route("Post1")]
        [HttpPost]
        public void Post1()
        {
            requestBL requestListBL = new requestBL();
             requestListBL.addRequestFromExcel();

        }
       
        [Route("put")]
        [HttpPut]
        public void Put(requestDTO requestDTO)
        {
            requestBL requestListBL = new requestBL();
            requestListBL.UpdateRequest(requestDTO);
        }

        [Route("delete/{requestID}")]
        [HttpDelete]
        public void Delete(string requestID)
        {
            requestBL requestListBL = new requestBL();
            requestListBL.DeleteRequest(requestID);
        }
    }
}
