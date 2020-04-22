using System;
using System.Collections.Generic;
using System.Text;

namespace HW_11
{
    abstract public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                var temp = (Person)obj;
                return (Name == temp.Name) && (Surname == temp.Surname);
            }
        }

        public override string ToString()
        {
            return $"My name is {Name} {Surname}. I'm Person";
        }
    }
}
