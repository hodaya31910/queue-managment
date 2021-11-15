using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
 public   class teacherDAL
    {
        public static List<teachers> GetTeachersByCodeInstation(int id)
        {
            using (var context = new PARENTSEntities())
            {
                List<teachers> teachersList = context.teachers.Where(x => x.code_instation == id).ToList();
                return teachersList;
            }
        }
        public static List<teachers> GetAllTeachers()
        {
            using (var context = new PARENTSEntities())
            {
                List<teachers> teachersList = context.teachers.ToList();
                return teachersList;
            }
        }
        //הוספת 
        public static string AddTeachers(teachers teachers)
        {
            using (var context = new PARENTSEntities())
            {
                teachers t = context.teachers.Add(teachers);
                context.SaveChanges();
                return t.id;
            }
        }
        //עדכון
        public static void UpdateTeachers(teachers teachers)
        {
            using (var context = new PARENTSEntities())
            {
                teachers t = context.teachers.Where(x => x.id == teachers.id).FirstOrDefault();
                if (t != null)
                {
                    t.code_instation = teachers.code_instation;
                    t.id = teachers.id;
                    t.NAME = teachers.NAME;
                    t.telefone = teachers.telefone;
                    context.SaveChanges();
                }
            }
        }
        // מחיקה   

        public static void DeleteTeachers(string id)
        {
            using (var context = new PARENTSEntities())
            {
                teachers teachers = context.teachers.Where(p => p.id.Equals(id)).FirstOrDefault();
                context.teachers.Remove(teachers);
                context.SaveChanges();

            }
        }

    }
}
