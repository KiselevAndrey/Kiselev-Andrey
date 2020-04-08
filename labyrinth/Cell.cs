using System;
using System.Collections.Generic;
using System.Text;

namespace labyrinth
{
    class Cell
    {

        private int x, y;
        readonly Dictionary<string, char> content_name = new Dictionary<string, char>
        { { "path", '\u25A0' }, { "wall", ' ' }, { "way", '*'}, { "border", '\u25A0'}, { "start", 'S' }, { "finish", 'F' }, { "another", '?' } };

        public string Content { get; private set; }

        public Cell PrevCell { get; private set; }

        public Cell(int x, int y, string content)
        {
            this.x = x;
            this.y = y;
            SetContent(content);
            SetPrevCell(this);
        }

        public void Print()
        {
            if (Content == "border")
                Console.ForegroundColor = ConsoleColor.Yellow;
            if (Content == "path")
                Console.ForegroundColor = ConsoleColor.White;
            if (Content == "start" || Content == "finish")
                Console.ForegroundColor = ConsoleColor.Red;

            Console.Write(content_name[Content]);
            Console.ResetColor();
        }

        public void SetContent(string content)
        {
            Content = content_name.ContainsKey(content) ? content : "anoter";
        }

        public void SetPrevCell(Cell cell)
        {
            PrevCell = cell;
        }

        public int[] GetCoordinate()
        {
            return new int[2] { x, y };
        }

        public void WallFromPath()
        {
            if (Content == "wall")
            {
                SetContent("path");
            }
        }

        public bool CompairCoord(int[] coordin)
        {
            if (x != coordin[0] || y != coordin[1])
            {
                return false;
            }
            return true;
        }

        public static bool operator ==(Cell cell1, Cell cell2)
        {
            return cell1.x == cell2.x && cell1.y == cell2.y;
        }

        public static bool operator !=(Cell cell1, Cell cell2)
        {
            return cell1.x != cell2.x || cell1.y != cell2.y;
        }
    }
}
