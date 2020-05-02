using System;
using System.Collections.Generic;
using System.Text;
using Kiselev_Andrey;

namespace HW_9
{
    class Status
    {
        public List<Card> Cards { get; private set; }

        public string Name { get; private set; }

        public Status(string name)
        {
            Name = name;
            Cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        public void Print()
        {
            Console.WriteLine($"\n\t{Name}\n");

            foreach (var card in Cards)
            {
                card.Print();
            }
        }

        public void Print(string author)
        {
            foreach (var card in Cards)
            {
                if (card.Author == author)
                    card.Print();
            }
        }

        public Card PopCard()
        {
            byte choice = StartMenu.Choiсe("Delete card", Cards);

            Card temp = Cards[choice];
            Cards.RemoveAt(choice);

            return temp;
        }

        public Card PipLastAddedCard()
        {
            return Cards[Cards.Count-1];
        }

        public static Status ReadConsole()
        {
            return new Status(ConsoleRead.String("Input status name: "));
        }

        public void DelCardConsole()
        {
            if (Cards.Count == 0)
            {
                Console.WriteLine("Nothing delete");
                return;
            }
            Cards.RemoveAt(StartMenu.Choiсe("Del Board", Cards));
        }

        public void ManagerConsole()
        {
            while (true)
            {
                byte choice = StartMenu.Choiсe(Name, "Print all Card", "Print Card one author", "Change name Status");

                if (choice == 0) break;

                else if (choice == 3) Name = ConsoleRead.String("Inut new name Status: ");
                else if (Cards.Count == 0)
                {
                    StartMenu.EnterClearConsole("Card count is null");
                    continue;
                }
                else if (choice == 1) Print();
                else if (choice == 2) Print(ConsoleRead.String("Input author name: "));

                StartMenu.EnterClearConsole();
            }
        }
        
        public override string ToString()
        {
            return $"{Name} ({Cards.Count} card)";
        }

        public bool Equals(string nameStatus)
        {
            return Name == nameStatus;
        }
    }
}
