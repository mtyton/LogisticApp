using LogisticApp.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.ViewModel.Utils
{
    class WindowMediator
    {
        // this class is a singleton we need only one mediator 
        // to communicate between windows

        MainMenuViewModel mainMenuViewModel;

        private static WindowMediator _instance;

        private WindowMediator(MainMenuViewModel vm)
        {
            mainMenuViewModel = vm;
        }

        public void closeSubWindow()
        {
            if (mainMenuViewModel.CloseSubWindow.CanExecute(null))
            {
                mainMenuViewModel.CloseSubWindow.Execute(null);
            }
        }

        public static WindowMediator getMediator(MainMenuViewModel vm)
        {
            if (_instance == null)
            {
                _instance = new WindowMediator(vm);
            }
            return _instance;
        }

    }
}
