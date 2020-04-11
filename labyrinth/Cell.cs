using System;
using System.Collections.Generic;
using System.Text;

namespace labyrinth
{
    class Cell
    {
        int X { get; set; }
        int Y { get; set; }
        
        public int LeePoint { get; set; }

        readonly Dictionary<string, char> content_name = new Dictionary<string, char>
        { { Info.Path, '\u25A0' }, { Info.Wall, ' ' }, { Info.Way, '\u25A0'}, { Info.Border, '\u25A0'}, { Info.Start, 'S' }, { Info.Finish, 'F' }, { Info.Another, '?' },
            {Info.Theseus, 'T' } };

        public string Content { get; private set; }

        public Cell PrevCell { get; private set; }

        public Cell(int x, int y, string content)
        {
            X = x;
            Y = y;
            SetContent(content);
            SetPrevCell(this);
            LeePoint = -1;
        }

        public Cell() { }

        public void Print()
        {
            if (Content == Info.Border)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else if (Content == Info.Path)
                Console.ForegroundColor = ConsoleColor.White;
            else if (Content == Info.Start || Content == Info.Finish)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (Content == Info.Way)
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            else if (Content == Info.Theseus)
                Console.ForegroundColor = ConsoleColor.Gray;

            Console.Write(content_name[Content]);
            Console.ResetColor();
        }

        public void SetContent(string content)
        {
            Content = content_name.ContainsKey(content) ? content : Info.Another;
        }

        public void SetPrevCell(Cell cell)
        {
            PrevCell = cell;
        }

        public int[] GetCoordinate()
        {
            return new int[2] { X, Y };
        }

        public void WallFromPath()
        {
            if (Content == Info.Wall)
            {
                SetContent(Info.Path);
            }
        }

        public bool CompairCoord(int[] coordin)
        {
            if (X != coordin[0] || Y != coordin[1])
            {
                return false;
            }
            return true;
        }

        public bool CompairContent(string content)
        {
            return content == Content;
        }


        public static bool operator ==(Cell cell1, Cell cell2)
        {
            return cell1.X == cell2.X && cell1.Y == cell2.Y;
        }

        public static bool operator !=(Cell cell1, Cell cell2)
        {
            return cell1.X != cell2.X || cell1.Y != cell2.Y;
        }

    }
}
