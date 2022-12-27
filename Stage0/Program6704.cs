
// See https://aka.ms/new-console-template for more information

using System;
using System.IO;


namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome6704();
            Welcome7963();

            Console.ReadKey();
        }
        static partial void Welcome7963();
        private static void Welcome6704()
        {
            string name;
            Console.WriteLine("Enter your name:");

            name = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first console application", name);
            ///Hi Ayala
        }
    }
}

