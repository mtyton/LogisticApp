using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using LogisticApp.Model;
using LogisticApp.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.ViewModel.Forms
{
    class PersonFormViewModel : BaseFormViewModel
    {
        #region {variables and properties}

        AddressFormViewModel _addressViewModel;

        public AddressFormViewModel AddrViewModel
        {
            get => _addressViewModel;
            set
            {
                _addressViewModel = value;
                onPropertyChanged(nameof(AddrViewModel));
            }
        }

        string _taxNumber;

        public string TaxNumber
        {
            get => _taxNumber;
            set
            {
                _taxNumber = value;
                onPropertyChanged(TaxNumber);
            }
        }

        string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                onPropertyChanged(Name);
            }
        }

        string _surname;

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                onPropertyChanged(Surname);
            }
        }

        #endregion


    }
}
