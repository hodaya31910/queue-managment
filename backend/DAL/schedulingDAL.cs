using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
public class schedulingDAL
    {
        public static List<scheduling> GetAllScheduling()
        {

            using (var context = new PARENTSEntities())
            {
                List<scheduling> schedulingList = context.scheduling.ToList();
                return schedulingList;
            }
        }
      
        public static List<scheduling> GetSchedulingByCodeClass(int id)
        {
            using (var context = new PARENTSEntities())
            {
                List<scheduling> schedulingList = context.scheduling.Where(r => r.code_class == id).OrderBy(w => w.hour_).ToList();

                return schedulingList;
            }
        }
        public static List<scheduling> GetSchedulingByCodeInstation(int id)
        {
            using (var context = new PARENTSEntities())
            {
                List<scheduling> schedulingList = context.scheduling.Where(pp => pp.studens.parents.code_instation == id).ToList();
                return schedulingList;
            }
        }
        public static scheduling GetSchedulingByIdParent(string id)
        {
            using (var context = new PARENTSEntities())
            {

                List<studens> studens = new List<studens>();
                studens = studensDAL.GetKids(id);
               scheduling schedulingList = context.scheduling.Where(pp => pp.id_student.Equals(studens.First())).FirstOrDefault();
                return schedulingList;
            }
        }
        //הוספת 
        public static int AddScheduling(scheduling scheduling)
        {
            using (var context = new PARENTSEntities())
            {
                scheduling s = context.scheduling.Add(scheduling);
                context.SaveChanges();
                return s.code;
            }
        }
        //עדכון
        public static void UpdateScheduling(scheduling scheduling)
        {
            using (var context = new PARENTSEntities())
            {
                scheduling s = context.scheduling.Where(x => x.code == scheduling.code).FirstOrDefault();
                if (s!= null)
                {
                    s.code = scheduling.code;
                    s.code_class = scheduling.code_class;
                    s.hour_ = scheduling.hour_;
                    s.hour_enter = scheduling.hour_enter;
                    s.hour_exit = scheduling.hour_exit;
                    s.hour_reach = scheduling.hour_reach;
                    s.id_student = scheduling.id_student;
                    context.SaveChanges();
                }
            }
        }
        // מחיקה   

        public static void DeleteScheduling(int id)
        {
            using (var context = new PARENTSEntities())
            {
                scheduling scheduling = context.scheduling.Where(p => p.code==id).FirstOrDefault();
                context.scheduling.Remove(scheduling);
                context.SaveChanges();

            }
        }
        //עדכון כניסה
        public static List<scheduling> updateEnter(string id)
        {
            using (var context = new PARENTSEntities())
            {
               scheduling scheduling = context.scheduling.Where(a=>a.studens.id==id).FirstOrDefault();
                scheduling.hour_enter = DateTime.Now.TimeOfDay;
                UpdateScheduling(scheduling);
                context.SaveChanges();
                return GetParentsThatWaitToClass(scheduling.code_class);
            }
        }
        //עדכון יציאה
        public static List<scheduling> updateExit(string id)
        {
            using (var context = new PARENTSEntities())
            {
                scheduling schedulings = context.scheduling.Where(a => a.studens.id == id).FirstOrDefault();
                schedulings.hour_exit = DateTime.Now.TimeOfDay;
                UpdateScheduling(schedulings);
                context.SaveChanges();
                return GetParentsThatWaitToClass(schedulings.code_class);
            }
        }
        public static List<scheduling> GetParentsThatWaitToClass(int code)
        {
            List<scheduling> parentsList = new List<scheduling>();
            using (var context = new PARENTSEntities())
            {
                parentsList = context.scheduling.
                    Where(u => u.code_class == code && u.hour_reach!=null).ToList();

                return parentsList;
            }
        }
    }
}
