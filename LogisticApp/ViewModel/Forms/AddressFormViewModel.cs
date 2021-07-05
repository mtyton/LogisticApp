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
    class AddressFormViewModel : BaseFormViewModel
    {
        #region{variables and properties}
        string _city;
        public string City
        {
            get => _city;
            set
            {
                _city = value;
                onPropertyChanged(nameof(City));
            }
        }

        string _postalCode;
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                onPropertyChanged(nameof(PostalCode));
            }
        }

        string _country;
        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                onPropertyChanged(nameof(Country));
            }
        }

        string _street;
        public string Street
        {
            get => _street;
            set
            {
                _street = value;
                onPropertyChanged(nameof(Street));
            }
        }

        int _buildingNumber;
        public int BuildingNumber
        {
            get => _buildingNumber;
            set
            {
                _buildingNumber = value;
                onPropertyChanged(nameof(BuildingNumber));
            }
        }

        int _apartmentNumber;
        public int ApartmentNumber
        {
            get => _apartmentNumber;
            set
            {
                _apartmentNumber = value;
                onPropertyChanged(nameof(ApartmentNumber));
            }
        }
        #endregion


        public override object[] serializeData()
        {
            object[] data = {
                _city, _country, _street, 
                _postalCode, _buildingNumber,
                _apartmentNumber
            };
            return data;
        }

        public override void loadData(BaseEntity entity)
        {
            Address address = (Address)entity;
            this.Creator.Record = address;
        }

        public override void save()
        {
            if (canSave())
                Creator.createOrUpdate("address", this.serializeData());
            else
                Console.WriteLine("Empty textbok or incorrect data.");
        }

        public bool canSave()
        {
            object[] entityParams = serializeData();
            for (int i = 0; i < entityParams.Length - 1; i++) // apartment nr allowed to be 0
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
