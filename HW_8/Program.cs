using System;
using Kiselev_Andrey;

namespace HW_8
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                byte choise = StartMenu.Choiсe("MyString", "Constructions", "Contains", "IndexOf");
                if (choise == 0) break;

                if (choise == 1) Constructors();
                else if (choise == 2) Contains();
                else if (choise == 3) IndexOf();

                StartMenu.EnterClearConsole();
            }
        }
            
        static void Constructors()
        {
            MyString str;

            Console.WriteLine("\n\tConstructors\n");
            StartMenu.Line();

            Console.WriteLine("With repit char\n");
            char c = ConsoleRead.Char("Input char symbol: ");
            int n = ConsoleRead.Int("Input repeat number: ");
            str = new MyString(c, n);
            Console.WriteLine($"\nYour string is: {str}");
            StartMenu.Line();

            Console.WriteLine("With array of char\n");
            n = ConsoleRead.Int("Input array length: ");
            char[] array = new char[n];
            for (int i = 0; i < n; i++)
                array[i] = ConsoleRead.Char("Input char symbol: ");
            str = new MyString(array);
            Console.WriteLine($"\nYour string is: {str}");
            StartMenu.Line();

            Console.WriteLine("With ReadOnlySpan<char>\n");
            str = new MyString(ConsoleRead.String("Input string: "));
            Console.WriteLine($"\nYour string is: {str}");
            StartMenu.Line();

            Console.WriteLine("With Int\n");
            str = new MyString(ConsoleRead.Int("Input number: "));
            Console.WriteLine($"\nYour string is: {str}");
            StartMenu.Line();
        }

        static void Contains()
        {
            Console.WriteLine("\n\tMyString.Contains\n");
            StartMenu.Line();
            MyString str = new MyString(ConsoleRead.String("Input string: "));
            Console.WriteLine("\nSubstring " + (str.Contains(ConsoleRead.String("Input substring: ")) ? "" : "don't ") + "input in string");
        }

        static void IndexOf()
        {
            Console.WriteLine("\n\tMyString.IndexOf\n");
            MyString str = new MyString(ConsoleRead.String("\nInput one string from all IndexOf: "));
            StartMenu.Line();

            Console.WriteLine("MyString.IndexOf(char)\n");
            char c = ConsoleRead.Char("Input char symbol: ");
            Console.WriteLine($"First index of input {c} in string is: {str.IndexOfToMyString(str.IndexOf(c))}");
            StartMenu.Line();

            Console.WriteLine("MyString.IndexOf(char, start_index)\n");
            c = ConsoleRead.Char("Input char symbol: ");
            int start = ConsoleRead.Int("Input start index: ");
            Console.WriteLine($"First index of input {c} in string is: {str.IndexOfToMyString(str.IndexOf(c, start))}");
            StartMenu.Line();

            Console.WriteLine("MyString.IndexOf(string)\n");
            string s = ConsoleRead.String("Input substring: ");
            Console.WriteLine($"First index of input {s} in string is: {str.IndexOfToMyString(str.IndexOf(s))}");
            StartMenu.Line();

            Console.WriteLine("MyString.IndexOf(string, start_index)\n");
            s = ConsoleRead.String("Input substring: ");
            start = ConsoleRead.Int("Input start index: ");
            Console.WriteLine($"First index of input {s} in string is: {str.IndexOfToMyString(str.IndexOf(s, start))}");
        }
    }
}
