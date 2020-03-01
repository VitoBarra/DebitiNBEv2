using AppDebitiV2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AppDebitiV2.ViewModels
{
    public class ViewModelMainWindow : BaseViewModel
    {

        public string Title { get; set; }
        private IUserData userData;

        private bool menuVisibility;
        public bool MenuVisibility 
        { 
            get => menuVisibility;
            set { menuVisibility = value; Notify(); }
            
        }

        public RelayCommand OpenFirendList { get; private set; }
        public RelayCommand OpenLookRequest { get; private set; }
        public RelayCommand OpenAddRequest { get; private set; }
        public RelayCommand OpenPaga { get; private set; }


        public ViewModelMainWindow() : this(new UserData("null","-1"))
        {
            if (IsDesignMode)
                Title = "design mode";
            else
                Title = "non design";
        }
        public ViewModelMainWindow(IUserData userData) 
        {
            this.userData = userData;
           
        }


        #region OpenWindows

        private void ListViewItem_AddFriend(object sender, MouseButtonEventArgs e)
        {
            AddFriend request = new AddFriend();
            request.ShowDialog();
        }

        private void ListViewItem_LookRequest(object sender, MouseButtonEventArgs e)
        {
            Richieste richieste = new Richieste();
            richieste.ShowDialog();
        }
        private void ListViewItem_AddRequest(object sender, MouseButtonEventArgs e)
        {
            AddRichiesta request = new AddRichiesta();
            request.ShowDialog();
        }


        private void ListViewItem_Paga(object sender, MouseButtonEventArgs e)
        {
            Paga paga = new Paga();
            paga.ShowDialog();
        }

        #endregion

    }
}
