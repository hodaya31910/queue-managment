using BL;
using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.projectBL
{
 public class addTable
    {
        public void func()
        {
            studentDTO s = new studentDTO();
            StreamReader read = new StreamReader(@"d:\aa.csv", Encoding.Default);
            string str = read.ReadToEnd();
            string[] arr = str.Split('\n');
            for (int i = 0; i < arr.Length; i++)
            {
                int j = 0;
                string[] arr1 = arr[i].Split(',');
                s.id = arr1[j++];
                s.first_name = arr1[j++];
                s.last_name = arr1[j++];
                s.id_parent = arr1[j++];
                s.code_class = int.Parse(arr1[j++]);
                studentBL sBL = new studentBL();
                sBL.AddStudent(s);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        public void addparents()
        {
            parentsDTO p = new parentsDTO();
            StreamReader read = new StreamReader(@"d:\parents.csv", Encoding.Default);
            string str = read.ReadToEnd();
            string[] arr = str.Split('\n');
            for (int i = 0; i < arr.Length; i++)
            {
                int j = 0;
                string[] arr1 = arr[i].Split(',');
                p.id = arr1[j++];
                p.first_name = arr1[j++];
                p.last_name = arr1[j++];
                p.telefone = arr1[j++];
                p.email = arr1[j++];
                p.code_instation = int.Parse(arr1[j++]);
                perentsBL pBL = new perentsBL();
                pBL.AddParents(p);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        public void addRequest()
        {
            requestDTO r = new requestDTO();
            StreamReader read = new StreamReader(@"d:\request.csv", Encoding.Default);
            string str = read.ReadToEnd();
            string[] arr = str.Split('\n');
            for (int i = 0; i < arr.Length; i++)
            {
                int j = 0;
                string[] arr1 = arr[i].Split(',');
                r.code =int.Parse( arr1[j++]);
                r.id_parent = arr1[j++];
                r.from_hour =TimeSpan.Parse( arr1[j++]);
                r.to_hour= TimeSpan.Parse(arr1[j++]);
                requestBL rBL = new requestBL();
                rBL.AddRequest(r);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        public void addTeacher()
        {
            teacherDTO t = new teacherDTO();
            StreamReader read = new StreamReader(@"d:\teachers.csv", Encoding.Default);
            string str = read.ReadToEnd();
            string[] arr = str.Split('\n');
            for (int i = 0; i < arr.Length; i++)
            {
                int j = 0;
                string[] arr1 = arr[i].Split(',');
                t.id = arr1[j++];
                t.NAME = arr1[j++];
                t.telefone = arr1[j++];
                t.code_instation =int.Parse( arr1[j++]);
                teacherBL tBL = new teacherBL();
                tBL.AddTeachers(t);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        public void addClasses()
        {
            ClassesDTO c = new ClassesDTO();
            StreamReader read = new StreamReader(@"d:\classes.csv", Encoding.Default);
            string str = read.ReadToEnd();
            string[] arr = str.Split('\n');
            for (int i = 0; i < arr.Length; i++)
            {
                int j = 0;
                string[] arr1 = arr[i].Split(',');
                c.code = int.Parse(arr1[j++]);
                c.id_teacher= arr1[j++];
               c.class_ = arr1[j++];
               c.num_class =int.Parse(arr1[j++]);
                ClassesBL cBL = new ClassesBL();
                cBL.AddClasses(c);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
} 


