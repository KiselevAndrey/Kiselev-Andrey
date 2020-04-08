using System;
using System.Collections.Generic;
using System.Text;

namespace HW_7
{
    class Student
    {
        public string Name { get; private set; }
        public string Group { get; private set; }
        List<int> marks = new List<int>();

        public Student(string name, bool print = false)
        {
            Name = name;
            if (print) Console.WriteLine($"Hi! My name is {Name}. I'm student!");
        }

        public Student(string name, string group, bool print = false) : this(name, print)
        {
            AddGroup(group, print);
        }

        public void AddMark(int mark, bool print = false)
        {
            marks.Add(mark);
            if (print)
            {
                Console.WriteLine($"My new mark: {mark}");
                Console.WriteLine($"My average mark: {AverageMark()}. {Name} {Group}");
            }
        }

        public float AverageMark()
        {
            if (marks.Count == 0) return 0;

            float summ = 0;
            foreach (var mark in marks)
            {
                summ += mark;
            }
            return summ / marks.Count;
        }

        public void Print()
        {
            Console.WriteLine($"{Group} {Name}");
        }

        public void PrintAverageMark()
        {
            Console.Write($"{Name}: ");
            if (marks.Count > 0)
            {
                foreach (var mark in marks)
                {
                    Console.Write($" {mark}");
                }
                Console.WriteLine($". averge = {AverageMark()}");
            }
            else Console.WriteLine("No marks");
        }

        public void AddGroup(string group, bool print = false)
        {
            Group = group;
            if (print) Console.WriteLine($"Now my group is {Group}. {Name}");
        }

        public static bool operator ==(Student st1, Student st2)
        {
            return st1.Name == st2.Name;
        }

        public static bool operator !=(Student st1, Student st2)
        {
            return st1.Name != st2.Name;
        }
    }
}
