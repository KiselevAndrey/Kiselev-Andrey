using System;
using System.Collections.Generic;
using System.Text;
using Kiselev_Andrey;

namespace HW_9
{
    class Card
    {
        public Author Author { get; private set; }
        public string Text { get; private set; }

        private readonly int maxPrintLength = 15;

        /////////////Dont realisation////////////////
        public DateTime DatePublish { get; private set; }
        public DateTime DueDate { get; private set; }
        /////////////////////////////

        public Card(Author author)
        {
            SetNameAuthor(author);
        }

        public Card(Author author, string text) : this(author)
        {
            SetText(text);
        }

        public void SetNameAuthor(Author author)
        {
            Author = author;
        }

        public void SetText(string text)
        {
            Text = text;
        }

        public void Print()
        {
            Console.WriteLine($"\t{Author}\n{Text}\n");
        }

        public override string ToString()
        {
            string res = $"{ShortText(Author.ToString())}\n{ShortText(Text)}\n";
            return res;
        }

        string ShortText(string text)
        {
            string res = text.Substring(0, Math.Min(text.Length, maxPrintLength));
            if (text.Length > maxPrintLength) res += "\b\b\b...";
            return res;
        }
        
        //public void ManagerConsole()
        //{
        //    while (true)
        //    {
        //        byte choice = StartMenu.Choiсe(Author.ToString(), "Change author", "Change text");

        //        if (choice == 0) break;

        //        //else if (choice == 1) Author = new Author(ConsoleRead.String("Input author name: "));
        //        else if (choice == 2) Text = ConsoleRead.String("Input new text: ");

        //        StartMenu.EnterClearConsole();
        //    }
        //}
    }
}
