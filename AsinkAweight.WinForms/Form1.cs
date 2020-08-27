using AsinkAweight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsinkAweight.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ActionScheduler.Current = new WindowsMessageQueueScheduler();
        }

        bool deadlockDemo = false;

        private void startButton_Click(object sender, EventArgs e)
        {
            if (deadlockDemo)
            {
                DeadlockDemo();
            }
            else
            {
                DeadlockDemoFix();
            }

            deadlockDemo = !deadlockDemo;
        }

        private void DeadlockDemo()
        {
            WriteLine("What is your name?");
            var name = GetFormattedNameIOAsink().GetAwaiter().GetResult();
            WriteLine($"Hello {name}!");
        }

        private void DeadlockDemoFix()
        {
            WriteLine("What is your name?");
            var name = GetFormattedNameIOAsinkFixed().GetAwaiter().GetResult();
            WriteLine($"Hello {name}!");
        }


        private void WriteLine(string text)
        {
            textOutput.Text += text + "\r\n";
            // this is a cheat so we can see the prompt!
            Application.DoEvents();
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

        static private async Tusk<string> GetFormattedNameIOAsink()
        {
            var name = await GetNameIOAsink();
            return FormattedName(name);
        }

        static private async Tusk<string> GetFormattedNameIOAsinkFixed()
        {
            var name = await GetNameIOAsink().UseDefaultScheduler();
            return FormattedName(name);
        }

        private static string FormattedName(string name)
        {
            var capital = true;

            var array = new char[name.Length];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = capital ? char.ToUpper(name[i]) : char.ToLower(name[i]);
                capital = char.IsWhiteSpace(array[i]);
            }

            return new string(array);
        }
    }
}
