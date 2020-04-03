using System;
using System.Collections.Generic;
using Kiselev_Andrey;

namespace HW_6
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                byte key = StartMenu.Choise("Домашняя работа 6", "Студенты и стек", "Студенты и очередь");

                if (key == 0)
                {
                    break;
                }

                Console.Clear();

                switch (key)
                {
                    case 1:
                        StudentsStack();
                        break;

                    case 2:

                        break;

                    default:
                        break;
                }

                Console.WriteLine("\n\nНажмите Enter.");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void StudentsStack()
        {
            Console.WriteLine("Студенты и стек");
            Stack<string> students = new Stack<string>();

            Console.WriteLine("\nAdd 3 student's task");
            for (int i = 0; i < 3; i++)
            {
                StudentReceived($"Student{i+1}", ref students);
            }

            Console.WriteLine("\nGive 4 coffee");
            for (int i = 0; i < 4; i++)
            {
                CoffeFromStudent(ref students);
            }
        }

        static void StudentReceived(string student_name, ref Stack<string> stack)
        {
            stack.Push(student_name);
            Console.WriteLine($"Task from <{student_name}> is recieved");
        }

        static void CoffeFromStudent(ref Stack<string> stack)
        {
            if (stack.Count > 0)
            {
                Console.WriteLine($"<{stack.Pop()}> got a coffee");
            }
            else
            {
                Console.WriteLine("No student's task - no coffee");
            }
        }
    }
}
