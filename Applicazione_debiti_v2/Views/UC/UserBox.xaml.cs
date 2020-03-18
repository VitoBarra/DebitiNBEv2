using AppDebitiV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppDebitiV2.Views.UC
{
    /// <summary>
    /// Logica di interazione per UserBox.xaml
    /// </summary>
    public partial class UserBox : UserControl
    {
        #region DependecyProperty

        public static readonly DependencyProperty usernameProperty =
            DependencyProperty.Register("username", typeof(string), typeof(UserBox), new PropertyMetadata("PlaceHolder"));
        public string username
        {
            get { return (string)GetValue(usernameProperty); }
            set { SetValue(usernameProperty, value); }
        }


        public string ID
        {
            get { return (string)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public static readonly DependencyProperty IDProperty =
            DependencyProperty.Register("ID", typeof(string), typeof(UserBox), new PropertyMetadata("Plahol"));



        #endregion




        public UserBox()
        {
            InitializeComponent();
        }
    }
}
