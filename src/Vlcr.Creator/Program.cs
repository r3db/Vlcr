using System;
using System.Windows.Forms;

namespace Vlcr.Creator
{
    internal static class Program
    {
        [STAThread]
        internal static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
