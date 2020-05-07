using System;
using Kiselev_Andrey;

namespace HW_9
{
    class Program
    {
        static void Main(string[] args)
        {
            Trello trello = new Trello();

            //trello.AddBoard(new Board("HW 6"));
            //trello.AddBoard(new Board("Quater close"));

            //trello.Boards[0].AddStatus(new Status("To Do"));
            //trello.Boards[0].AddStatus(new Status("To student"));
            
            //trello.Boards[1].AddStatus(new Status("To perform"));
            //trello.Boards[1].AddStatus(new Status("To check"));
            //trello.Boards[1].AddStatus(new Status("Done"));

            //trello.Boards[0].AddCard("To Do", "Kiselev", "Home work is done");
            //trello.Boards[0].AddCard("ToDO", "Avramenko", "Home work is not done");
            //trello.Boards[0].AddCard("To Do", "Karytko", "Home work it's a good idea");

            trello.ManagerConsole();
        }
    }
}
