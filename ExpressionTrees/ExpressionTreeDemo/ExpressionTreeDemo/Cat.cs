﻿namespace ExpressionTreeDemo
{
    public class Cat
    {
        public Cat()
        {

        }
        public Cat(string name)
        {
            this.Name = name;
            this.SomeProperty = "Very Important value";
        }
        public string Name { get; set; }

        public Owner Owner { get; set; }

        private string SomeProperty { get; set; }

        public string SayMew(int number)
        {
            return number.ToString();
        }
    }
}
