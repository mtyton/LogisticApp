using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.ViewModel.BaseClass;
using LogisticApp.Model;
using System.ComponentModel;

namespace LogisticApp.ViewModel.Forms
{
    class OrderFormViewModel : BaseViewModel
    {
        public BaseViewModel orderFormViewModel = new OrderFormViewModel();

        public event PropertyChangedEventHandler PropertyChanged;

        private string jobTitle;

        public string JobTitle
        {
            get => jobTitle;
            set
            {
                jobTitle = value;
                PropertyChanged?.Invoke(
                    this, new PropertyChangedEventArgs(nameof(JobTitle))
                    );
            }
        }
    }
}
