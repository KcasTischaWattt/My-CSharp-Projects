using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peer4
{
    static class Program
    {
        /// <summary>
        /// Настройки приложения, установленные в момент закрытия приложения
        /// </summary>
        public static AppSettings Settings;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Settings = AppSettings.LoadFromJson();
            Application.Run(new NotePad());
        }
    }
}
