using System;
using Newtonsoft.Json;

namespace HW_9
{
    [Serializable]
    class Card
    {
        public string AuthorName { get; private set; }
        public string Text { get; private set; }

        private readonly int maxPrintLength = 15;
        
        public Card(Author author)
        {
            SetNameAuthor(author);
        }

        public Card(Author author, string text) : this(author)
        {
            SetText(text);
        }

        [JsonConstructor]
        public Card(string authorName, string text)
        {
            AuthorName = authorName;
            Text = text;
        }

        public void SetNameAuthor(Author author)
        {
            AuthorName = author.Name;
        }

        public void SetText(string text)
        {
            Text = text;
        }

        public void Print()
        {
            Console.WriteLine($"\t{AuthorName}\n{Text}\n");
        }

        public override string ToString()
        {
            string res = $"{ShortText(AuthorName.ToString())}\n{ShortText(Text)}\n";
            return res;
        }

        string ShortText(string text)
        {
            string res = text.Substring(0, Math.Min(text.Length, maxPrintLength));
            if (text.Length > maxPrintLength) res += "\b\b\b...";
            return res;
        }
    }
}
