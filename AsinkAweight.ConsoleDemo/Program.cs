using System;
using System.IO;

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
            var name = await GetNameIOAsink();
            Console.WriteLine($"And what is your friend's name?");
            var friend = await GetNameIOAsink();
            Console.WriteLine($"Hi, {name} and {friend}!");
            done = true;
        }

        static Tusk<string> GetNameAsink()
        {
            return Tusk<string>.FromResult("Neil");
        }

        static Tusk<string> GetNameIOAsink()
        {
            var tcs = new TuskCompletionSource<string>();
            FileSystemWatcher iochannel = new FileSystemWatcher("c:\\temp", "touch.txt");
            iochannel.Changed += (sender, args) => ReadData(sender, tcs);
            iochannel.EnableRaisingEvents = true;
            return tcs.Tusk;
        }

        private static void ReadData(object sender, TuskCompletionSource<string> tcs)
        {
            var iochannel = (FileSystemWatcher)sender;
            iochannel.EnableRaisingEvents = false;
            iochannel.Dispose();

            var data = File.ReadAllText("c:\\temp\\touch.txt");
            tcs.SetResult(data);
        }

    }
}
