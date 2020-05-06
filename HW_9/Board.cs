using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Kiselev_Andrey;

namespace HW_9
{
    class Board
    {
        public List<Status> Statuses { get; private set; }
        public string Name { get; private set; }
        readonly List<Author> authors;

        public delegate void CreateCard();
        public delegate void ChangeCardStatus(string changedStatus);

        public event CreateCard CardCreated;
        public event ChangeCardStatus CardStatusChanged;

        public Board(string name)
        {
            Name = name;
            Statuses = new List<Status>();
            authors = new List<Author>();
        }

        #region Add
        public void AddStatus()
        {
            Statuses.Add(new Status(ConsoleRead.String("Input name new status: ")));
        }

        public void AddStatus(Status status)
        {
            Statuses.Add(status);
        }

        void AddStatus(ref int posNewStatus)
        {
            AddStatus();
            posNewStatus = Statuses.Count - 1;
        }

        void AddStatus(ref int posNewStatus, string statusName)
        {
            AddStatus(new Status(statusName));
            posNewStatus = Statuses.Count - 1;
        }

        void AddAuthor()
        {
            authors.Add(new Author(ConsoleRead.String("Input name new author: ")));
        }

        void AddAuthor(ref int posNewAuthor)
        {
            AddAuthor();
            posNewAuthor = authors.Count - 1;
            CardCreated += authors[posNewAuthor].NotificationCreateCard;
            CardStatusChanged += authors[posNewAuthor].NotificationChangeCardStatus;
        }

        void AddAuthor(ref int posNewAuthor, string authorName)
        {
            authors.Add(new Author(authorName));
            posNewAuthor = authors.Count - 1;
            CardCreated += authors[posNewAuthor].NotificationCreateCard;
            CardStatusChanged += authors[posNewAuthor].NotificationChangeCardStatus;
        }

        public void AddCard()
        {
            GetStatusIndex(out int choiceStatus);
            GetAuthorIndex(out int choiceAuthor);

            Statuses[choiceStatus].AddCard(new Card(authors[choiceAuthor], ConsoleRead.String("Input card text: ")));
            authors[choiceAuthor].AddCard(Statuses[choiceStatus].PipLastAddedCard());
            OnCardCreated();
        }

        public void AddCard(string status, string author, string text)
        {
            GetStatusIndex(out int choiceStatus, status);
            GetAuthorIndex(out int choiceAuthor, author);

            Statuses[choiceStatus].AddCard(new Card(authors[choiceAuthor], text));
            authors[choiceAuthor].AddCard(Statuses[choiceStatus].PipLastAddedCard());
        }
        #endregion
        
        #region Work with Status
        public void WorkWithStatus()
        {
            while (true)
            {
                byte choice = StartMenu.Choiсe(Name, "Add Status", "Del Status", "Print all Statuses");

                if (choice == 0) break;

                else if (choice == 1) AddStatus(Status.ReadConsole());
                else if (Statuses.Count == 0)
                {
                    StartMenu.EnterClearConsole("Status count is null");
                    continue;
                }
                else if (choice == 2) DelStatusConsole();
                else if (choice == 3) Print();


                StartMenu.EnterClearConsole();
            }
        }

        public void ChangeStatusOnCardConsole()
        {
            byte oldChoice = StartMenu.Choiсe("Old card statuses", Statuses);
            if (Statuses[oldChoice].Cards.Count == 0)
            {
                Console.WriteLine("None cards in status");
                return;
            }
            int choiceNewStatus = StartMenu.Choiсe("New card status", Statuses);
            Statuses[choiceNewStatus].AddCard(Statuses[oldChoice].PopCard());
            OnCardStatusChanged(Statuses[choiceNewStatus].ToString());
        }

        public void DelStatusConsole()
        {
            Statuses.RemoveAt(StartMenu.Choiсe("Del Status", Statuses));
        }

        public void TravelToStatusConsole()
        {
            Console.Clear();
            Statuses[StartMenu.Choiсe("Travel", Statuses)].ManagerConsole();
        }
        #endregion

        #region Get Index
        void GetStatusIndex(out int statusIndex)
        {
            statusIndex = StartMenu.Choiсe("Choice card status", Statuses, "Status");
            if (statusIndex == -1) AddStatus(ref statusIndex);
        }

        void GetStatusIndex(out int statusIndex, string statusName)
        {
            statusIndex = Statuses.FindIndex(x => x.Equals(statusName));
            if (statusIndex == -1) AddStatus(ref statusIndex, statusName);
        }

        void GetAuthorIndex(out int authorIndex)
        {
            authorIndex = StartMenu.Choiсe("Choice card author", authors, "Author");
            if (authorIndex == -1) AddAuthor(ref authorIndex);
        }

        void GetAuthorIndex(out int authorIndex, string authorName)
        {
            authorIndex = authors.FindIndex(x => x == authorName);
            if (authorIndex == -1) AddAuthor(ref authorIndex, authorName);
        }
        #endregion

        #region Work with Events
        void OnCardCreated()
        {
            Console.Clear();
            CardCreated();
            StartMenu.Enter();
        }

        void OnCardStatusChanged(string changedStatus)
        {
            Console.Clear();
            CardStatusChanged(changedStatus);
            StartMenu.Enter();
        }
        #endregion

        public void DelCard()
        {
            Statuses[StartMenu.Choiсe("Choice Status from delete", Statuses)].DelCardConsole();
        }

        public static Board ReadConsole()
        {
            return new Board(ConsoleRead.String("Input board name: "));
        }

        public void ManagerConsole()
        {
            Dictionary<byte, string> nameChoice = new Dictionary<byte, string>
            {
                [1] = "Work with Status",
                [2] = "Change name Board",
                [3] = "Add Card",
                [4] = "Del Card",
                [5] = "Print all Statuses",
                [6] = "Change card Status"
            };

            while (true)
            {
                byte choice = StartMenu.Choiсe(Name, nameChoice);

                if (choice == 0) break;

                else if (choice == Dict.KeyByValue(nameChoice, "Work with Status")) WorkWithStatus();
                else if (choice == Dict.KeyByValue(nameChoice, "Change name Board")) Name = ConsoleRead.String("Input new name Board: ");
                else if (Statuses.Count == 0)
                {
                    StartMenu.EnterClearConsole("Status count is null");
                    continue;
                }
                else if (choice == Dict.KeyByValue(nameChoice, "Add Card")) AddCard();
                else if (choice == Dict.KeyByValueLinq(nameChoice, "Print all Statuses")) Print();
                else if (choice == Dict.KeyByValueLinq(nameChoice, "Change card Status")) ChangeStatusOnCardConsole();
                else if (choice == Dict.KeyByValueLinq(nameChoice, "Del Card")) DelCard();

                Console.Clear();
            }
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public void Print()
        {
            Console.WriteLine($"\n\t\t{Name}\n");

            foreach (var status in Statuses)
            {
                status.Print();
            }
        }
    }
}
