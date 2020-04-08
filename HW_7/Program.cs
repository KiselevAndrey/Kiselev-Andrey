using System;
using Kiselev_Andrey;

namespace HW_7
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                byte choice = StartMenu.Choiсe("Student and Student's Group", "Auto display", "Manual entry");
                if (choice == 0) break;

                switch (choice)
                {
                    case 1:
                        Auto();
                        break;

                    case 2:
                        Manual();
                        break;

                    default:
                        Console.WriteLine("Wrong choise");
                        break;
                }

                StartMenu.EnterClearConsole();
            }
        }

        static void Auto()
        {

            Console.WriteLine();

            bool print = true;

            Student st1 = new Student("Andrey", print);
            st1.AddGroup("p-47", print);
            st1.AddMark(9, print);
            st1.AddMark(7, print);
            st1.AddMark(8, print);
            st1.AddMark(9, print);

            Console.WriteLine();
            st1.Print();

            Console.WriteLine("\n\nNow student's group");
            StartMenu.EnterClearConsole();

            Console.WriteLine();

            Student_s_group sg1 = new Student_s_group("python_03.20", print);
            Student_s_group sg2 = new Student_s_group("c#_02.20", print);

            Console.WriteLine();

            sg1.AddStudent("Igor", print);
            sg1.AddStudent("Tatiana", print);
            sg1.AddStudent("Leonid", print);
            sg1.AddStudent("Maksim Nikolaevich", print);

            Console.WriteLine();

            sg2.AddStudent("Genadiy", print);
            sg2.AddStudent("Polina", print);
            sg2.AddStudent("Lolita Anakent'evna", print);

            Console.WriteLine();

            sg2.AddMark("Polina", 10, print);
            Console.WriteLine();
            sg2.AddMark("Igor", 10, print);

            Console.WriteLine("\nAdded 10 random marks every students");
            sg1.AddRandomMarksAll(10);
            sg2.AddRandomMarksAll(10);

            Console.WriteLine();

            sg1.PrintStudents();
            sg2.PrintStudents();

            Console.WriteLine();

            sg1.PrintStudentsAverageMark();
            sg2.PrintStudentsAverageMark();
        }

        static void Manual()
        {
            bool print = true;
            Console.WriteLine("Input name group");
            Student_s_group sg = new Student_s_group(Console.ReadLine(), print);
            Console.Clear();
            while (true)
            {
                byte choice = StartMenu.Choiсe(sg.Name, "Add student", "Add mark", "Print all student", "Print student's average mark", "-----------", "Add to all students random marks");
                if (choice == 0) break;

                switch (choice)
                {
                    case 1:
                        sg.AddStudent(ConsoleRead.String("Input name new student: "), print);
                        break;

                    case 2:
                        string name = ConsoleRead.String("Input name student: ");
                        if (sg.FindStudent(name, print))
                        {
                            sg.AddMark(name, ConsoleRead.Int("Input mark: "), print);
                        }
                        break;

                    case 3:
                        sg.PrintStudents();
                        break;

                    case 4:
                        sg.PrintStudentsAverageMark();
                        break;

                    case 5:
                        break;

                    case 6:
                        sg.AddRandomMarksAll(ConsoleRead.Int("Input count of marks: "));
                        break;

                    default:
                        Console.WriteLine("Wrong choise");
                        break;
                }

                StartMenu.EnterClearConsole();
            }
        }
    }
}
