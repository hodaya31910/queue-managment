using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class studensDAL
    {
        public static List <studens> GetAllStudents()
        {
            using (var context = new PARENTSEntities())
            {
                List<studens> studensList = context.studens.ToList();
                return studensList;
            }
        }
        public static List<studens> GetKids(string id)
        {
            using (var context = new PARENTSEntities())
            {
                List<studens> studensList = context.studens.Where(s=>s.id_parent==id).ToList();
                return studensList;
            }
        }
        public static List<studens> GetStudentsByCodeInstation(int id)
        {
            List<studens> studensList = null;
            using (var context = new PARENTSEntities())
            {
                studensList = context.studens.Where(o => o.parents.code_instation == id).ToList();
                            return studensList;              
            }
        }
       
        //הוספת 
        public static string AddStudens(studens studens)
        {
            using (var context = new PARENTSEntities())
            {
                studens s = context.studens.Add(studens);
                context.SaveChanges();
                return s.id;
            }
        }
        //עדכון
        public static void UpdateStudens(studens studens)
        {
            using (var context = new PARENTSEntities())
            {
                studens s = context.studens.Where(x => x.id == studens.id).FirstOrDefault();
                if (s!= null)
                {
                    s.id = studens.id;
                    s.last_name = studens.last_name;
                    s.first_name = studens.first_name;
                    s.code_class = studens.code_class;
                    s.id_parent = studens.id_parent;
                    context.SaveChanges();
                }
            }
        }
        // מחיקה   

        public static void DeleteStudens(string id)
        {
            using (var context = new PARENTSEntities())
            {
                studens studens = context.studens.Where(p => p.id.Equals(id)).FirstOrDefault();
                context.studens.Remove(studens);
                context.SaveChanges();

            }
        }
  
    }
}
