using System;
using System.Collections.Generic;
using Kiselev_Andrey;

namespace Priority_Queue
{
    class Program
    {
        static int Num_queue { get; set; }

        static void Main(string[] args)
        {
            Num_queue = 0;

            Queue<string> patients = new Queue<string>();

            while (true)
            {
                byte choiсe = StartMenu.Choiсe("Priotity Queue", "Take ticket", "Next patient");
                if (choiсe == 0) break;

                switch (choiсe)
                {
                    case 1:
                        AddPatient(ref patients);
                        break;

                    case 2:
                        NextPatient(ref patients);
                        break;

                    default:
                        Console.WriteLine("Error choise");
                        break;
                }

                StartMenu.EnterClearConsole();
            }
        }

        static void AddPatient(ref Queue<string> queue)
        {
            byte choise = StartMenu.Choiсe("Take ticket", "Doctor survey", "Hight temperure");
            if (choise == 0) return;

            int position;
            switch (choise)
            {
                case 1:
                    TakeTicket($"Q{++Num_queue}", ref queue, out position);
                    Console.WriteLine($"You are {queue.Count} patient in queue");
                    break;

                case 2:
                    TakeTicket($"PQ{++Num_queue}", ref queue, out position);
                    Console.WriteLine($"You are {position} patient in queue");
                    break;

                default:
                    Console.WriteLine("Error choise");
                    break;
            }
        }

        static void TakeTicket(string num_ticket, ref Queue<string> queue, out int position)
        {
            position = 1;
            if (queue.Count == 0)
            {
                queue.Enqueue(num_ticket);
            }
            else
            {
                Queue<string> temp_queue = new Queue<string>();
                bool flag_add_new_patient = false;
                foreach (var patient in queue)
                {
                    if (patient[0] == 'P')
                    {
                        position++;
                        temp_queue.Enqueue(patient);
                        continue;
                    }
                    if (!flag_add_new_patient && num_ticket[0] == 'P')
                    {
                        temp_queue.Enqueue(num_ticket);
                        flag_add_new_patient = true;
                    }
                    temp_queue.Enqueue(patient);
                }
                if (!flag_add_new_patient)
                {
                    temp_queue.Enqueue(num_ticket);
                }
                queue.Clear();
                foreach (var patient in temp_queue)
                {
                    queue.Enqueue(patient);
                }
            }            

            Console.WriteLine($"Your ticket num is <{num_ticket}>");
        }

        static void NextPatient(ref Queue<string> queue)
        {
            if (queue.Count > 0)
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
