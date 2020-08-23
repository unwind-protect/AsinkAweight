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

        private async void startButton_Click(object sender, EventArgs e)
        {
            WriteLine("What is your name?");
            var name1 = await GetNameIOAsink();
            WriteLine("And what is your friend's name?");
            var name2 = await GetNameIOAsink();
            WriteLine($"Hello {name1} and {name2}!");
        }

        private void WriteLine(string text)
        {
            textOutput.Text += text + "\r\n";
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
