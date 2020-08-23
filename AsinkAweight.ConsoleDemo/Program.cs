using System;

namespace AsinkAweight.ConsoleDemo
{
    class Program
    {
        static bool done = false;

        public static void Main(string[] args)
        {
            AsyncMain();

            while (!done)
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        static async void AsyncMain()
        {
            Console.WriteLine("Hi, what's your name?");
            var name = await GetNameAsink();
            Console.WriteLine($"And what is your friend's name?");
            var friend = await GetNameAsink();
            Console.WriteLine($"Hi, {name} and {friend}!");
            done = true;
        }

        static Tusk<string> GetNameAsink()
        {
            return Tusk<string>.FromResult("Neil");
        }
    }
}
