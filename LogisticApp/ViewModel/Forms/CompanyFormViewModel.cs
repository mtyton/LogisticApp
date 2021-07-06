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

        AddressFormViewModel _addressViewModel = new AddressFormViewModel();

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

        public override void updateRecord()
        {
            Company company = (Company)this.Creator.Record;
            company.companyName = _companyName;
            company.taxNumber = _taxNumber;
            company.address = (Address)_addressViewModel.Creator.Record;
            this.Creator.Record = company;
        }


        public override void loadData(BaseEntity entity)
        {
            Company company = (Company)entity;
            this.AddrViewModel.loadData(company.address);
            this.Creator.Record = company;
            CompanyName = company.companyName;
            TaxNumber = company.taxNumber;
        }

        //TODO add validation if there will be enough time
        public override void save()
        {
            AddrViewModel.save();
            this.updateRecord();
            Creator.createOrUpdate("company");
        }
    }
}
