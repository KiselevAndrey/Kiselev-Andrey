using System;
using Kiselev_Andrey;

namespace GCD
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                byte choise = StartMenu.Choise("Greatest Common Divisor", "Euclidean algorithm", "Binary GCD algorithm");
                if (choise == 0) break;

                Console.Clear();
                
                switch (choise)
                {
                    case 1:
                        EuclideanAlgorithm();
                        break;

                    case 2:
                        BinaryAlgorithm();
                        break;

                    default:
                        break;
                }

                StartMenu.EnterClearConsole();
            }
        }

        static void EuclideanAlgorithm()
        {
            Console.WriteLine("Euclidean Algorithm");
            int a = ConsoleRead.Int("Input first num: ");
            int b = ConsoleRead.Int("Input second num: ");
            Console.WriteLine($"\n\tGCD {a} & {b} = {EuclidGCD(a, b)}");
        }

        static int EuclidGCD(int a, int b)
        {
            return b == 0 ? Math.Abs(a) : EuclidGCD(b, a % b);
        }

        static void BinaryAlgorithm()
        {
            Console.WriteLine("Binary Algorithm");
            int a = ConsoleRead.Int("Input first num: ");
            int b = ConsoleRead.Int("Input second num: ");
            Console.WriteLine($"\n\tGCD {a} & {b} = {BinaryGCD(a, b)}");
        }

        static int BinaryGCD(int a, int b)
        {
            // алгоритм взят с https://ru.wikipedia.org/wiki/%D0%91%D0%B8%D0%BD%D0%B0%D1%80%D0%BD%D1%8B%D0%B9_%D0%B0%D0%BB%D0%B3%D0%BE%D1%80%D0%B8%D1%82%D0%BC_%D0%B2%D1%8B%D1%87%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%B8%D1%8F_%D0%9D%D0%9E%D0%94
            if (a == 0) return b;
            if (b == 0) return a;
            if (a == b) return a;
            if (a == 1 || b == 1) return 1;
            if ((a & 1) == 0)       // то же самое что и a%2==0, только быстрее
            {
                return ((b & 1) == 0) ?                 // если b - четн
                    BinaryGCD(a >> 1, b >> 1) << 1 :    // то ф-ла 2*GCD(a/2,b/2)
                    BinaryGCD(a >> 1, b);               // иначе ф-ла GCD(a/2,b)
            }
            else                                    // a - нечетн
            {
                return ((b & 1) == 0) ?             // если b - четн     
                    BinaryGCD(a, b >> 1) :          // то ф-ла GCD(a,a/2)
                    (a > b) ?                       // если оба нечетн, то ещё проверка
                    BinaryGCD((a - b) >> 1, b) :    // то ф-ла GCD((a-b)/2,b)
                    BinaryGCD((b - a) >> 1, a);     // иначе ф-ла GCD((b-a)/2,a)
            }
        }
    }
}
