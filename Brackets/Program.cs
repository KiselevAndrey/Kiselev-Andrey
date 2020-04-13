using System;
using Kiselev_Andrey;
using System.Collections.Generic;

namespace Brackets
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                byte choice = StartMenu.Choiсe("Brackets", "Check brackets accuracy");
                if (choice == 0) break;

                else if (choice == 1) Accuracy();

                StartMenu.EnterClearConsole();
            }
        }

        static void Accuracy()
        {
            Dictionary<string, char[]> braсkets = new Dictionary<string, char[]>();

            string round = "round";
            string square = "square";
            string figured = "figured";

            braсkets[round] = new char[2] { '(', ')' };
            braсkets[square] = new char[2] { '[', ']' };
            braсkets[figured] = new char[2] { '{', '}' };

            string str = ConsoleRead.String("Input sting with braсkets: ");

            Console.WriteLine();

            Dictionary<string, int> counts = new Dictionary<string, int>();
            Dictionary<string, Stack<int>> open_brackets = new Dictionary<string, Stack<int>>();
            Dictionary<string, Stack<int>> error_brackets = new Dictionary<string, Stack<int>>();

            foreach (var bracket_name in braсkets.Keys)
            {
                counts[bracket_name] = 0;
                open_brackets[bracket_name] = new Stack<int>();
                error_brackets[bracket_name] = new Stack<int>();
            }

            for (int i = 0; i < str.Length; i++)
            {
                foreach (var bracket in braсkets)
                {
                    if (str[i] == bracket.Value[0])
                    {
                        counts[bracket.Key]++;
                        open_brackets[bracket.Key].Push(i);
                    }

                    else if (str[i] == bracket.Value[1])
                    {
                        counts[bracket.Key]--;
                        if (!open_brackets[bracket.Key].TryPop(out int res))
                        {
                            error_brackets[bracket.Key].Push(i);
                        }
                    }
                }
            }

            bool flag = true;
            foreach (var count in counts)
            {
                if (count.Value != 0)
                {
                    Console.WriteLine($"In _{count.Key}_ brakets is no balance");
                    flag = false;
                }
            }

            if (flag)
            {
                Console.WriteLine("\nBalance brakets is correctly");
            }

            Console.WriteLine();

            foreach (var bracket in open_brackets)
            {
                if (bracket.Value.Count > 0)
                {
                    Console.WriteLine($"Error position _{braсkets[bracket.Key][0]}_ brakets : {string.Join(", ",bracket.Value.ToArray())}");
                }
            }

            Console.WriteLine();

            foreach (var bracket in error_brackets)
            {
                if (bracket.Value.Count > 0)
                {
                    Console.WriteLine($"Error position _{braсkets[bracket.Key][1]}_ brakets : {string.Join(", ", bracket.Value.ToArray())}");
                }
            }
        }
    }
}
