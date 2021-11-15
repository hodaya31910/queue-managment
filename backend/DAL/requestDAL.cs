using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DAL
{
  public class requestDAL
    {
        public static List<request> GetRequestByCodeInstation(int id)
        {
            using (var context = new PARENTSEntities())
            {
                List<request> requestList = context.request.Where(r => r.parents.instation.code == id).ToList();
                   //List <request> requestList = (from r in context.request
                   //                          from p in context.parents
                   //                          from i in context.instation
                   //                          where r.id_parent == p.id && p.code_instation == i.code && i.code == id 
                   //                          select new request { from_hour = r.from_hour, code = r.code,  id_parent= r.id_parent, to_hour= r.to_hour}).ToList();
                return requestList;
            }
        }
        public static List<request> GetAllRequest()
        {
            using (var context = new PARENTSEntities())
            {
                List<request> RequestList = context.request.ToList();
                return RequestList;
            }
        }
        //הוספת 
        public static int AddRequest(request request)
        {
            using (var context = new PARENTSEntities())
            {
                request r = context.request.Add(request);
                context.SaveChanges();
                return r.code;
            }
        }
       // בודק האם הבקשה של האמא אפשרית
        public static Boolean check(request t)
        {
            using (var context = new PARENTSEntities())
            {
                int c = 0;
                 c = context.request.Where(r => r.id_parent == t.id_parent).Count();
                if (c == 0)
                {
                    TimeSpan h = new TimeSpan(t.from_hour.Hours + 1, t.from_hour.Minutes, 00);
                    AddRequest(t);
                    return true;
                }
                else
                    UpdateRequest(t);
                return false; 
            }
          
        }

        //עדכון
        public static void UpdateRequest(request request)
        {
            using (var context = new PARENTSEntities())
            {
                request r = context.request.Where(x => x.code == request.code).FirstOrDefault();
                if (r != null)
                {
                    r.code = request.code;
                    r.id_parent = request.id_parent;
                    r.from_hour = request.from_hour;
                    r.to_hour= request.to_hour;
                   
                    context.SaveChanges();
                }
            }
        }
        // מחיקה   

        public static void DeleteRequest(string id)
        {
            using (var context = new PARENTSEntities())
            {
                request request = context.request.Where(p => p.code.Equals(id)).FirstOrDefault();
                context.request.Remove(request);
                context.SaveChanges();

            }
        }
    }
}
