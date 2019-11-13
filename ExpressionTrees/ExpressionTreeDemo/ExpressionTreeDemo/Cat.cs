using System;
using System.Collections.Generic;
using System.Text;

namespace ExpressionTreeDemo
{
    public class Cat
    {
        public Cat()
        {

        }
        public Cat(string name)
        {
            this.Name = name;
        }
        public string Name { get; set; }

        public Owner Owner { get; set; }

        public string SayMew(int number)
        {
            return number.ToString();
        }
    }
}
