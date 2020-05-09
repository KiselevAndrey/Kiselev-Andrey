using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Kiselev_Andrey;

namespace HW_9
{
    [Serializable]
    class Board
    {
        public string Name { get; private set; }
        public List<Status> Statuses { get; private set; }
        public List<Author> authors;

        //public delegate void CreateCard();
        //public delegate void ChangeCardStatus(string changedStatus);
        public delegate void ChangeBoard(string textMessage);

        //public event CreateCard CardCreated;
        //public event ChangeCardStatus CardStatusChanged;
        public event ChangeBoard BoardChanged;

        public Board(string name)
        {
            Name = name;
            Statuses = new List<Status>();
            authors = new List<Author>();
        }

        #region Add
        public void AddStatus()
        {
            AddStatus(new Status(ConsoleRead.String("Input name new status: ")));
        }

        public void AddStatus(Status status)
        {
            Statuses.Add(status);
            BoardChanged($"Added status \"{status.Name}\"");
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

        void AddAuthor(string authorName)
        {
            authors.Add(new Author(authorName));
            BoardChanged($"Added author \"{authorName}\"");
            //int posNewAuthor = authors.Count - 1;
            //CardCreated += authors[posNewAuthor].NotificationCreateCard;
            //CardStatusChanged += authors[posNewAuthor].NotificationChangeCardStatus;
        }

        void AddAuthor(ref int posNewAuthor)
        {
            AddAuthor(ConsoleRead.String("Input name new author: "));
            posNewAuthor = authors.Count - 1;
        }

        void AddAuthor(ref int posNewAuthor, string authorName)
        {
            AddAuthor(authorName);
            posNewAuthor = authors.Count - 1;
        }

        public void AddCard()
        {
            GetStatusIndex(out int choiceStatus);
            Console.Clear();
            GetAuthorIndex(out int choiceAuthor);

            Statuses[choiceStatus].AddCard(new Card(authors[choiceAuthor], ConsoleRead.String("Input card text: ")));
            //authors[choiceAuthor].AddCard(Statuses[choiceStatus].PipLastAddedCard());
            OnCardCreated($"{authors[choiceAuthor]} created card");
        }

        public void AddCard(string status, string author, string text)
        {
            GetStatusIndex(out int choiceStatus, status);
            GetAuthorIndex(out int choiceAuthor, author);

            Statuses[choiceStatus].AddCard(new Card(authors[choiceAuthor], text));
            //authors[choiceAuthor].AddCard(Statuses[choiceStatus].PipLastAddedCard());
        }
        #endregion
        
        #region Work with Status
        public void WorkWithStatus()
        {
            while (true)
            {
                byte choice = StartMenu.Choiсe(Name, "Add Status", "Del Status", "Print all Statuses");

                if (choice == 0) break;

                else if (choice == 1) AddStatus();
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
            Card tempCard = Statuses[oldChoice].PopCard();
            Statuses[choiceNewStatus].AddCard(tempCard);
            OnCardStatusChanged($"Card (Author \"{tempCard.AuthorName}\") change status with \"{Statuses[oldChoice].Name}\" on \"{Statuses[choiceNewStatus].Name}\"");
        }

        public void DelStatusConsole()
        {
            int choice = StartMenu.Choiсe("Del Status", Statuses);
            BoardChanged($"Status \"{Statuses[choice].Name}\" is removed");
            Statuses.RemoveAt(choice);
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
        void OnCardCreated(string textCreatedCard)
        {
            Console.Clear();
            //CardCreated();
            BoardChanged(textCreatedCard);
            StartMenu.Enter();
        }

        void OnCardStatusChanged(string textChangedStatus)
        {
            Console.Clear();
            //CardStatusChanged(changedStatus);
            BoardChanged(textChangedStatus);
            StartMenu.Enter();
        }
        #endregion

        public void DelCard()
        {
            int choice = StartMenu.Choiсe("Choice Status from delete", Statuses);
            Card temp = Statuses[choice].PopCard();
            BoardChanged($"Card (Author \"{temp.AuthorName}\") remove from status \"{Statuses[choice].Name}\"");
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

                if (choice == 0)
                {
                    BoardChanged("Returned to Trello");
                    break;
                }


                else if (choice == Dict.KeyByValue(nameChoice, "Work with Status")) WorkWithStatus();
                else if (choice == Dict.KeyByValue(nameChoice, "Change name Board"))
                {
                    string oldName = Name;
                    Name = ConsoleRead.String("Input new name Board: ");
                    BoardChanged($"Board name changed with \"{oldName}\" on \"{Name}\"");
                }
                else if (choice == Dict.KeyByValue(nameChoice, "Add Card")) AddCard();
                else if (Statuses.Count == 0)
                {
                    StartMenu.EnterClearConsole("Status count is null");
                    continue;
                }
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

            BoardChanged($"Printed all Statuses from Board \"{Name}\"");

            StartMenu.Enter();
        }
    }
}
