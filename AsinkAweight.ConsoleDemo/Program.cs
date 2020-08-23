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
            var thing = await MyDummyAsyncFn();

            done = true;
        }

        static Tusk<string> MyDummyAsyncFn()
        {
            throw new NotImplementedException();
        }
    }
}
