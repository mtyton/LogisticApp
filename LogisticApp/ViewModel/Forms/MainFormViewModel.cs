using LogisticApp.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.ViewModel.Forms
{
    class MainFormViewModel : BaseViewModel
    {
        BaseViewModel _selectedViewModel = null;

        public BaseViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                this.onPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public void setViewModel(string name)
        {
            if (name == "company")
            {
                SelectedViewModel = new CompanyForm();
            }
        }

        public void setData(string entityContentString)
        {
           //TODO load record to Update
        }


    }
}
