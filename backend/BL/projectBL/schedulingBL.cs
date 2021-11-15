using AutoMapper;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using System.Net.Mail;
using System.Net;

namespace BL
{
    public class schedulingBL
    {
        //public static void SendEmail()
        //{
        //    string smtpAddress = "smtp.gmail.com";
        //     int portNumber = 587;
        //     bool enableSSL = true;
        //     string emailFromAddress = "hodaya31910@gmail.com"; //Sender Email Address  
        //     string password = "0583210657hodaya"; //Sender Password  
        //     string emailToAddress = "pamelaartzi@gmail.com"; //Receiver Email Address  
        //     string subject = "Hello";
        //     string body = "Hello, This is Email sending test using gmail.";
        //        using (MailMessage mail = new MailMessage())
        //    {
        //        mail.From = new MailAddress(emailFromAddress);
        //        mail.To.Add(emailToAddress);
        //        mail.Subject = subject;
        //        mail.Body = body;
        //        mail.IsBodyHtml = true;
        //        //mail.Attachments.Add(new Attachment("D:\\TestFile.txt"));//--Uncomment this to send any attachment  
        //        using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
        //        {
        //            smtp.Credentials = new NetworkCredential(emailFromAddress, password);
        //            smtp.EnableSsl = enableSSL;
        //            smtp.Send(mail);
        //        }
        //    }
        //}
            IMapper iMapper;
        public schedulingBL()
        {
            //SendEmail();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ObjectMappingProfile>();
            });
            iMapper = config.CreateMapper();

        }
        //פונקציה המחזירה את כל השיבוצים של כל המוסדות
        public List<schedulingDTO> GetAllScheduling()
        {
            List<scheduling> schedulingList = schedulingDAL.GetAllScheduling();
            if (schedulingList == null)
                return new List<schedulingDTO>();
            List<schedulingDTO> schedulingListDTO = new List<schedulingDTO>();
            schedulingList.ForEach(x =>
            {
                schedulingDTO s = new schedulingDTO();
                s.code = x.code;
                s.code_class = x.code_class;
                s.hour_ = x.hour_;
                s.id_student = x.id_student;
                s.hour_enter = x.hour_enter;
                s.disableEnter = false;
                s.disableExit = false;
                s.hour_exit = x.hour_exit;
                s.hour_reach = x.hour_reach;
                schedulingListDTO.Add(s);
            });
            return schedulingListDTO;
        }
        public List<schedulingDTO> Get_parents_that_wait_to_class(int id)
        {
            var v = new PARENTSEntities();
            List<scheduling> schedulingList = schedulingDAL.GetParentsThatWaitToClass(id);
            if (schedulingList == null)
                return new List<schedulingDTO>();
            List<schedulingDTO> schedulingDTOList = new List<schedulingDTO>();
            schedulingList.ForEach(x =>
            {
                perentsBL p = new perentsBL();
                parents parents = new parents();
                schedulingDTO s = new schedulingDTO();
                parents = p.GetParentByIdStudent(x.id_student);
                s.code = x.code;
                s.code_class = x.code_class;
                s.hour_ = x.hour_;
                s.hour_enter = x.hour_enter;
                s.hour_exit = x.hour_exit;
                s.disableEnter = false;
                s.disableExit = false;
                s.nameMother = parents.first_name + " " + parents.last_name;
                s.idMother = parents.id;
                s.hour_reach = x.hour_reach;
                s.id_student = x.id_student;
                schedulingDTOList.Add(s);
            });
            return schedulingDTOList;
        }
        //פונקציה המחזירה את כל השיבוצים לפי כיתה
        public List<schedulingDTO> GetSchedulingByCodeClass(int id)
        {
            List<scheduling> schedulingList = schedulingDAL.GetSchedulingByCodeClass(id);
            if (schedulingList == null)
                return new List<schedulingDTO>();
            List<schedulingDTO> schedulingDTOList = new List<schedulingDTO>();
            schedulingList.ForEach(x =>
            {
                perentsBL p = new perentsBL();
                parents parents = new parents();
                schedulingDTO s = new schedulingDTO();
                parents = p.GetParentByIdStudent(x.id_student);
                s.code = x.code;
                s.code_class = x.code_class;
                s.hour_ = x.hour_;
                s.hour_enter = x.hour_enter;
                s.hour_exit = x.hour_exit;
                s.disableEnter = false;
                s.disableExit = false;
                s.nameMother = parents.first_name + " " + parents.last_name;
                s.idMother = parents.id;
                s.hour_reach = x.hour_reach;
                s.id_student = x.id_student;
                schedulingDTOList.Add(s);
            });
            return schedulingDTOList;
        }
        public List<schedulingDTO> GetSchedulingByCodeInstatition(int id)
        {
            List<scheduling> schedulingList = schedulingDAL.GetSchedulingByCodeInstation(id);
            if (schedulingList == null)
                return new List<schedulingDTO>();
            List<schedulingDTO> schedulingDTOList = new List<schedulingDTO>();
            schedulingList.ForEach(x =>
            {
                schedulingDTO s = new schedulingDTO();
                s.code = x.code;
                s.code_class = x.code_class;
                s.hour_ = x.hour_;
                s.hour_enter = x.hour_enter;
                s.hour_exit = x.hour_exit;
                s.hour_reach = x.hour_reach;
                s.id_student = x.id_student;
                schedulingDTOList.Add(s);
            });
            return schedulingDTOList;
        }
        public int AddScheduling(schedulingDTO scheduling)
        {
            var schedulingMapper = iMapper.Map<schedulingDTO, scheduling>(scheduling);

            return schedulingDAL.AddScheduling(schedulingMapper);
        }
        //עדכון 

        public void UpdateScheduling(schedulingDTO scheduling)
        {
            var schedulingMapper = iMapper.Map<schedulingDTO, scheduling>(scheduling);
            schedulingDAL.UpdateScheduling(schedulingMapper);
        }
        //מחיקה
        public void DeleteScheduling(int id)
        {
            schedulingDAL.DeleteScheduling(id);
        }


        public void DeleteSchedulingByCodeInstation(int id)
        {
            List<scheduling> l = new List<scheduling>();
            l = schedulingDAL.GetSchedulingByCodeInstation(id).ToList();
            l.ForEach(x =>
            {
                schedulingDAL.DeleteScheduling(x.code);
            }
          );
        }
        List<studens> allStudents = new List<studens>();
        List<parents> allParents = new List<parents>();
        List<times> allTimes = new List<times>();
        List<request> allRquest = new List<request>();
        List<classes> allClasses = new List<classes>();
        //רשימה של כל התלמידים במוסד
        public List<studens> lst_studens;
        //רשימת בקשות
        public List<request> lst_request;
        //רשימה של השיבוצים
        private List<scheduling> lstscheduling = new List<scheduling>();
        List<List<scheduling>> all_sceduling = new List<List<scheduling>>();
        int idMosad;


        public Boolean ShibutzAndMark(int idMosad)
        {
            var context = new PARENTSEntities();
            allStudents = context.studens.ToList();
            allTimes = context.times.ToList();
            allParents = context.parents.ToList();
            allClasses = context.classes.ToList();
            allRquest = context.request.ToList();
            this.idMosad = idMosad;
            lst_studens = SortByClasses(allStudents.Where(o => o.parents.code_instation == idMosad).ToList());
            func_shibutz();
            all_sceduling.Add(lstscheduling);
            lstscheduling = new List<scheduling>();
            lst_studens = SortByName(allStudents.Where(o => o.parents.code_instation == idMosad).ToList());
            func_shibutz();
            all_sceduling.Add(lstscheduling);
            lstscheduling = new List<scheduling>();
            lst_studens = SortByNumGirles(allStudents.Where(o => o.parents.code_instation == idMosad).ToList());
            func_shibutz();
            all_sceduling.Add(lstscheduling);
            lstscheduling = new List<scheduling>();
            lst_studens = SortByNameDesc(allStudents.Where(o => o.parents.code_instation == idMosad).ToList());
            func_shibutz();
            all_sceduling.Add(lstscheduling);
            lstscheduling = new List<scheduling>();
            lst_studens = SortByLastName(allStudents.Where(o => o.parents.code_instation == idMosad).ToList());
            func_shibutz();
            all_sceduling.Add(lstscheduling);
            lstscheduling = new List<scheduling>();
            lst_studens = SortByLastNameDesc(allStudents.Where(o => o.parents.code_instation == idMosad).ToList());
            func_shibutz();
            all_sceduling.Add(lstscheduling);
            lstscheduling = new List<scheduling>();
            lst_studens = SortByidParentDesc(allStudents.Where(o => o.parents.code_instation == idMosad).ToList());
            func_shibutz();
            all_sceduling.Add(lstscheduling);
            //lstscheduling = new List<scheduling>();
            //lst_studens = SortByhour(studensDAL.GetStudentsByCodeInstation(idMosad));
            //func_shibutz();
            //all_sceduling.Add(lstscheduling);
            lstscheduling = new List<scheduling>();
            lst_studens = SortByidParent(allStudents.Where(o => o.parents.code_instation == idMosad).ToList());
            func_shibutz();
            all_sceduling.Add(lstscheduling);
            //lstscheduling = new List<scheduling>();
            //lst_studens = SortByEmail(studensDAL.GetStudentsByCodeInstation(idMosad));
            //func_shibutz();
            //all_sceduling.Add(lstscheduling);
            double x, max =0;
            int imax = 0;
            //הלולאה תעבוד עד שהציון הגבוה יהיה מעל 80
            while (max <90)
            {
                //חיפוש השיבוץ הטוב ביותר
                for (int i = 0; i < all_sceduling.Count; i++)
                {
                    x = mark(all_sceduling[i]);
                    if (x > max)
                    {
                        max = x;
                        imax = i;
                    }
                }
                shipur(all_sceduling[imax]);//שיפור של הטוב ביותר
                shipur(all_sceduling[0]);//שיפור של איבר קבוע שוב ושוב
                Random rnd = new Random();
                int p = rnd.Next(all_sceduling.Count);
                shipur(all_sceduling[p]);//שיפור של איבר אקראי
            }
            //אחרי שהגענו למיון מ 80 ומעלה - עדכון במסד הנתונים
            foreach (scheduling item in all_sceduling[imax])
            {
                schedulingDAL.AddScheduling(item);
            }

            return true;
        }
        //פונקציות שיוצרות שיבוצים של כל האמהות על פי הבקשות שלהן
        //כל פונקציה תמיין את רשימת האמהות לפי פרמטר אחר
        // וכן תבצע חלק מהשיבוצים באמצעות הגרלה
        //כמובן שכל הפונקציות מתיחסות גם לשמירה על רצף קטן ככל האפשר בין אחיות

        //פונקצית שיבוץ לכל התלמידות 
        public bool func_shibutz()
        {
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
        public bool Shibutz_stud(studens s)
        {
            //נשלוף את הבקשות של האמא
            //את השיבוצים של האחיות של התלמידה
            // את השעות הפנויות בכיתה שלה
            //וננסה לשבץ באופן שמתאים לכל הקריטריונים
         
            //בקשות
            parents p = new parents();
            p = allParents
                .Where(t => t.id == s.id_parent).FirstOrDefault();
            request r = allRquest
                .Where(rr => rr.id_parent == p.id).FirstOrDefault();
            if (r == null)
            {
                r = new request();
                r.id_parent = s.id_parent;
                r.from_hour = allTimes.Where(t => t.code_class == s.code_class).FirstOrDefault().from_hour;
                r.to_hour = allTimes.Where(t => t.code_class == s.code_class).FirstOrDefault().to_hour;
            }
            //שיבוצים של   אחיות
            List<studens> lst_sis = allStudents
                .Where(a => a.id_parent == s.id_parent).ToList();

            List<scheduling> lstSis = new List<scheduling>();
            foreach (studens item in lst_sis)
            {
                scheduling scd = lstscheduling
                    .Where(u => u.id_student == item.id)
                    .FirstOrDefault();
                if (scd != null)
                    lstSis.Add(scd);
            }
            //שעות פנויות בכיתה ממוין לפי שעה
            List<studens> all_class = allStudents
                .Where(cc => cc.code_class == s.code_class).ToList();
            List<scheduling> lstClassNotEmpty = new List<scheduling>();
            foreach (var item in all_class)
            {
                scheduling scd = lstscheduling
                    .Where(cc => cc.id_student == item.id).FirstOrDefault();
                if (scd != null)
                    lstClassNotEmpty.Add(scd);
            }
            lstClassNotEmpty = lstClassNotEmpty.OrderBy(ss => ss.hour_).ToList();
            TimeSpan t1 = allTimes
                .Where(o => o.code_class == s.code_class)
                .FirstOrDefault().from_hour;
            TimeSpan t2 =allTimes
                .Where(o => o.code_class == s.code_class)
                .FirstOrDefault().to_hour;
            List<TimeSpan> lstClass = new List<TimeSpan>();
            for (TimeSpan h = t1; h < t2; h += (new TimeSpan(0, 15, 0)))
            {
                if (lstClassNotEmpty.Where(ss => ss.hour_.Equals(h)).ToList().Count() == 1)
                    lstClass.Add(h);
                else
                    if (lstClassNotEmpty.Where(ss => ss.hour_.Equals(h)).ToList().Count() == 0)
                {
                    lstClass.Add(h);
                    lstClass.Add(h);
                }
            }
            //לולאה שעוברת על כל השעות שבטווח הבקשה של האמא
            //ומנסה לשבץ
            //אם הצליחה מחזירה אמת
            for (TimeSpan h = r.from_hour; h < r.to_hour; h += (new TimeSpan(0, 15, 0)))
            {
                if (lstSis == null || lstSis.Where(ss => ss.hour_.Equals(h)).FirstOrDefault() == null)
                {
                    TimeSpan f = lstClass.Where(ss => ss == h).FirstOrDefault();
                    if (lstClass.Where(ss => ss == h).FirstOrDefault() != (new TimeSpan(00, 00, 00)) && h >= t1 && h < t2)
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

            
            foreach (TimeSpan h in lstClass)
                if (lstSis == null || lstSis.Where(ss => ss.hour_.Equals(h)).FirstOrDefault() == null)
                {
                    {    //שיבוץ בפועל 
                         //והחזרת אמת
                        scheduling x = new scheduling();
                        x.code_class = s.code_class;
                        x.hour_ = h;
                        x.id_student = s.id;
                        lstscheduling.Add(x); ;
                        return true;
                    }
                }

            //אם לא הצליחה יחזיר שקר
            return false;
        }



        ////מיון לפי מספר טלפון
        //public List<studens> SortByhour(List<studens> ls)
        //{
        //   return ls.OrderBy(x => x.parents.telefone).ToList();
        //}
        //מיון לפי שם פרטי
        public List<studens> SortByName(List<studens> ls)
        {
            return ls.OrderBy(x => x.first_name).ToList();
        }
        public List<studens> SortByLastName(List<studens> ls)
        {
            return ls.OrderBy(x => x.last_name).ToList();
        }
        //מיון לפי כיתות
        public List<studens> SortByClasses(List<studens> ls)
        {
            return ls.OrderBy(x => x.code_class).ToList();
        }

        //מיון לפי מספר הבנות
        public List<studens> SortByNumGirles(List<studens> ls)
        {
            return ls.OrderBy(x => x.id_parent.Count()).ToList();
        }
        // מיון לפי שם פרטי בסדר הפוך
        public List<studens> SortByNameDesc(List<studens> ls)
        {
            return ls.OrderByDescending(x => x.first_name).ToList();
        }

        public List<studens> SortByLastNameDesc(List<studens> ls)
        {
            return ls.OrderByDescending(x => x.last_name).ToList();
        }
        public List<studens> SortByidParent(List<studens> ls)
        {
            return ls.OrderBy(x => x.id_parent).ToList();
        }
        public List<studens> SortByidParentDesc(List<studens> ls)
        {
            return ls.OrderByDescending(x => x.id_parent).ToList();
        }
        //public List<studens> SortByEmail(List<studens> ls)
        //{
        //    return ls.OrderBy(x => x.parents.email).ToList();
        //}
        //פונקצית הערכה
        //מקבלת אוסף -ליסט- של השיבוץ של כל בית הספר
        //ומחזירה ציון לשיבוץ
        //לפי
        //1. פרק הזמן שאמא לכמה בנות שוהה בבית הספר
        //2. רמת סטיה מבקשות של אמהות
        private int mark(List<scheduling> l)
        {
            List<parents> lst_par = perentsDAL.GetParentByCodeInstation(this.idMosad);
            double avg = 0;
            int mark = 100, cntExp = 0;
            TimeSpan from = new TimeSpan(00, 0, 0);
            TimeSpan to = new TimeSpan(00, 0, 0);
            TimeSpan min = new TimeSpan(23, 59, 0);
            TimeSpan max = new TimeSpan(00, 00, 0);
            TimeSpan temp;
            perentsBL pars = new perentsBL();
            requestBL r = new requestBL();
            //1. פרק הזמן שאמא לכמה בנות שוהה בבית הספר
            foreach (parents p in lst_par)
            {
                List<studens> lst_k = pars.get_Allkids(p.id);
                min = new TimeSpan(23, 59, 0);
                max = new TimeSpan(00, 00, 0);
                for (int i = 0; i < lst_k.Count(); i++)
                {
                    //השוואה של timespan
                    temp = l.Where(x => x.id_student == lst_k[i].id).FirstOrDefault().hour_;
                    if (temp > (max))
                        max = temp;
                    if (temp < min)
                        min = temp;
                }
                avg = (max - min).TotalMinutes;
                avg = (max - min).TotalMinutes / lst_k.Count();
                if (avg > 30)
                {
                    cntExp++;
                }

            }
            mark = mark - cntExp;
            cntExp = 0;
            //2.רמת סטיה מבקשות של אמהות
            //עובר על רשימת האימהות
            foreach (parents pa in lst_par)
            {
                List<studens> lst_k = pars.get_Allkids(pa.id);
                List<studens> st;
                TimeSpan scheTime = new TimeSpan();
                parents p = new parents();
                request h = allRquest.Where(rr => rr.id_parent == pa.id).FirstOrDefault();
                if (h != null)
                {
                    from = h.from_hour;
                    to = h.to_hour;
                    st = lst_studens.Where(y => y.id_parent == pa.id).ToList();
                    //שולף מרשימת השיבוצים שהתקבלה את השעות של הילדים של ההורה 
                    foreach (studens item in lst_k)
                    {
                        scheTime = l.Where(e => e.id_student == item.id).Select(t => t.hour_).FirstOrDefault();
                        if (scheTime < from || scheTime > to)
                        {
                            cntExp++;
                        }
                    }
                }
            }
            
            mark = mark - cntExp;
            return mark;
        }
        //פונקצית שיפור - מקבלת שיבוץ
        //מחפשת את ההורים ששוהים הכי הרבה זמן בבית הספר ומנסה לקצר את זמן השהות שלהם ע"י החלפה עם חברה
        //רק אם לא יוצר שיבוץ לא תקין של אמא בשתי כיתות בו זמנית
        public void shipur(List<scheduling> l)
        {
            List<parents> lst_par = allParents.Where(b=>b.code_instation==this.idMosad).ToList();
            ParentMark[] lst_mark = new BL.ParentMark[lst_par.Count / 100];
            int cur = 0;
            perentsBL pars = new perentsBL();
            double avg = 0;
            TimeSpan min, max, temp;
            int i;
            foreach (parents p in lst_par)
            {
                List<studens> lst_k = pars.get_Allkids(p.id);
                min = new TimeSpan(23, 59, 0);
                max = new TimeSpan(00, 00, 0);
                for (i = 0; i < lst_k.Count(); i++)
                {
                    //השוואה של timespan
                    temp = l.Where(x => x.id_student == lst_k[i].id).FirstOrDefault().hour_;
                    if (temp > (max))
                        max = temp;
                    if (temp < min)
                        min = temp;
                }
                avg = (max - min).TotalMinutes / lst_k.Count();
                //עצם שמכיל את האמא יחד עם הממוצע שלה
                ParentMark pm = new BL.ParentMark(p, avg);
                //הכנסת ההורה למקום המתאים במערך (אם בכלל) כך שבסיום הלולאה המערך יכיל את 1% ההורים שהממוצע שלהם הוא הגרוע ביותר ממוינים בסדר יורד
                for (i = 0; i < cur && avg > lst_mark[i].avg; i++) ;
                if (i == cur && cur < lst_mark.Length)
                    lst_mark[cur++] = pm;
                else
                {
                    if (i < cur)
                    {
                        for (int j = cur++; j > i; j--)
                            lst_mark[j] = lst_mark[j - 1];
                        lst_mark[i] = pm;
                    }
                    else
                    if (cur == lst_mark.Length - 1 && lst_mark[cur].avg < avg)
                        lst_mark[cur] = pm;
                }
            }
            //מעבר על מערך האמהות שהשיבוץ שלהן הוא הגרוע ביותר מבחינת השעות
            foreach (ParentMark item in lst_mark)
            {
                //מציאת כל השיבוצים של האמא
                List<scheduling> kids_scd = new List<scheduling>();
                List<studens> lst_kids = pars.get_Allkids(item.par.id);
                foreach (studens s in lst_kids)
                    kids_scd.Add(l.Where(scd => scd.id_student == s.id).FirstOrDefault());
                kids_scd.OrderBy(ks => ks.hour_);
                List<TimeSpan> free = new List<TimeSpan>();
                //חיפוש כל שעות החורים של האמא
                for (TimeSpan t = kids_scd[0].hour_ + new TimeSpan(0, 15, 0); t < kids_scd[kids_scd.Count - 1].hour_; t += (new TimeSpan(0, 15, 0)))
                {
                    if (kids_scd.Where(ks => ks.hour_.Equals(t)).FirstOrDefault() == null)
                        free.Add(t);
                }
                //מעבר על רשימת החורים של האמא
                //ונסיון לבצע החלפה בין השעה המוקדמת ביותר או המאוחרת ביותר לאחת השעות הפנויות
                foreach (TimeSpan t in free)
                {
                    //נסיון להחליף את הבת המשובצת ראשונה עם החור
                    if (Replace(l, kids_scd[0], t))
                        break;
                    //נסיון להחליף את הבת המשובצת אחרונה עם החור
                    if (Replace(l, kids_scd[kids_scd.Count - 1], t))
                        break;
                }

            }
        }
        //פונקצית החלפת שיבוץ לשעה אחרת באם מתאפשר
        //מחזירה אמת אם החליפה ושקר אם לא
        private bool Replace(List<scheduling> l, scheduling sc, TimeSpan t)
        {
            var context = new PARENTSEntities();
            scheduling scd = l.Where(ss => ss.code_class == sc.code_class && ss.hour_.Equals(t)).FirstOrDefault();
            List<studens> sis = new List<studens>();
            List<studens> lst_sis = allStudents
              .Where(a => a.id_parent == scd.studens.id_parent).ToList();
            //שיבוצים של האחיות
            List<scheduling> lstSis = new List<scheduling>();
            foreach (studens item in lst_sis)
            {
                scheduling scd1 = lstscheduling
                    .Where(u => u.id_student == item.id)
                    .FirstOrDefault();
                if (scd1 != null)
                    lstSis.Add(scd1);
            }
            //אם אף אחות לא משובצת בשעה המבוקשת להחלפה
            if (lstSis.Where(ssc => ssc.hour_.Equals(sc.hour_)).FirstOrDefault() == null)
            {
                //ביצוע החלפה בפועל והחזרת ערך אמת
                scd.hour_ = sc.hour_;
                sc.hour_ = t;
                return true;
            }
            return false;
        }

        // בלחיצה על כפתור שבץ יופעל פונקציה של השיבוצים השונים ותגדיר רשימה של שיבוצים והפונקציה תפעיל על כל שיבוץ ברשימה פונקצית הערכה ותבחר את השיבוץ הטוב ביותר ואותו תשמור בפונקצית השיבוצים
        //ברגע שהאמא הגיע הוא בודק מה התור הקרוב ביותר ומעדכן את השיבוץ של הכיתות השונות
        //לשקול בהמשך אם כדאי להוסיף פונקציה לשיפור של שיבוץ קיים




        //פונקצית זמן אמת מקבלת קוד הורה ןבודקת האם כדאי לשנות את התור
        public schedulingDTO FuncRealTime(string id)
        {
            List<scheduling> s = new List<scheduling>();
            schedulingDTO sn = new schedulingDTO();
            List<classes> c = new List<classes>();
            classes shortQ = new classes();
            var p = new PARENTSEntities();
            //שליפה ל כל השיבוצים של הבנות שהאמא עדיין לא נכנסה ומיון לפי שעה
            s = p.scheduling.Where(x => x.studens.id_parent == id && x.hour_exit == null)
                .OrderBy(f => f.hour_).ToList();
            c = get_classes(id);
            //אם להורה יש יותר מילד אחד יש לבדוק באיזה כיתה התור קצר יותר ואם צריך להחליף
            if (s.Count() > 1)
            {
                for (int i = 0; i < s.Count; i++)
                {
                    //עדכון שעת הגעה
                    if (s[i].hour_reach == null)
                        s[i].hour_reach = DateTime.Now.TimeOfDay;
                    schedulingDAL.UpdateScheduling(s[i]);
                }

                shortQ = getShortClass(c);

                if (s[0].code_class != shortQ.code)
                {
                    scheduling n = new scheduling();
                    List<scheduling> l = new List<scheduling>();
                    n = s.Where(f => f.code_class == shortQ.code).FirstOrDefault();
                    //תור ההורים שהגיעו ממוין לפי שעות
                    l = p.scheduling.Where(x => x.hour_reach != null && x.hour_enter == null
                     && x.studens.code_class == shortQ.code).OrderBy(v => v.hour_).ToList();
                    //אם הגיע ראשון
                    if (l.Count == 1)
                    {
                        sn.idMother = id;
                        sn.nameMother = s.First().studens.parents.first_name;
                        sn.code_class = shortQ.code;
                        sn.nameStudent = s.First().studens.first_name + " " + s.First().studens.last_name;
                        sn.class_ = shortQ.class_+" "+ shortQ.num_class;
                        return sn;
                    }
                    //בדיקה האם הגיעה השעה בכיתה אחרת

                    foreach (scheduling item in s)
                    {

                        if (item.hour_.Hours == DateTime.Now.Hour && item.hour_.Minutes == DateTime.Now.Minute)
                        {
                            if (l.Count == 1)
                            {
                                var f = new PARENTSEntities();
                                classes clas = f.classes.Where(j => j.code == item.code_class).FirstOrDefault();
                                sn.nameMother = s.First().studens.parents.first_name;
                                sn.idMother = id;
                                sn.code_class = item.code_class;
                                sn.nameStudent = s.First().studens.first_name + " " + s.First().studens.last_name;
                                sn.class_ = clas.class_+" "+clas.num_class;
                                return sn;
                            }

                        }
                    }
                    scheduling b = l.Where(u => u.hour_ == l.Max(t => t.hour_)).First();
                    TimeSpan a = new TimeSpan(b.hour_.Hours, b.hour_.Minutes + 10, 00);
                    n.hour_ = a;
                    schedulingDAL.UpdateScheduling(n);
                    sn.hour_ = n.hour_;
                    sn.nameStudent = s.First().studens.first_name + " " + s.First().studens.last_name;
                    sn.class_ = shortQ.class_ + " " + shortQ.num_class;
                    return sn;
                }
            }
            else
            {
                //עדכון שעת הגעה לשעה הנוכחית אם יש ילד אחד להורה
                if (s.First().hour_reach == null)
                    s.First().hour_reach = DateTime.Now.TimeOfDay;
                schedulingDAL.UpdateScheduling(s.First());
                sn.nameMother = s.First().studens.parents.first_name;
                sn.idMother = id;
                sn.nameStudent = s.First().studens.first_name + " " + s.First().studens.last_name;
                sn.class_ = c.First().class_ + " " + c.First().num_class;
                return sn;
            }

            return sn;
        }
        //מחזירה רשימה של הכתות של הבנות
        public static List<classes> get_classes(string id)
        {
            var PARENTSEntities1 = new PARENTSEntities();
            //שליפת ילדים
            List<studens> s = PARENTSEntities1.studens.Where(ss => ss.id_parent == id).ToList();
            List<classes> c = new List<classes>();
            foreach (studens item in s)
            {
                c.Add(PARENTSEntities1.classes.Where(o => o.code == item.code_class).FirstOrDefault());
            }

            return c;
        }


        //מקבלת רשימת כיתות
        //מחזירה את הכיתה עם התור הקצר ביותר
        public static classes getShortClass(List<classes> list)
        {
            var parents = new PARENTSEntities();
            int min = 100000, i_min = 0;
            foreach (classes item in list)
            {
                int v = parents.scheduling.Where(x => x.hour_reach != null
               && x.hour_exit == null && x.code_class == item.code).Count();
                if (v < min)
                {
                    i_min = list.IndexOf(item);
                    min = v;
                }

            }

            return list[i_min];
        }



        //מחזירה את מספר הדקות שנותר לאמא לחכות
        public static int getTimeToWait(string id)
        {
            classes class1;
            List<classes> cl;
            cl = get_classes(id);
            class1 = getShortClass(cl);
            List<parents> v = (from s in global.PARENTS.scheduling
                               from t in global.PARENTS.studens
                               from p in global.PARENTS.parents
                               where id == t.id_parent && t.id == s.id_student && class1.code == t.code_class && s.hour_exit == null && s.hour_reach != null && p.id == t.id_parent
                               select new parents { id = p.id, last_name = p.last_name, first_name = p.first_name }).ToList();
            int i = 0;
            int count = 0;
            while (v[i].id != id)
            {
                count += 7;
            }
            return count;
        }
        //עדכון כניסה
        public List<schedulingDTO> updateEnter(string id)
        {
            var context = new PARENTSEntities();
            List<scheduling> schedulingList = schedulingDAL.updateEnter(id); ;
            if (schedulingList == null)
                return new List<schedulingDTO>();
            List<schedulingDTO> schedulingListDTO = new List<schedulingDTO>();
            schedulingList.ForEach(x =>
            {
                schedulingDTO s = new schedulingDTO();
                s.code = x.code;
                s.code_class = x.code_class;
                s.hour_ = x.hour_;
                s.id_student = x.id_student;
                s.hour_enter = x.hour_enter;
                s.nameMother = context.parents.Where(p => p.id == id).Select(o => o.first_name + " " + o.last_name).FirstOrDefault();
                s.disableExit = false;
                s.hour_exit = x.hour_exit;
                s.hour_reach = x.hour_reach;
                schedulingListDTO.Add(s);
            });
            foreach (var item in schedulingListDTO)
            {
                if (item.id_student == id)
                    item.disableEnter = true;
            }

            return schedulingListDTO;
        }
        //עדכון יציאה
        public List<schedulingDTO> updateExit(string id)
        {
            var context = new PARENTSEntities();
            List<scheduling> schedulingList = schedulingDAL.updateExit(id); ;
            if (schedulingList == null)
                return new List<schedulingDTO>();
            List<schedulingDTO> schedulingListDTO = new List<schedulingDTO>();
            schedulingList.ForEach(x =>
            {
                schedulingDTO s = new schedulingDTO();
                s.code = x.code;
                s.nameMother = context.parents.Where(p => p.id == id).Select(o => o.first_name + " " + o.last_name).FirstOrDefault();
                s.code_class = x.code_class;
                s.hour_ = x.hour_;
                s.id_student = x.id_student;
                s.hour_enter = x.hour_enter;
                s.disableExit = false;
                s.hour_exit = x.hour_exit;
                s.hour_reach = x.hour_reach;
                schedulingListDTO.Add(s);
            });
            foreach (var item in schedulingListDTO)
            {
                if (item.id_student == id)
                {
                    item.disableExit = true;
                    item.disableEnter = true;
                }
            }

            return schedulingListDTO;
        }
    }
}
