using System;
using System.Collections.Generic;
using System.Text;

namespace HW_11
{
    public class Teacher : Person
    {
        public string Departament { get; set; }

        public Teacher(string name, string surname, string departament) : base(name, surname)
        {
            Departament = departament;
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
                var temp = (Teacher)obj;
                return (Departament == temp.Departament) ? base.Equals((Person)obj) : false;
            }
        }

        public override string ToString()
        {
            return $"My name is {Name} {Surname}.\tI'm Teacher in facultet {Departament}";
        }
    }
}
