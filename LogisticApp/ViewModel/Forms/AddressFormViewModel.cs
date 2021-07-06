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


        public override void createRecord()
        {
            this.Creator.Record = new Address(
                _city, _postalCode, _country, _street,
                _buildingNumber, _apartmentNumber
                );
        }

        public override void updateRecord()
        {
            Address address = (Address)this.Creator.Record;
            address.city = _city;
            address.country = _country;
            address.street = _street;
            address.postalCode = _postalCode;
            address.buildingNumber = _buildingNumber;
            address.apartmentNumber = _apartmentNumber;
            this.Creator.Record = address;
        }

        public override void loadData(BaseEntity entity)
        {
            Address address = (Address)entity;
            this.Creator.Record = address;
            City = address.city;
            Country = address.country;
            Street = address.street;
            PostalCode = address.postalCode;
            BuildingNumber = address.buildingNumber;
            ApartmentNumber = address.apartmentNumber;
        }

        public override void save()
        {
            Creator.createOrUpdate("address");
        }

    }
}
