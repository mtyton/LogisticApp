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
    class CompanyFormViewModel : BaseFormViewModel
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

        string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                onPropertyChanged(CompanyName);
            }
        }
        #endregion

        public CompanyFormViewModel()
        {
            AddrViewModel = new AddressFormViewModel();
        }

        public override object[] serializeData()
        {
            object[] data = { _companyName, _taxNumber, _addressViewModel.Creator.Record };
            return data;
        }

        public override void loadData(BaseEntity entity)
        {
            Company company = (Company)entity;
            this.AddrViewModel.loadData(company.address);
            this.Creator.Record = company;
        }

        //TODO add validation if there will be enough time
        public override void save()
        {
            AddrViewModel.save();
            Creator.createOrUpdate("company", this.serializeData());
        }
    }
}
