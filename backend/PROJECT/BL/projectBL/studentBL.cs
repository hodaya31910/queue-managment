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
   public class studentBL
    {
        IMapper iMapper;
        public studentBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ObjectMappingProfile>();
            });
            iMapper = config.CreateMapper();
        }
        public List<studentDTO> GetAllStudent()
        {
            List<studens> studentList = studensDAL.GetAllStudents();
            if (studentList == null)
                return new List<studentDTO>();
            List<studentDTO> studentListDTO = new List<studentDTO>();
            studentList.ForEach(x =>
            {
                studentDTO s = new studentDTO();
                s.id = x.id;
                s.code_class = x.code_class;
                s.id_parent = x.id_parent;
                s.first_name = x.first_name;
                s.last_name = x.last_name;
                studentListDTO.Add(s);
            });
            return studentListDTO;
        }
        public List<studentDTO> GetStudentsByCodeInstatition(int id)
        {
            List<studens> studentList = studensDAL.GetStudentsByCodeInstation(id);
            if (studentList == null)
                return new List<studentDTO>();
            List<studentDTO> studentListDTO = new List<studentDTO>();
            studentList.ForEach(x =>
            {
                studentDTO s = new studentDTO();
                s.id = x.id;
                s.code_class = x.code_class;
                s.id_parent = x.id_parent;
                s.first_name = x.first_name;
                s.last_name = x.last_name;
                studentListDTO.Add(s);
            });
            return studentListDTO;
        } 
        public string AddStudent(studentDTO student)
        {
            var studensMapper = iMapper.Map<studentDTO, studens>(student);
            return studensDAL.AddStudens(studensMapper);
        }
        //עדכון 

        public void UpdateStudens(studentDTO student)
        {
            var studensMapper = iMapper.Map<studentDTO, studens>(student);
             studensDAL.UpdateStudens(studensMapper);
           
        }
        //מחיקה
        public void DeleteStudent(string id)
        {
            studensDAL.DeleteStudens(id);
        }
    }
}
