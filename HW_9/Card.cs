using System;
using System.Collections.Generic;
using System.Text;
using Kiselev_Andrey;

namespace HW_9
{
    class Card
    {
        public string NameAuthor { get; private set; }
        public string Text { get; private set; }

        private int maxPrintLength = 15;

        /////////////Dont realisation////////////////
        public DateTime DatePublish { get; private set; }
        public DateTime DueDate { get; private set; }
        /////////////////////////////

        public Card(string author)
        {
            SetNameAuthor(author);
        }

        public Card(string author, string text) : this(author)
        {
            SetText(text);
        }

        public void SetNameAuthor(string author)
        {
            NameAuthor = author;
        }

        public void SetText(string text)
        {
            Text = text;
        }

        public void Print()
        {
            Console.WriteLine($"\t{NameAuthor}\n{Text}");
        }

        public override string ToString()
        {
            string res = $"{ShortText(NameAuthor)}\n{ShortText(Text)}\n";
            return res;
        }

        string ShortText(string text)
        {
            string res = text.Substring(0, Math.Min(text.Length, maxPrintLength));
            if (text.Length > maxPrintLength) res += "\b\b\b...";
            return res;
        }

        static public Card ReadConsole()
        {
            return new Card(ConsoleRead.String("Input name author: "), ConsoleRead.String("Input card text: "));
        }

        public void ManagerConsole()
        {
            while (true)
            {
                byte choice = StartMenu.Choiсe(NameAuthor, "Change author", "Change text");

                if (choice == 0) break;

                else if (choice == 1) NameAuthor = ConsoleRead.String("Input author name: ");
                else if (choice == 2) Text = ConsoleRead.String("Input new text: ");

                StartMenu.EnterClearConsole();
            }
        }
    }
}
