using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UMS_System_2._0.Repositories;
using UMS_System_2._0.Views;

namespace UMS_System_2._0
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
#if NETCOREAPP3_0_OR_GREATER
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ✅ Step 3: Initialize database from .sql files
            DatabaseInitializer.InitializeDatabase();

            Application.Run(new LoginForm());
        }
    }
}
