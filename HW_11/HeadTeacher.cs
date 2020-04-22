using System;
using System.Collections.Generic;
using System.Text;

namespace HW_11
{
    public class HeadTeacher : Teacher
    {
        public HeadTeacher(string name, string surname, string departament) : base(name, surname, departament) { }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return $"My name is {Name} {Surname}.\tI'm Head Teacher in facultet {Departament}";
        }
    }
}
