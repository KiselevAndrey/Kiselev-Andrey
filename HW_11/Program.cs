using System;

namespace HW_11
{
    class Program
    {
        static void Main(string[] args)
        {
            Person[] persons = new Person[5]
            {
                new Teacher("Ярослав", "Беляк", "Абстракции"),
                new Student("Кирилл", "Туровский", "Княжеводчества"),
                new Teacher("Сабина", "Бабаева", "Аудита"),
                new Student("Кирилл", "Туровский", "Княжеводчества"),
                new HeadTeacher("Ярослав", "Беляк", "Абстракции"),
            };

            for (int i = 0; i < persons.Length; i++)
            {
                Console.WriteLine(persons[i]);
            }

            Console.WriteLine();

            for (int i = 0; i < persons.Length - 1; i++)
            {
                for (int j = i+1; j < persons.Length; j++)
                {
                    Console.WriteLine(persons[i]);
                    Console.WriteLine(persons[j]);
                    Console.WriteLine("We are " + (persons[i].Equals(persons[j]) ? "" : "don't ") + "equals.\n");
                }
            }

            Console.ReadLine();
        }
    }
}
