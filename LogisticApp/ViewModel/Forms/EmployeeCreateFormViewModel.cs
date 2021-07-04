using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.ViewModel.Forms
{
    class EmployeeCreateFormViewModel : BaseFormViewModel
    {
        #region{variables and properties}

        private string _firstname;
        public string FirstName 
        { 
            get => _firstname;
            set
            {
                _firstname = value;
                onPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastname;
        public string LastName
        {
            get => _lastname;
            set
            {
                _lastname = value;
                onPropertyChanged(nameof(LastName));
            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                onPropertyChanged(nameof(BirthDate));
            }
        }

        private DateTime _dateOfEmployment;
        public DateTime DateOfEmployment
        {
            get => _dateOfEmployment;
            set
            {
                _dateOfEmployment = value;
                onPropertyChanged(nameof(DateOfEmployment));
            }
        }
        private int _hourlyPayment;
        public int HourlyPaymnet
        {
            get => _hourlyPayment;
            set
            {
                _hourlyPayment = value;
                onPropertyChanged(nameof(HourlyPaymnet));
            }
        }
        #endregion

        public override object[] serializeData()
        {
            object[] data = { _firstname, _lastname, 
                _dateOfEmployment, _birthDate, 
                _hourlyPayment
            };
            return data;
        }

        public override void loadData(BaseEntity entity)
        {
            Employee employee = (Employee)entity;
            this.Creator.Record = employee;
        }

        //TODO add validation if there will be enough time
        public override void save()
        {
            Creator.createOrUpdate("employee", this.serializeData());
        }

    }
}
