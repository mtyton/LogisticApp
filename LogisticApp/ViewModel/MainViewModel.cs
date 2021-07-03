using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LogisticApp.ViewModel.BaseClass;
using LogisticApp.Views.Forms;

namespace LogisticApp.ViewModel
{
    class MainViewModel: BaseViewModel
    {

        BaseViewModel _selectedViewModel = new LoginViewModel();

        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                this.onPropertyChanged(nameof(SelectedViewModel));
            }
        }

        #region{commands}
        private ICommand _loginCommand;

        public ICommand LoginCommand => _loginCommand ?? (
            _loginCommand = new RelayCommand(login, canLogin)
        );

        private bool canLogin(object param)
        {
            if (_selectedViewModel is LoginViewModel)
            {
                return true;
            }
            return false;
        }

        private void login(object param)
        {
            LoginViewModel loginViewModel = (LoginViewModel)_selectedViewModel;
            if (loginViewModel.authenticate(param))
            {
                SelectedViewModel = new MainMenuViewModel();
            }
        }
        #endregion
    }
}
