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
            if (canSave())
            {
                AddrViewModel.save();
                Creator.createOrUpdate("company", this.serializeData());
            }
            else
                Console.WriteLine("Empty textbok or incorrect data.");
        }

        public bool canSave()
        {
            if (!_addressViewModel.canSave()) return false;
            object[] entityParams = serializeData();
            for (int i = 0; i < entityParams.Length - 1; i++) // no address entity needed
            {
                if (entityParams[i] != null)
                {
                    Type varType = entityParams[i].GetType();
                    switch (varType.Name)
                    {
                        case "String":
                            if (((string)entityParams[i]).Length < 2)
                                return false;
                            break;
                        case "Int32":
                            if ((int)entityParams[i] == 0)
                                return false;
                            break;
                    }
                }
                else
                    return false;
            }
            return true;
        }
    }
}
