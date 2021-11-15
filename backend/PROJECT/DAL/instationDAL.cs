using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
public class instationDAL
    {
        public static instation GetInstationById(int id)
        {
            using (var context = new PARENTSEntities1())
            {
                instation instationList = context.instation.FirstOrDefault(x => x.code == id);
                return instationList;
            }
        }
        public static List<instation> GetAllInstation()
        {

            using (var context = new PARENTSEntities1())
            {
                List<instation> instationList = context.instation.ToList();
                return instationList;
            }
        }
        //הוספת 
        public static int AddInstation(instation instation)
        {
            using (var context = new PARENTSEntities1())
            {
                instation i = context.instation.Add(instation);
                context.SaveChanges();
                return i.code;
            }
        }
        //עדכון
        public static void UpdateInstation(instation instation)
        {
            using (var context = new PARENTSEntities1())
            {
                instation pro = context.instation.Where(p => p.code == instation.code).FirstOrDefault();
                if (pro != null)
                {
                    pro.code = instation.code;
                    pro.email = instation.email;
                    pro.NAME = instation.NAME;
                    pro.password = instation.password;
                    context.SaveChanges();
                }
            }
        }
        // מחיקה   

        public static void DeleteInstation(int code)
        {
            using (var context = new PARENTSEntities1())
            {
                instation instation = context.instation.Where(p => p.code == code).FirstOrDefault();
                context.instation.Remove(instation);
                context.SaveChanges();

            }
        }
    }
}
