using LogisticApp.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.ViewModel.Forms
{
    class CompanyForm : BaseViewModel
    {
        AddressViewModel _addressViewModel;

        public AddressViewModel AddrViewModel
        {
            get => _addressViewModel;
            set
            {
                _addressViewModel = value;
                onPropertyChanged(nameof(AddrViewModel));
            }
        }
        string _companyName;
        string _taxNumber;

        public CompanyForm()
        {
            AddrViewModel = new AddressViewModel();
        }

    }
}
