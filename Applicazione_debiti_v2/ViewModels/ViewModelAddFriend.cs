using AppDebitiV2.Views;
using debitiNBEService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace AppDebitiV2.ViewModels
{
    public class ViewModelAddFriend : BaseViewModel, IDataErrorInfo
    {

        public string Error { get; private set; }

        public string this[string PropertyName]
        {
            get
            {
                string ErrorMessage = null;


                switch (PropertyName)
                {
                    case nameof(TextValue):
                        {
                            string str = TextValue;
                            if (!string.IsNullOrEmpty(str)  && Regex.IsMatch(str, "[^0-9.]"))
                                {
                                    TextValue = Regex.Replace(str, "[^0-9.]", "");
                                    ErrorMessage = "l ID non puo contenere lettere";
                                }
                            return ErrorMessage;
                        }
                    default:
                        return null;
                }

            }


        }




        private string _textValue = null;

        public string TextValue
        {
            get => _textValue;
            set
            {
                _textValue = value;
                Notify();
                AddFriendRequest.RaiseCanExecuteChanged();
            }
        }





        private bool _NotifyMesageVis = false;
        public bool NotifyMesageVis
        {
            get => _NotifyMesageVis;
            set
            {
                _NotifyMesageVis = value;
                Notify();
                AddFriendRequest.RaiseCanExecuteChanged();
            }
        }

        private string _LaberlContent = "PALCE HOLDER";

        public string LaberlContent
        {
            get => _LaberlContent;
            set
            {
                _LaberlContent = value;
                Notify();
            }
        }



        public RelayCommand AddFriendRequest { get; private set; }



        public ViewModelAddFriend()
        {
            AddFriendRequest = new RelayCommand(
                p =>
                {
                    int value = int.Parse(TextValue);

                    int httpResp;
                    if (MainWindowView.vm.LoggedUserdata.ID != value)
                        httpResp = int.Parse(HttpEmulator.PostFriendRequest(MainWindowView.vm.LoggedUserdata.ID, value));
                    else
                        httpResp = 3;

                    switch (httpResp)
                    {
                        case 0:
                            LaberlContent = "richieste effetuata";
                            goto default;
                        case 1:
                            LaberlContent = "richiesta gia esistente";
                            goto default;
                        case 2:
                            LaberlContent = "utente inesistente";
                            goto default;
                        case 3:
                            LaberlContent = "non pui fare una richiesta a te stesso";
                            goto default;

                        default:
                            NotifyMesageVis = true;
                            break;
                    }
                },
                p => !string.IsNullOrEmpty(TextValue));
        }

    }
}
