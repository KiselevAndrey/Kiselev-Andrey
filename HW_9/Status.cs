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
            foreach (var card in Cards)
            {
                card.Print();
            }
        }

        public void Print(string author)
        {
            foreach (var card in Cards)
            {
                if (card.NameAuthor == author)
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

        public void TravelToCardConsole()
        {
            if (Cards.Count == 0)
            {
                Console.WriteLine("There's no travel");
                return;
            }

            Console.Clear();
            Cards[StartMenu.Choiсe("Travel", Cards)].ManagerConsole();

        }

        public void ManagerConsole()
        {
            while (true)
            {
                byte choice = StartMenu.Choiсe(Name, "Add Card", "Del Card", "Print all Card", "Print Card one author", "Travel to Card", "Change name Status");

                if (choice == 0) break;

                else if (choice == 1) AddCard(Card.ReadConsole());
                else if (choice == 6) Name = ConsoleRead.String("Inut new name Status: ");
                else if (Cards.Count == 0)
                {
                    StartMenu.EnterClearConsole("Card count is null");
                    continue;
                }
                else if (choice == 2) DelCardConsole();
                else if (choice == 3) Print();
                else if (choice == 4) Print(ConsoleRead.String("Input author name: "));
                else if (choice == 5) TravelToCardConsole();

                StartMenu.EnterClearConsole();
            }
        }
        
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
