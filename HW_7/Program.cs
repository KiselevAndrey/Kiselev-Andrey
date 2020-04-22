using System;
using Kiselev_Andrey;

namespace HW_7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();

            bool print = true;

            Student st1 = new Student("Andrey", print);
            st1.AddGroup("p-47", print);
            st1.AddMark(9, print);
            st1.AddMark(7, print);
            st1.AddMark(8, print);
            st1.AddMark(9, print);

            st1.Print();

            Console.WriteLine("Now student's group");
            StartMenu.EnterClearConsole();

            while (true)
            {
                byte choice = StartMenu.Choiсe("Student's group", ");

                StartMenu.EnterClearConsole();
            }
        }
    }
}
