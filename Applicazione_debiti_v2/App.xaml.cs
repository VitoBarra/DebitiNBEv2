using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppDebitiV2.Views;

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

            this.MainWindow = new MainWindowView() ;
            this.MainWindow.DataContext = new ViewModels.ViewModelMainWindow(new UserData("-1", "asd"));
            this.MainWindow.Show();
        }
    }
}
