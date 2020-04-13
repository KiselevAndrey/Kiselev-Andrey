using System;
using System.Collections.Generic;

namespace HW_7
{
    class Student_s_group
    {
        public string Name { get; private set; }
        public List<Student> students;

        public Student_s_group(string name, bool print = false)
        {
            Name = name;
            students = new List<Student>();

            if (print) Console.WriteLine($"Create new group: {Name}");
        }

        public void AddStudent(string name, bool print = false)
        {
            students.Add(new Student(name, Name));

            if (print) Console.WriteLine($"New student ({name}) added in group ({Name})");
        }

        public void PrintStudents()
        {
            Console.WriteLine($"\n{Name}\nStudents:");
            foreach (var student in students)
            {
                student.Print();
            }
        }

        public void PrintStudentsAverageMark()
        {
            Console.WriteLine($"\n{Name}\nStudents average mark:");
            foreach (var student in students)
            {
                student.PrintAverageMark();
            }
        }
        
        public void AddMark(string student_name, int mark, bool print = false)
        {
            int index = FindIndexStudent(student_name, print);  
            if (index > -1)
            {
                students[index].AddMark(mark, print);
            }
        }

        int FindIndexStudent(string student_name, bool print = false)
        {
            var st = new Student(student_name);
            var index = -1;
            foreach (var stud in students)
            {
                if (st == stud)
                {
                    index = students.IndexOf(stud);
                    break;
                }
            }
            if (print && index == - 1) Console.WriteLine($"No student {student_name} in {Name}");
            return index;
        }

        public bool FindStudent(string student_name, bool print = false)
        {
            return FindIndexStudent(student_name, print) > -1 ? true : false;
        }

        public void AddRandomMarksAll(int count)
        {
            Random rand = new Random();
            foreach (var student in students)
            {
                for (int i = 0; i < count; i++)
                {
                    student.AddMark(rand.Next(10));
                }
            }
        }
    }
}
