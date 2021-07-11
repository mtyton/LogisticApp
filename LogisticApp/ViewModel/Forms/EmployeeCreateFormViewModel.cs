using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
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

        private DateTime _birthDate = DateTime.Parse("1/2000"); // Initial date set to 01.01.2000
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                onPropertyChanged(nameof(BirthDate));
            }
        }

        private DateTime _dateOfEmployment = DateTime.Now;
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

        public override void updateRecord()
        {
            Employee employee = (Employee)this.Creator.Record;
            employee.name = _firstname;
            employee.surname = _lastname;
            employee.dateOfEmployment = _dateOfEmployment;
            employee.birthDate = _birthDate;
            employee.hourlyPayment = _hourlyPayment;
            this.Creator.Record = employee;
        }

        public override void createRecord()
        {
            this.Creator.Record = new Employee(
                _firstname, _lastname, _birthDate, _dateOfEmployment, _hourlyPayment
                );
        }

        public override void loadData(BaseEntity entity)
        {
            Employee employee = (Employee)entity;
            this.Creator.Record = employee;
            FirstName = employee.name;
            LastName = employee.surname;
            HourlyPaymnet = employee.hourlyPayment;
            DateOfEmployment = employee.dateOfEmployment;
            BirthDate = employee.birthDate;
        }

        public override void save()
        {
            Creator.createOrUpdate("employee");
        }

        public override bool canSave()
        {
            return _firstname != "" &&
                _lastname != "" &&
                _hourlyPayment > 0 &&
                _birthDate.AddYears(15) < DateOfEmployment && // employee has to be 15 years old to take up work
                _birthDate < DateTime.Now; // cannot be born in the future
        }
    }
}
