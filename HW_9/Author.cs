using System;
using System.Collections.Generic;
using System.Text;

namespace HW_9
{
    class Author
    {
        public string Name { get; private set; }
        public List<Card> Cards { get; private set; }

        public Author(string name)
        {
            Name = name;
            Cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            Cards.Add(card);
        }

        #region Work with Events
        public void NotificationCreateCard()
        {
            Console.WriteLine($"{Name} recieved notification by create card");
        }

        public void NotificationChangeCardStatus(string changedStatus)
        {
            Console.WriteLine($"{Name} recieved notification by card status changed by {changedStatus}");
        }
        #endregion

        public override string ToString()
        {
            return Name;
        }

        public static bool operator ==(Author author, string name_author)
        {
            return name_author == author.Name;
        }

        public static bool operator !=(Author author, string name_author)
        {
            return name_author != author.Name;
        }
    }
}
