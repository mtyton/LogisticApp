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

        public override object[] serializeData()
        {
            object[] data = { _name, _surname, _addressViewModel.Creator.Record };
            return data;
        }

        public override void loadData(BaseEntity entity)
        {
            Person person = (Person)entity;
            this.AddrViewModel.loadData(person.address);
            this.Creator.Record = person;
        }

        //TODO add validation if there will be enough time
        public override void save()
        {
            if (canSave())
            {
                _addressViewModel.save();
                Creator.createOrUpdate("person", this.serializeData());
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
