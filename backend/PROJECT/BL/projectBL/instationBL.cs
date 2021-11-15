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
   public class instationBL
    {
        IMapper iMapper;
        public instationBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ObjectMappingProfile>();
            });
            iMapper = config.CreateMapper();
        }
        public instationDTO GetInstatitionById(int id)
        {
            instation instationList = instationDAL.GetInstationById(id);
            if (instationList == null)
                return new instationDTO();
            instationDTO instationDTOList = new instationDTO();
            instationDTO i = new instationDTO();
                i.code= instationDTOList.code;
                i.email = instationDTOList.email;
                i.NAME = instationDTOList.NAME;
                i.password = instationDTOList.password;
                instationDTOList=i;
            return instationDTOList;
        }
        public List<instationDTO> GetAllInstatition()
            {
                List<instation> instationList = instationDAL.GetAllInstation();
                if (instationList == null)
                    return new List<instationDTO>();
                List<instationDTO> instationListDTO = new List<instationDTO>();
            instationList.ForEach(x =>
                {
                    instationDTO i = new instationDTO();
                    i.code = x.code;
                    i.email = x.email;
                    i.NAME = x.NAME;
                    i.password = x.password;
                    instationListDTO.Add(i);
                });
                return instationListDTO;
            }
            //הוספת קטגוריה
            public int AddInstation(instationDTO ins)
            {
            var InstationMapper = iMapper.Map<instationDTO, instation>(ins);
            return instationDAL.AddInstation(InstationMapper);
        }
            //עדכון קטגוריה
            public void Updateinstation(instationDTO ins)
            {
            var InstationMapper = iMapper.Map<instationDTO, instation>(ins);
            instationDAL.UpdateInstation(InstationMapper);
        }
            //מחיקה
            public void DeleteInstation(int id)
            {
            instationDAL.DeleteInstation(id);
            }
        //פונקציה המקבלת שם משתמש וסיסמא של מוסד ובודקת האם המוסד קיים במערכת
        //אם לא מוצאת תחזיר null
        public instationDTO check(instationDTO ifromClient)
        {
            List<instation> instationList = instationDAL.GetAllInstation();
            instation id = instationList.FirstOrDefault(k => k.NAME.Equals(ifromClient.NAME) && k.password.Equals(ifromClient.password));
            if (id == null)
                return null;
            
            instationDTO i = new instationDTO();
            i.code = id.code;
            i.email = id.email;
            i.NAME = id.NAME;
            i.password = id.password;
            
            return i;
        }
        }
}
