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
   public class timesBL
    {
        IMapper iMapper;
        public timesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ObjectMappingProfile>();
            });
            iMapper = config.CreateMapper();
        }
        public List<timesDTO> GetAllTimes()
        {
            List<times> timesList = timesDAL.GetAllTimes();
            if (timesList == null)
                return new List<timesDTO>();
            List<timesDTO> timesListDTO = new List<timesDTO>();
            timesList.ForEach(x =>
            {
                timesDTO t = new timesDTO();
                t.code = x.code;
                t.code_class = x.code_class;
                t.from_hour = x.from_hour;
                t.to_hour = x.to_hour;
                timesListDTO.Add(t);
            });
            return timesListDTO;
        }
          public List<timesDTO> GetTimesByCodeInstatition(int id)
        {
            List<times> timesList = timesDAL.GetTimesByCodeInstation(id);
            if (timesList == null)
                return new List<timesDTO>();
            List<timesDTO> timesListDTO = new List<timesDTO>();
            timesList.ForEach(x =>
            {
                timesDTO t = new timesDTO();
                t.code = x.code;
                t.code_class = x.code_class;
                t.from_hour = x.from_hour;
                t.to_hour = x.to_hour;
                timesListDTO.Add(t);
            });
            return timesListDTO;
        }
        public int AddTimes(timesDTO times)
        {
            var timeMapper = iMapper.Map<timesDTO, times>(times);
            return timesDAL.AddTimes(timeMapper);
        }
        //עדכון 

        public void UpdateTimes(timesDTO times)
        {
            var timeMapper = iMapper.Map<timesDTO, times>(times);
            timesDAL.UpdateTimes(timeMapper);
        }
        //מחיקה
        public void DeleteTimes(string id)
        {
            timesDAL.DeleteTimes(id);
        }
    }
}
