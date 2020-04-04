using System;
using System.Collections.Generic;
using Kiselev_Andrey;

namespace Priority_Queue
{
    class Program
    {
        static int Num_queue { get; set; }
        static int Num_prior_queue { get; set; }

        static void Main(string[] args)
        {
            Random rand = new Random();
            Num_queue = rand.Next(50, 100);
            Num_prior_queue = rand.Next(20, 100);

            Queue<string> patients = new Queue<string>();
            Queue<string> prior_patients = new Queue<string>();

            while (true)
            {
                byte choise = StartMenu.Choise("Priotity Queue", "Take ticket", "Next patient");
                if (choise == 0) break;

                switch (choise)
                {
                    case 1:
                        AddPatient(ref patients, ref prior_patients);
                        break;

                    case 2:
                        NextPatient(ref patients, ref prior_patients);
                        break;

                    default:
                        Console.WriteLine("Error choise");
                        break;
                }

                StartMenu.EnterClearConsole();
            }
        }

        static void AddPatient(ref Queue<string> queue, ref Queue<string> prior_queue)
        {
            byte choise = StartMenu.Choise("Take ticket", "Doctor survey", "Hight temperure");
            if (choise == 0) return;

            switch (choise)
            {
                case 1:
                    TakeTicket($"Q{Num_queue++}", ref queue);
                    Console.WriteLine($"You are {queue.Count + prior_queue.Count} patient in queue");
                    break;

                case 2:
                    TakeTicket($"PQ{Num_prior_queue++}", ref prior_queue);
                    Console.WriteLine($"You are {prior_queue.Count} patient in queue");
                    break;

                default:
                    Console.WriteLine("Error choise");
                    break;
            }
        }

        static void TakeTicket(string num_ticket, ref Queue<string> queue)
        {
            queue.Enqueue(num_ticket);
            Console.WriteLine($"Your ticket num is <{num_ticket}>");
        }

        static void NextPatient(ref Queue<string> queue, ref Queue<string> prior_queue)
        {
            if (prior_queue.Count > 0) 
            {
                Console.WriteLine($"Next patient {prior_queue.Dequeue()}");
            }
            else if (queue.Count > 0)
            {
                Console.WriteLine($"Next patient {queue.Dequeue()}");
            }
            else
            {
                Console.WriteLine("No patient");
            }
        }
    }
}
