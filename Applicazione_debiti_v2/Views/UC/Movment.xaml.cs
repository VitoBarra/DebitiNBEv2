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
    /// Logica di interazione per Movment.xaml
    /// </summary>
    public partial class Movment : UserControl
    {

        #region DependecyProperty

        public static readonly DependencyProperty CreditProperty =
            DependencyProperty.Register("Credit", typeof(string), typeof(Movment), new PropertyMetadata("0"));
        public string Credit
        {
            get { return (string)GetValue(CreditProperty); }
            set { SetValue(CreditProperty, value); }
        }



        public static readonly DependencyProperty UsernameProperty =
            DependencyProperty.Register("Username", typeof(string), typeof(Movment), new PropertyMetadata("PlaceHolder"));
        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        #endregion


        public Movment()
        {
            InitializeComponent();
        }
    }
}
