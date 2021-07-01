using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LogisticApp.ViewModel.BaseClass;
using LogisticApp.Model;
using System.Windows;
using LogisticApp.Views;

namespace LogisticApp.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string currentUsername;

        public string CurrentUsername
        {
            get => currentUsername;
            set
            {
                currentUsername = value;
                PropertyChanged?.Invoke(
                    this, new PropertyChangedEventArgs(nameof(CurrentUsername))
                    );
            }
        }

        public bool authenticate(object param)
        {
            User user = User.getUser();
            if (!user.IsAuthenticated)
            {
                MessageBox.Show(
                    "Your username or password did not match",
                    "AuthError", MessageBoxButton.OK, 
                    MessageBoxImage.Error
                    );
                return false;
            }
            return true;
            
        }

    }

}
