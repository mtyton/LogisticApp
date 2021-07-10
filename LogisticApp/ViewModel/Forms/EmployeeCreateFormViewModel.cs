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

        private DateTime _birthDate = DateTime.Now;
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

        //TODO add validation if there will be enough time
        public override void save()
        {
            Creator.createOrUpdate("employee");
        }

    }
}
