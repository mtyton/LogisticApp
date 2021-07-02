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
using System.Security.Authentication;
using System.Windows.Controls;

namespace LogisticApp.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        private string currentUsername;

        public string CurrentUsername
        {
            get => currentUsername;
            set
            {
                currentUsername = value;
                onPropertyChanged(nameof(CurrentUsername));
            }
        }

        public bool authenticate(object param)
        {
            PasswordBox box = (PasswordBox)param;
            Session session = null;
            try
            {
                session = Session.getOrCreateSession(this.currentUsername, box.Password.ToString());
            }
            catch(AuthenticationException e)
            {
                MessageBox.Show(
                    e.Message, "AuthError",
                    MessageBoxButton.OK, MessageBoxImage.Error
                    );
                return false;
            }
            

            if (!session.IsAuthenticated)
            {
                MessageBox.Show(
                    "Username or password was incorrect",
                    "AuthError", MessageBoxButton.OK, 
                    MessageBoxImage.Error
                    );
                return false;
            }
            return true;
            
        }

    }

}
