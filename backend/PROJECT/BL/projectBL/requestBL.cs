using AutoMapper;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
  public  class requestBL
    {
        IMapper iMapper;
        public requestBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ObjectMappingProfile>();
            });
            iMapper = config.CreateMapper();
        }
        public List<requestDTO> GetRequestByCodeInstatition(int id)
        {
            List<request> requestList = requestDAL.GetRequestByCodeInstation(id);
            if (requestList == null)
                return new List<requestDTO>();
            List<requestDTO> requestDTOList = new List<requestDTO>();
            requestDTOList.ForEach(x =>
            {
                requestDTO r = new requestDTO();
                r.code = x.code;
                r.from_hour = x.from_hour;
                r.id_parent = x.id_parent;
                r.to_hour = x.to_hour;
                requestDTOList.Add(r);
            });
            return requestDTOList;
        }
        public List<requestDTO> GetAllRequest()
        {
            List<request> requestList = requestDAL.GetAllRequest();
            if (requestList == null)
                return new List<requestDTO>();
            List<requestDTO> requestListDTO = new List<requestDTO>();
            requestList.ForEach(x =>
            {
                requestDTO r = new requestDTO();
                r.code = x.code;
                r.from_hour = x.from_hour;
                r.id_parent = x.id_parent;
                r.to_hour = x.to_hour;
                requestListDTO.Add(r);
            });
            return requestListDTO;
        }

        public int AddRequest(requestDTO request)
        {
            var requestMapper = iMapper.Map<requestDTO, request>(request);
            return requestDAL.AddRequest(requestMapper);
           
        }
        //עדכון 

        public void UpdateRequest(requestDTO request)
        {
            var requestMapper = iMapper.Map<requestDTO, request>(request);
            requestDAL.UpdateRequest(requestMapper);
        }
        //מחיקה
        public void DeleteRequest(string id)
        {
            requestDAL.DeleteRequest(id);
        }
        //מחזירה אמהות שלא משובצות והגישו בקשה
        //public List<parents> get_parents_not_request(int id_parent)
        //{
        //    var v = from p in global.PARENTS.parents
        //            where p.id != id_parent
        //            select new parents { id = p.id, last_name = p.last_name, first_name = p.first_name };

        //    return v.ToList();
        //}
        //פונקציה המקבלת אמא שלא משובצת ומחזירה האם יכולה להשתבץ בשעה שביקשה
        //public Boolean canToScheduleInHouer(parents parents)
        //{

        //  var q = (from s in global.PARENTS.scheduling
        //             from st in global.PARENTS.studens
        //             from p in global.PARENTS.parents
        //             where this.id_parent == p.id && p.id == st.id_parent && st.id == s.id_student && s.hour_ >= this.from_hour && s.hour_ <= this.to_hour
        //             select new parents { id = p.id, last_name = p.last_name, first_name = p.first_name }
        //             ).Count();
        //    if (q < 8)
        //        return true;
        //    return false;
        //}
        //מחזירה רשימה של כל האמהות שלא משובצות ויכולות להשתבץ בשעה שביקשו
        //public List<parents> AllCanToScheduleInHouer()
        //{
        //    List<parents> parents;
        //    parents = this.get_parents_not_request();
        //    for (int i = 0; i < parents.Count(); i++)
        //    {
        //        if (this.canToScheduleInHouer(parents[i]) != true)
        //            parents.Remove(parents[i]); 
        //    }
        //    return parents;
        //}
    }
}
