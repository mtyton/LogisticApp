using LogisticApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Model.Utils
{
    class WindowObserver
    {

        MainMenuViewModel mainMenuViewModel;

        private static WindowObserver _instance;

        private WindowObserver(MainMenuViewModel vm)
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

        public static WindowObserver getObserver(MainMenuViewModel vm)
        {
            if (_instance == null)
            {
                _instance = new WindowObserver(vm);
            }
            return _instance;
        }

    }
}
