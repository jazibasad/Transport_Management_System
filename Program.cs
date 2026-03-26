using System;
using System.Windows.Forms;

namespace Transport_Management_System
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // This line fixes the "missing content" by telling Windows 
            // to scale the form correctly for your monitor's resolution.
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Bookings());
        }

        // Imports the Windows library to handle High DPI scaling
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}