using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppDebitiV2.Views;
using debitiNBEService;

namespace AppDebitiV2
{
    /// <summary>
    /// Logica di interazione per App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            HttpEmulator.DatabaseConnection();
            MainWindow = new MainWindowView(new ViewModels.ViewModelMain());



            if (Global.loggedState)
                MainWindow.Show();
            else
                MainWindow.Close();

        }
    }
}
