using AutoMapper;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
        //הוספת תלמידים מטבלת אקסל
        public void AddStudentfromEcsel()
        {
            studentDTO s = new studentDTO();
            StreamReader read = new StreamReader(@"F:\פרויקט הודיה ואפרת החדש\PROJECT\upload\students\table.csv", Encoding.Default);
            string str = read.ReadToEnd();
            string[] arr = str.Split('\n');
            for (int i = 0; i < arr.Length; i++)
            {
                int j = 0;
                string[] arr1 = arr[i].Split(',');
                s.id = arr1[j++];
                s.first_name = arr1[j++];
                s.last_name = arr1[j++];
                s.id_parent = arr1[j++];
                s.code_class = int.Parse(arr1[j++]);
                studentBL sBL = new studentBL();
                sBL.AddStudent(s);
                Console.WriteLine();
            }
            Console.ReadLine();
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
        // מחזירה לכל אמא את ילידה   
        public List<studentDTO> get_kids(string id_p)
        {
            List<studens> ls = studensDAL.GetKids(id_p);
            if (ls == null)
                return new List<studentDTO>();
            List<studentDTO> studentListDTO = new List<studentDTO>();
            ls.ForEach(x =>
            {
                studentDTO s = new studentDTO();
                s.id = x.id;
                s.code_class = x.code_class;
                s.id_parent = x.id_parent;
                s.first_name = x.first_name;
                s.last_name = x.last_name;
                timesBL time = new timesBL();
                studentListDTO.Add(s);
            });
            return studentListDTO;

        }
    }
}
