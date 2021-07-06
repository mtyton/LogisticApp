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

        public override void updateRecord()
        {
            Person person = (Person)this.Creator.Record;
            person.name = _name;
            person.surname = _surname;
            person.address = (Address)_addressViewModel.Creator.Record;
            this.Creator.Record = person;
        }

        public override void loadData(BaseEntity entity)
        {
            Person person = (Person)entity;
            this.AddrViewModel.loadData(person.address);
            this.Creator.Record = person;
            Name = person.name;
            Surname = person.surname;
            
        }

        //TODO add validation if there will be enough time
        public override void save()
        {
            _addressViewModel.save();
            this.updateRecord();
            Creator.createOrUpdate("person");
        }
    }
}
