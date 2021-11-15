using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class classeesDAL
    {
        public static List<classes> GetClasesByCodeInstation(int id)
        {
            using (var context = new PARENTSEntities1())
            {
                var c1 = context.classes.ToList();
                var t1= context.teachers.ToList();
          var i1    = context.instation.ToList();
                List<classes> classesList = (from c in c1
                                             from t in t1
                                             from i in i1
                                             where c.id_teacher == t.id && t.code_instation == i.code && i.code == id
                                             select new classes {class_ =c.class_, code = c.code, id_teacher = c.id_teacher, num_class = c.num_class }).ToList();
               
                return classesList;
            }
        }
        public static List<classes> GetAllClasses()
        {

            using (var context = new   PARENTSEntities1())
            {
                List<classes> classesList = context.classes.ToList();
                return classesList;
            }
        }
        //הוספת 
        public static int AddClass(classes classes)
        {
            using (var context = new PARENTSEntities1())
            {
                context.Database.ExecuteSqlCommand("set identity_insert [dbo].[classes] on");

                classes c = context.classes.Add(classes);
                context.SaveChanges();
                context.Database.ExecuteSqlCommand("set identity_insert [dbo].[classes] of");

                return c.code;
            }
        }
        //עדכון
        public static void UpdateClass(classes classes)
        {
            using (var context = new PARENTSEntities1())
            {
                classes pro = context.classes.Where(p => p.code == classes.code).FirstOrDefault();
                if (pro != null)
                {
                    pro.code = classes.code;
                    pro.num_class = classes.num_class;
                    pro.id_teacher = classes.id_teacher;
                    context.SaveChanges();
                }
            }
        }
        // מחיקה   

        public static void DeleteClass(int code)
        {
            using (var context = new PARENTSEntities1())
            {
                classes classes = context.classes.Where(p => p.code == code).FirstOrDefault();
                context.classes.Remove(classes);
                context.SaveChanges();

            }
        }
    }
}
//מחזיר אמהות שלא משובצות בכיתה זו
//public List<parents> parents_not_in_class ()
//{
//    var v = from c in global.PARENTS.scheduling
//            from s in global.PARENTS.studens
//            from p in global.PARENTS.parents
//            from r in global.PARENTS.request
//            where c.id_student != s.id && s.code_class == this.code && c.code_class==s.code_class && p.id == s.id_parent  && r.id_parent==p.id      
//            select new parents {id= p.id,last_name= p.last_name,first_name= p.first_name };
//    return v.ToList();
//}
//  מחזיר אמהות שלא הגישו בקשה בכיתה זו
//public List<parents> parents_not_in_request()
//{
//    var v =from s in global.PARENTS.studens
//            from p in global.PARENTS.parents
//            from r in global.PARENTS.request
//            where  s.code_class == this.code &&  p.id == s.id_parent && r.id_parent != p.id
//            select new parents { id = p.id, last_name = p.last_name, first_name = p.first_name };

//    return v.ToList();
//}
//פונקציה המחזירה את אורך התור בכיתה
//public int length()
//{

//    var v = from s in global.PARENTS.studens
//            from p in global.PARENTS.scheduling
//            where s.code_class == this.code && p.id_student == s.id && p.code_class == this.code && p.hour_exit == null &&p.hour_reach!=null
//            group this by s.code_class;
//          //  into newlist
//            //select new { newlist.Key, count = newlist.Count() };
//    return v.Count();

//}