using System;
using System.Collections.Generic;
using System.Text;
using Kiselev_Andrey;

namespace HW_9
{
    class Trello
    {
        public string Name { get => "Trello"; }
        public List<Board> Boards { get; }

        public Trello()
        {
            Boards = new List<Board>();
        }

        public void AddBoard(Board board)
        {
            Boards.Add(board);
        }

        public void DelBoardConsole()
        {
            Boards.RemoveAt(StartMenu.Choiсe("Del Board", Boards));
        }

        public void TravelToBoardConsole()
        {
            Console.Clear();
            Boards[StartMenu.Choiсe("Travel", Boards)].ManagerConsole();
        }

        public void ManagerConsole()
        {
            while (true)
            {
                byte choice = StartMenu.Choiсe(Name, "Add Board", "Print all Board", "Del Board", "Travel to Board");

                if (choice == 0) break;
                
                else if (choice == 1) AddBoard(Board.ReadConsole());
                else if (Boards.Count == 0)
                {
                    StartMenu.EnterClearConsole("Board count is null");
                    continue;
                }
                else if (choice == 2) Print();
                else if (choice == 3) DelBoardConsole();
                else if (choice == 4) TravelToBoardConsole();

                Console.Clear();
            }
        }

        public void Print()
        {
            Console.WriteLine($"\n\t{Name}\n");

            foreach (var board in Boards)
            {
                Console.WriteLine(board);
            }
        }
    }
}
