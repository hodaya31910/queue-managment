using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
  public  class timesDAL
    {
        public static List<times> GetAllTimes()
        {
            using (var context = new PARENTSEntities())
            {
                List<times> timesList = context.times.ToList();
                return timesList;
            }
        }
        public static List<times> GetTimesByCodeInstation(int id)
        {
            using (var context = new PARENTSEntities())
            {
                List<times> timesList = (from c in context.classes
                                         from t in context.teachers
                                         from i in context.instation
                                         from ti in context.times
                                         where ti.code_class==c.code && c.id_teacher == t.id && t.code_instation == i.code && i.code == id 
                                         select new times { code_class = ti.code_class, code = ti.code, from_hour = ti.from_hour, to_hour = ti.to_hour }).ToList();
                return timesList;
            }
        }
        //מקבל תז תלמיד
        public static  times GetTimesForMother(string id)
        {
            using (var context = new PARENTSEntities())
            {
              times times = context.studens.FirstOrDefault(j => j.id == id).classes.times.FirstOrDefault();


               
                return times;
            }
        }


        //הוספת 
        public static int AddTimes(times times)
        {
            using (var context = new PARENTSEntities())
            {
                times t = context.times.Add(times);
                context.SaveChanges();
                return t.code;
            }
        }
        //עדכון
        public static void UpdateTimes(times times)
        {
            using (var context = new PARENTSEntities())
            {
                times t = context.times.Where(x => x.code == times.code).FirstOrDefault();
                if (t != null)
                {
                    t.code = times.code;
                    t.code_class = times.code_class;
                    t.from_hour = times.from_hour;
                    t.to_hour = times.to_hour;
                    context.SaveChanges();
                }
            }
        }
        // מחיקה   

        public static void DeleteTimes(string code)
        {
            using (var context = new PARENTSEntities())
            {
                times times = context.times.Where(p => p.code.Equals(code)).FirstOrDefault();
                context.times.Remove(times);
                context.SaveChanges();

            }
        }

    }
}
