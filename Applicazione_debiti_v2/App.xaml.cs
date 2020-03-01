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
            this.MainWindow = new MainWindowView(new ViewModels.ViewModelMainWindow());
            this.MainWindow.Show();
        
        }
    }
}
