using System;

namespace Inheritance
{
    // Lớp cha
    public class Animal
    {
        public string Name { get; set; }

        public virtual void MakeSound()
        {
            Console.WriteLine("Animal makes a sound");
        }
    }

    // Lớp con kế thừa
    public class Dog : Animal
    {
        public override void MakeSound()
        {
            Console.WriteLine("Woof! Woof!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Animal a = new Animal();
            a.Name = "Some Animal";
            a.MakeSound();

            Dog d = new Dog();
            d.Name = "Buddy";
            d.MakeSound();
        }
    }
}
