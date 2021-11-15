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
  public  class schedulingBL
    {
        IMapper iMapper;
        public schedulingBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ObjectMappingProfile>();
            });
            iMapper = config.CreateMapper();
        }
        public List<schedulingDTO> GetAllScheduling()
        {
            List<scheduling> schedulingList = schedulingDAL.GetAllScheduling();
            if (schedulingList == null)
                return new List<schedulingDTO>();
            List<schedulingDTO> schedulingListDTO = new List<schedulingDTO>();
            schedulingList.ForEach(x =>
            {
                schedulingDTO s = new schedulingDTO();
                s.code = x.code;
                s.code_class = x.code_class;
                s.hour_= x.hour_;
                s.id_student= x.id_student;
                s.hour_enter = x.hour_enter;
                s.hour_exit = x.hour_exit;
                s.hour_reach = x.hour_reach;
                schedulingListDTO.Add(s);
            });
            return schedulingListDTO;
        }
        public List<schedulingDTO> GetSchedulingByCodeInstatition(int id)
        {
            List<scheduling> schedulingList = schedulingDAL.GetSchedulingByCodeInstation(id);
            if (schedulingList == null)
                return new List<schedulingDTO>();
            List<schedulingDTO> schedulingListDTO = new List<schedulingDTO>();
            schedulingList.ForEach(x =>
            {
                schedulingDTO s = new schedulingDTO();
               s.code = x.code;
                s.code_class = x.code_class;
                s.hour_ = x.hour_;
               s.hour_enter= x.hour_enter;
                s.hour_exit = x.hour_exit;
                s.hour_reach = x.hour_reach;
                s.id_student = x.id_student;
                schedulingListDTO.Add(s);
            });
            return schedulingListDTO;
        }
        public int AddScheduling(schedulingDTO scheduling)
        {
            var schedulingMapper = iMapper.Map<schedulingDTO, scheduling>(scheduling);

            return schedulingDAL.AddScheduling(schedulingMapper);
        }
        //עדכון 

        public void UpdateScheduling(schedulingDTO scheduling)
        {
            var schedulingMapper = iMapper.Map<schedulingDTO, scheduling>(scheduling);
             schedulingDAL.UpdateScheduling(schedulingMapper);
        }
        //מחיקה
        public void DeleteScheduling(string id)
        {
            schedulingDAL.DeleteScheduling(id);
        }
    }
}
