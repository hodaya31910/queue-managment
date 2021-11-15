using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using AutoMapper;

namespace BL
{
  public  class perentsBL
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
                p.first_name = parentsList.last_name;
                p.id = parentsList.id;
                p.telefone = parentsList.telefone;
              
           
            return p;
        }
        public List<parentsDTO> GetParentByCodeInstatition(int id)
        {
            List<parents> parentsList = perentsDAL.GetParentByCodeInstation(id);
            if (parentsList == null)
                return new List<parentsDTO>();
            List<parentsDTO> parentsDTOList = new List<parentsDTO>();
            parentsDTOList.ForEach(x =>
            {
                parentsDTO p = new parentsDTO();
                p.code_instation = x.code_instation;
                p.email = x.email;
                p.first_name = p.last_name;
                p.id = x.id;
                p.telefone = x.telefone;
                parentsDTOList.Add(p);
            });
            return parentsDTOList;
        }
        //מחזירה לכל אמא את ילדיה
        public List<studens> get_Allkids(string id_p)
        {
            parents p = DAL.perentsDAL.GetAllParents().FirstOrDefault(pp => pp.id == id_p);
          return DAL.studensDAL.GetAllStudents().Where(s => s.id_parent == p.id).ToList();
            
        }
        //מחזירה לכל אמא את ילדיה
        public List<studentDTO> get_kids(string  id_p)
        {
            parents p = DAL.perentsDAL.GetAllParents().FirstOrDefault(pp => pp.id == id_p);
            List<studens> ls= DAL.studensDAL.GetAllStudents().Where(s => s.id_parent == p.id).ToList();
            List<studentDTO> studentListDTO = new List<studentDTO>();
            ls.ForEach(x =>
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
        //עדכון קטגוריה
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












        //מחזירה רשימה של הכתות של הבנות
        //public List<classes> get_classes()
        //{
        //    var q = from s in global.PARENTS.studens
        //            from c in global.PARENTS.classes
        //            where s.id_parent == this.id && s.code_class == c.code
        //            select new classes { @class = c.@class };
        //    return q.ToList();
        //}
        //מחזירה את הכיתה עם התור הקצר ביותר
        //public classes get_class(List<classes> list)
        //{
        //    int min = 100000, i_min = 0;
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        if (list[i].length() < min)
        //        {
        //            min = list[i].length();
        //            i_min = i;
        //        }
        //    }
        //    return list[i_min];
        //}
        //מחזירה את מספר הדקות שנותר לאמא לחכות
        //public int getTimeToWait()
        //{
        //    classes class1;
        //    List<classes> cl;
        //    cl = this.get_classes();
        //    class1 = this.get_class(cl);
        //    List<parents> v = (from s in global.PARENTS.scheduling
        //                       from t in global.PARENTS.studens
        //                       from p in global.PARENTS.parents
        //                       where this.id == t.id_parent && t.id == s.id_student && class1.code == t.code_class && s.hour_exit == null && s.hour_reach != null && p.id == t.id_parent
        //                       select new parents { id = p.id, last_name = p.last_name, first_name = p.first_name }).ToList();
        //    int i = 0;
        //    int count = 0;
        //    while (v[i].id != this.id)
        //    {
        //        count += 7;
        //    }
        //    return count;
        //}
    }
}
