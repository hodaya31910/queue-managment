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
   public class ClassesBL
    {
        IMapper iMapper;
        public ClassesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ObjectMappingProfile>();
            });
            iMapper = config.CreateMapper();
        }
        public List<ClassesDTO> GetClasesByCodeInstatition(int id)
        {
            List<classes> classesList = classeesDAL.GetClasesByCodeInstation(id);
            if (classesList == null)
                return new List<ClassesDTO>();
            List<ClassesDTO> ClassesDTOList = new List<ClassesDTO>();
            classesList.ForEach(x =>
            {
                ClassesDTO c = new ClassesDTO();
                c.code = x.code;
                c.id_teacher = x.id_teacher;
                c.class_ = x.class_;
                c.num_class = x.num_class;
                ClassesDTOList.Add(c);
            });
            return ClassesDTOList;
        }

        public List<ClassesDTO> GetAllClasses()
        {
            List<classes> classesList = classeesDAL.GetAllClasses();
            if (classesList == null)
                return new List<ClassesDTO>();
            List<ClassesDTO> classesListDTO = new List<ClassesDTO>();
            classesList.ForEach(x =>
            {
                ClassesDTO c = new ClassesDTO();
                c.class_ = x.class_;
                c.code = x.code;
                c.num_class = x.num_class;
                c.id_teacher = x.id_teacher;
                classesListDTO.Add(c);
            });
            return classesListDTO;
        }
       
        public int AddClasses(ClassesDTO x)
        {
            var ClassesMapper = iMapper.Map<ClassesDTO, classes>(x);
            return  classeesDAL.AddClass(ClassesMapper);
        }
        //עדכון 

        public void UpdateClasses(ClassesDTO clas)
        {
            var ClassesMapper = iMapper.Map<ClassesDTO, classes>(clas);
            classeesDAL.UpdateClass(ClassesMapper);
        }
        //מחיקה
        public void DeleteClasses(int id)
        {
            classeesDAL.DeleteClass(id);
        }
    }
}
