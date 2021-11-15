using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
 public class perentsDAL
    {
        public static parents GetParentById(string id)
        {
            using (var context = new PARENTSEntities())
            {
                parents parentList = context.parents.FirstOrDefault(x => x.id == id);
                return parentList;
            }
        }

        public static parents GetParentByIdStudent(string id)
        {
            using (var context = new PARENTSEntities())
            {
                studens student = context.studens.Where(s => s.id == id).FirstOrDefault();
                parents parentList = context.parents.Where(p => p.id == student.id_parent).FirstOrDefault();
                return parentList;
            }
        }
        public static List<scheduling> GetShibutsForParents(string id)
        {
            using (var context = new PARENTSEntities())
            {
             List<   scheduling > parentList = context.scheduling.Where(x => x.studens.id_parent.Equals( id)).ToList();
                //foreach (scheduling item in parentList)
                //{
                //    item.nam
                //}
                return parentList;
            }
        }
        public static List<parents> GetParentByCodeInstation(int id)
        {
            using (var context = new PARENTSEntities())
            {
                List<parents> parentList = context.parents.Where(x=>x.code_instation==id).ToList();
                return parentList;
            }
        }
        public static List<parents> GetAllParents()
        {

            using (var context = new PARENTSEntities())
            {
                List<parents> ParentsList = context.parents.ToList();
                return ParentsList;
            }
        }
        //הוספת 
        public static string AddParents(parents parents)
        {
            using (var context = new PARENTSEntities())
            {
                parents p = context.parents.Add(parents);
                context.SaveChanges();
                return p.id;
            }
        }
        //עדכון
        public static void UpdateParents(parents parents)
        {
            using (var context = new PARENTSEntities())
            {
                parents p = context.parents.Where(x => x.id == parents.id).FirstOrDefault();
                if (p != null)
                {
                    p.id = parents.id;
                    p.first_name = parents.first_name;
                    p.code_instation = parents.code_instation;
                    p.email = parents.email;
                    p.last_name = parents.last_name;
                    p.telefone = parents.telefone;
                    context.SaveChanges();
                }
            }
        }
        // מחיקה   

        public static void DeleteParents(string id)
        {
            using (var context = new PARENTSEntities())
            {
                parents parents = context.parents.Where(p => p.id.Equals(id)).FirstOrDefault();
                context.parents.Remove(parents);
                context.SaveChanges();

            }
        }
    }
}
