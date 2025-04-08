using Zadanie9.Forms;
using Zadanie9.Models;
using System;
using System.Windows.Forms;
using Zadanie.Models;

namespace Zadanie9
{
    internal static class Program
    {
        public static ShopModel context = new ShopModel();
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "Goods catalogue loading";
            Console.WriteLine("Loading");

            System.Threading.Thread.Sleep(1000);

            if (!context.Database.Exists())
            {
                MessageBox.Show("Unable to establish a connection to the database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IntPtr handle = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
            if (handle != IntPtr.Zero)
            NativeMethods.ShowWindow(handle, NativeMethods.SW_HIDE);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new fmLogin());
        }

        private static class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            public const int SW_HIDE = 0;
        }
    }
}
