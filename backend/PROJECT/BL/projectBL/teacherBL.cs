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
   public class teacherBL
    {
        IMapper iMapper;
        public teacherBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ObjectMappingProfile>();
            });
            iMapper = config.CreateMapper();
        }
        public List<teacherDTO> GetTeachersByCodeInstatition(int id)
        {
            List<teachers> teachersList = teacherDAL.GetTeachersByCodeInstation(id);
            if (teachersList == null)
                return new List<teacherDTO>();
            List<teacherDTO> teacherDTOList = new List<teacherDTO>();
            teacherDTOList.ForEach(x =>
            {
                teacherDTO t = new teacherDTO();
                t.code_instation = x.code_instation;
                t.id = x.id;
                t.NAME = x.NAME;
                t.telefone= x.telefone;
                teacherDTOList.Add(t);
            });
            return teacherDTOList;
        }
        public List<teacherDTO> GetAllTeachers()
        {
            List<teachers> teachersList =teacherDAL.GetAllTeachers();
            if (teachersList == null)
                return new List<teacherDTO>();
            List<teacherDTO> teachersListDTO = new List<teacherDTO>();
            teachersList.ForEach(x =>
            {
                teacherDTO t = new teacherDTO();
                t.id = x.id;
                t.NAME = x.NAME;
                t.telefone = x.telefone;
                t.code_instation = x.code_instation;
                teachersListDTO.Add(t);
            });
            return teachersListDTO;
        }

        public string AddTeachers(teacherDTO teacher)
        {
            var teacherMapper = iMapper.Map<teacherDTO, teachers>(teacher);
            return teacherDAL.AddTeachers(teacherMapper);
            
        }
        //עדכון 

        public void UpdateTeachers(teacherDTO teacher)
        {
            var teacherMapper = iMapper.Map<teacherDTO, teachers>(teacher);
            teacherDAL.UpdateTeachers(teacherMapper);
        }
        //מחיקה
        public void DeleteTeachers(string id)
        {
            teacherDAL.DeleteTeachers(id);
        }
    }
}
