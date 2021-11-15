using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.projectBL
{
    class Shibutz
    {
        public List<studens> lst_studens;
        public List<request> lst_request;
        private List<scheduling> lstscheduling;
        //פונקציות שיוצרות שיבוצים של כל האמהות על פי הבקשות שלהן
        //כל פונקציה תמיין את רשימת האמהות לפי פרמטר אחר
        // וכן תבצע חלק מהשיבוצים באמצעות הגרלה
        //כמובן שכל הפונקציות מתיחסות גם לשמירה על רצף קטן ככל האפשר בין אחיות

        //פונקצית שיבוץלכל התלמידות 
        bool func_shibutz()
        {
            //if (lst_studens.Count == 0)
            //    return true ;
            while (lst_studens.Count != 0)
            {
                studens s = lst_studens[0];
                lst_studens.RemoveAt(0);
                if (Shibutz_stud(s))
                    return func_shibutz();
                lstscheduling.RemoveAll(ss => ss.id_student == s.id);
                lst_studens.Add(s);
            }
            return false;
        }

        //פונקצית שיבוץ לתלמידה בודדת
        // אם יכול לשבץ מוסיף לאוסף השיבוץ ומחזיר אמת
        //אחרת מחזיר שקר
        bool Shibutz_stud(studens s)
        {
            //נשלוף את הבקשות של האמא
            //את השיבוצים של האחיות של התלמידה
            // את השעות הפנויות בכיתה שלה
            //וננסה לשבץ באופן שמתאים לכל הקריטריונים
            request r = s.parents.request.FirstOrDefault();
            if (r == null)
            {
                r = new request();
                r.id_parent = s.id_parent;
                r.from_hour = s.classes.times.FirstOrDefault().from_hour;
                r.to_hour = s.classes.times.FirstOrDefault().to_hour;
            }
            List<scheduling> lstSis = lstscheduling.Where(ss => ss.studens.id_parent == s.id_parent).ToList();
            List<scheduling> lstClass = lstscheduling.Where(ss => ss.studens.code_class == s.code_class)
                .OrderBy(ss => ss.hour_).ToList();
            //לולאה שעוברת על כל השעות שבטווח הבקשה של האמא
            //ומנסה לשבץ
            //אם הצליחה מחזירה אמת
            for (TimeSpan h = r.from_hour; h < r.to_hour; h.Add(new TimeSpan(0, 15, 0)))
            {
                if (lstSis == null || lstSis.Where(ss => ss.hour_.Equals(h)).FirstOrDefault() == null)
                {
                    if (lstClass.Where(ss => ss.hour_.Equals(h)).ToList().Count < 2)
                    {    //שיבוץ בפועל 
                        //והחזרת אמת
                        scheduling x = new scheduling();
                        x.code_class = s.code_class;
                        x.hour_ = h;
                        x.id_student = s.id;
                        lstscheduling.Add(x);
                        return true;
                    }
                }
            }
            //אם לא הצליחה
            //לולאה שעוברת על כל הטווח של הכיתה
            //ומנסה לשבץ
            //!!!!!!!!!!!!!!!!!!!!!!!!!!חובה לשנות שישלוף את הזמנים של הכיתה
            times tt = new times();//global.PARENTS.times.Where(t => t.code_class == s.code_class);
            for (TimeSpan h = tt.from_hour; h < tt.to_hour; h.Add(new TimeSpan(0, 15, 0)))
            {
                if (lstClass.Where(ss => ss.hour_.Equals(h)).ToList().Count < 2)
                {
                    if (lstSis == null || lstSis.Where(ss => ss.hour_.Equals(h)).FirstOrDefault() == null)
                    {
                        {    //שיבוץ בפועל 
                             //והחזרת אמת
                            scheduling x = new scheduling();
                            x.code_class = s.code_class;
                            x.hour_ = h;
                            x.id_student = s.id;
                            lstscheduling.Add(x);
                            return true;
                        }
                    }


                }
            }
            //אם לא הצליחה יחזיר שקר
            return false;
        }
        //מיון לפי שם פרטי
        public void SortByName()
        {
            var q = lstscheduling.OrderBy(x => x.studens.first_name).ToList();
        }
        //מיון לפי כיתות
        public void SortByClasses()
        {
            var q = lstscheduling.OrderBy(x => x.code_class).ToList();
        }
        //מיון לפי מספר הבנות
        public void SortByNumGirles()
        {
            var q = lstscheduling.OrderBy(x => x.studens.first_name).ToList();
        }
        //פונקצית הערכה
        //מקבלת אוסף -ליסט- של השיבוץ של כל בית הספר
        //ומחזירה ציון לשיבוץ
        //לפי
        //1. פרק הזמן שאמא לכמה בנות שוהה בבית הספר
        //2. רמת סטיה מבקשות של אמהות
        private double mark()
        {
            //!!!!!!!!!!!!!!!!!!!!לשלוף את כל האמהות מהמסד נתונים
            List<parents> lst_par = perentsDAL.GetAllParents();
            double avg = 0, cntExp = 0;
            TimeSpan min = new TimeSpan(00, 0, 0);
            TimeSpan max = new TimeSpan(12, 0, 0);
            TimeSpan temp;
            perentsBL pars = new perentsBL();
            requestBL r = new requestBL();
            //1. פרק הזמן שאמא לכמה בנות שוהה בבית הספר
            foreach (parents p in lst_par)
            {
                List<studens> lst_k = pars.get_Allkids(p.id);
                for (int i = 0; i < lst_k.Count(); i++)
                {
                    //השוואה של timespan
                    temp = lstscheduling.Where(x => x.id_student == lst_k[i].id).FirstOrDefault().hour_;
                    if (temp > (max))
                        max = temp;
                    if (temp < min)
                        min = temp;
                }
                avg = Convert.ToInt32(max - min) / lst_k.Count();
                if (avg > 30)
                {
                    cntExp++;
                }
            }
            //2. רמת סטיה מבקשות של אמהות
            foreach (parents p in lst_par)
            {
               //List<request> lst_r =r;
            }
            return 0;
        }



        //לשקול בהמשך אם כדאי להוסיף פונקציה לשיפור של שיבוץ קיים

    }
}
