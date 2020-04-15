using System;
using System.Collections.Generic;
using System.Text;
using Kiselev_Andrey;

namespace HW_9
{
    class Board
    {
        public List<Status> Statuses { get; private set; }
        public string Name { get; private set; }

        public Board(string name)
        {
            Name = name;
            Statuses = new List<Status>();
        }

        public void AddStatus(Status status)
        {
            Statuses.Add(status);
        }

        public void ChangeStatusOnCardConsole()
        {
            byte oldChoice = StartMenu.Choiсe("Old card statuses", Statuses);
            if (Statuses[oldChoice].Cards.Count == 0)
            {
                Console.WriteLine("None cards in status");
                return;
            }
            Statuses[StartMenu.Choiсe("New card status", Statuses)].AddCard(Statuses[oldChoice].PopCard());
        }

        public static Board ReadConsole()
        {
            return new Board(ConsoleRead.String("Input board name: "));
        }
        
        public void DelStatusConsole()
        {
            Statuses.RemoveAt(StartMenu.Choiсe("Del Board", Statuses));
        }

        public void TravelToStatusConsole()
        {
            Console.Clear();
            Statuses[StartMenu.Choiсe("Travel", Statuses)].ManagerConsole();
        }

        public void ManagerConsole()
        {
            while (true)
            {
                byte choice = StartMenu.Choiсe(Name, "Add Status", "Del Status", "Change Status on Card", "Travel to Status", "Change name Board", "Print all Statuses");

                if (choice == 0) break;

                else if (choice == 1) AddStatus(Status.ReadConsole());
                else if (choice == 5) Name = ConsoleRead.String("Input new name Board: ");
                else if (Statuses.Count == 0)
                {
                    StartMenu.EnterClearConsole("Status count is null");
                    continue;
                }
                else if (choice == 2) DelStatusConsole();
                else if (choice == 3) ChangeStatusOnCardConsole();
                else if (choice == 4) TravelToStatusConsole();
                else if (choice == 6) Print();


                StartMenu.EnterClearConsole();
            }
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public void Print()
        {
            Console.WriteLine($"\n\t{Name}\n");

            foreach (var status in Statuses)
            {
                status.Print();
            }
        }
    }
}
