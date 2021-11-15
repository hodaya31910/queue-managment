using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using AutoMapper;
using System.IO;

namespace BL
{
    public class perentsBL
    {
        IMapper iMapper;
        public perentsBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ObjectMappingProfile>();
            });
            iMapper = config.CreateMapper();
        }
        public parentsDTO GetParentById(string id)
        {
            parents parentsList = perentsDAL.GetParentById(id);
            if (parentsList == null)
                return null;

            parentsDTO p = new parentsDTO();
            p.code_instation = parentsList.code_instation;
            p.email = parentsList.email;
            p.first_name = parentsList.first_name;
            p.id = parentsList.id;
            p.telefone = parentsList.telefone;
            p.last_name = parentsList.last_name;


            return p;
        }
        public List<parentsDTO> GetParentByCodeInstatition(int id)
        {
            List<parents> parentsList = perentsDAL.GetParentByCodeInstation(id);
            if (parentsList == null)
                return new List<parentsDTO>();
            List<parentsDTO> parentsDTOList = new List<parentsDTO>();
            parentsList.ForEach(x =>
            {
                parentsDTO p = new parentsDTO();
                p.code_instation = x.code_instation;
                p.email = x.email;
                p.first_name = x.first_name;
                p.last_name = x.last_name;
                p.id = x.id;
                p.telefone = x.telefone;
                parentsDTOList.Add(p);
            });
            return parentsDTOList;
        }
        public List<schedulingDTO> GetShibutsForParents(string id)
        {
            List<scheduling> parentsList = perentsDAL.GetShibutsForParents(id);
            if (parentsList == null)
                return new List<schedulingDTO>();
            List<schedulingDTO> parentsDTOList = new List<schedulingDTO>();
            parentsList.ForEach(x =>
            {
                schedulingDTO s = new schedulingDTO();
                s.code = x.code;
                s.code_class = x.code_class;
                s.hour_ = x.hour_;
                s.hour_enter = x.hour_enter;
                s.hour_exit = x.hour_exit;
                s.hour_reach = x.hour_reach;
                s.id_student = x.id_student;
                var v = new PARENTSEntities();
                s.nameStudent = v.studens.Where(o => o.id == s.id_student).Select(r=>r.first_name+" "+r.last_name).FirstOrDefault();
                parentsDTOList.Add(s);
            });
            return parentsDTOList;
        }
        //מחזירה לכל אמא את ילדיה
        public List<studens> get_Allkids(string id_p)
        {
            parents p = DAL.perentsDAL.GetAllParents().FirstOrDefault(pp => pp.id == id_p);
            return DAL.studensDAL.GetAllStudents().Where(s => s.id_parent == p.id).ToList();

        }
        public parents GetParentByIdStudent(string idS)
        {
            parents p = new parents();
          return p=   DAL.perentsDAL.GetParentByIdStudent(idS);
            

        }
       
        /// <summary>
        /// ///שליפת שעות לאמא
        /// </summary>
        /// 

        public  List<TimeSpan> getAllH(string idP)
        {
            List<studens> ls = DAL.studensDAL.GetAllStudents().Where(s => s.id_parent == idP).ToList();
            List<TimeSpan> lfromHour=new List<TimeSpan>();
            List<TimeSpan> ltohoure = new List<TimeSpan>();
            ls.ForEach(x =>
            {
                timesBL time = new timesBL();
                timesDTO g = time.GetTimeforMother(x.id);
                if (g.code_class != 0)
                {
lfromHour.Add(g.from_hour);
                ltohoure.Add(g.to_hour);
                }
                
            });
            List<TimeSpan> lt = new List<TimeSpan>();
            TimeSpan min = new TimeSpan(00, 00, 00);
            TimeSpan max = new TimeSpan(00, 00, 00);
            min = lfromHour.Min();
                max = ltohoure.Max();
    
                TimeSpan h = new TimeSpan(01, 00, 00);
                TimeSpan y1 = new TimeSpan();
                for (TimeSpan y = min; y <= max; y = y1)
                {
                    y1 = y;
                    y1 = y.Add(h);
                   lt.Add(y);
                }
            
            return lt;
            }
        public void addparentsFromExcel()
        {
            parentsDTO p = new parentsDTO();
            StreamReader read = new StreamReader(@"d:\parents.csv", Encoding.Default);
            string str = read.ReadToEnd();
            string[] arr = str.Split('\n');
            for (int i = 0; i < arr.Length; i++)
            {
                int j = 0;
                string[] arr1 = arr[i].Split(',');
                p.id = arr1[j++];
                p.first_name = arr1[j++];
                p.last_name = arr1[j++];
                p.telefone = arr1[j++];
                p.email = arr1[j++];
                p.code_instation = int.Parse(arr1[j++]);
                perentsBL pBL = new perentsBL();
                pBL.AddParents(p);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        public List<parentsDTO> GetAllParents()
        {
            List<parents> parentsList = perentsDAL.GetAllParents();
            if (parentsList == null)
                return new List<parentsDTO>();
            List<parentsDTO> parentsListDTO = new List<parentsDTO>();
            parentsList.ForEach(x =>
            {
                parentsDTO p = new parentsDTO();
                p.email = x.email;
                p.first_name = x.first_name;
                p.last_name = x.last_name;
                p.telefone = x.telefone;
                p.code_instation = x.code_instation;
                parentsListDTO.Add(p);
            });
            return parentsListDTO;
        }

        public string AddParents(parentsDTO p)
        {
            var ParentsMapper = iMapper.Map<parentsDTO, parents>(p);
            return perentsDAL.AddParents(ParentsMapper);
        }
        //עדכון 
        public void UpdateParents(parentsDTO parents)
        {
            var ParentsMapper = iMapper.Map<parentsDTO, parents>(parents);
            perentsDAL.UpdateParents(ParentsMapper);
        }
        //מחיקה
        public void DeleteParents(string id)
        {
            perentsDAL.DeleteParents(id);
        }


    }
}
