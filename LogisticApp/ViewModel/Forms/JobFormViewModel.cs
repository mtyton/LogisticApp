using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using LogisticApp.Model;
using LogisticApp.Model.Pagination;
using LogisticApp.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogisticApp.ViewModel.Forms
{
    class JobFormViewModel : BaseFormViewModel
    {
        #region{form variables and properties}
        // this variable keeps currently chosen customerType
        private string _currentCustomerType;

        public string CurrentCustomerType
        {
            get => _currentCustomerType;
            set
            {
                _currentCustomerType = value;
                onPropertyChanged(nameof(CurrentCustomerType));
            }
        }

        private string _jobTitle;
        public string JobTitle
        {
            get => _jobTitle;
            set
            {
                _jobTitle = value;
                onPropertyChanged(nameof(JobTitle));
            }
        }

        private int _predictedTime;
        public int PredictedTime
        {
            get => _predictedTime;
            set
            {
                _predictedTime = value;
                onPropertyChanged(nameof(PredictedTime));
            }
        }

        private int _predictedCost;
        public int PredictedCost
        {
            get => _predictedCost;
            set
            {
                _predictedCost = value;
                onPropertyChanged(nameof(PredictedCost));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                onPropertyChanged(nameof(Description));
            }
        }

        #endregion

        #region{searchable list box variables and properites}

        private ForeingKeyListBoxViewModel _customer;

        public ForeingKeyListBoxViewModel Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                onPropertyChanged(nameof(Customer));
            }
        }

        private ForeingKeyListBoxViewModel _assignedEmployee;

        public ForeingKeyListBoxViewModel AssignedEmployee
        {
            get => _assignedEmployee;
            set
            {
                _assignedEmployee = value;
                onPropertyChanged(nameof(AssignedEmployee));
            }
        }

        #endregion

        public JobFormViewModel()
        {
            AssignedEmployee = new ForeingKeyListBoxViewModel();
            if (AssignedEmployee.LoadData.CanExecute("employee"))
            {
                AssignedEmployee.LoadData.Execute("employee");
            }

            Customer = new ForeingKeyListBoxViewModel();
        }

        #region{commands}
        ICommand _customerTypeChange;
        public ICommand CustomerTypeChange => _customerTypeChange ?? (
            _customerTypeChange = new RelayCommand(changeCustomerType, p=>true)
        );

        private void changeCustomerType(object param)
        {
            string entityName = CurrentCustomerType;
            ForeingKeyListBoxViewModel customer = new ForeingKeyListBoxViewModel();
            if (customer.LoadData.CanExecute(entityName))
            {
                customer.LoadData.Execute(entityName);
            }
            Customer = customer;
            CurrentCustomerType = entityName;
        }
        #endregion
        #region{form methods}
        public override void createRecord()
        {
           Job job = new Job(
                _jobTitle, _description, _predictedCost, _predictedTime
                );
            // assigne proper customer, depending on type
            if(CurrentCustomerType == "company")
            {
                job.clientCompany = (Company)Customer.SelectedRecord;
            }
            else
            {
                job.clientPerson = (Person)Customer.SelectedRecord;
            }
            // assing employee
            // TODO rethink Assigned Employee name
            job.assignedEmployee = (Employee)AssignedEmployee.SelectedRecord;
            this.Creator.Record = job;
        }
        public override void updateRecord()
        {
            Job job = (Job)this.Creator.Record;
            job.title = _jobTitle;
            job.description = _description;
            job.predictedCost = _predictedCost;
            job.predictedTime = _predictedTime;
            if (CurrentCustomerType == "company")
            {
                job.clientCompany = (Company)Customer.SelectedRecord;
            }
            else
            {
                job.clientPerson = (Person)Customer.SelectedRecord;
            }
            job.assignedEmployee = (Employee)AssignedEmployee.SelectedRecord;
            this.Creator.Record = job;
        }

        public override void loadData(BaseEntity entity)
        {
            Job job = (Job)entity;
            _jobTitle = job.title;
            _description = job.description;
            _predictedCost = job.predictedCost;
            _predictedTime = job.predictedTime;
            if(job.clientCompany != null)
            {
                CurrentCustomerType = "company";
                Customer.LoadData.Execute(CurrentCustomerType);
                Customer.SelectedRecord = job.clientCompany;
            }
            else if(job.clientPerson != null)
            {
                CurrentCustomerType = "person";
                Customer.LoadData.Execute(CurrentCustomerType);
                Customer.SelectedRecord = job.clientPerson;
            }
            AssignedEmployee.LoadData.Execute("employee");
            AssignedEmployee.SelectedRecord = job.assignedEmployee;
            this.Creator.Record = job;
        }

        public override void save()
        {
            Creator.createOrUpdate("job");
        }

        public override bool canSave()
        {
            return (
                _jobTitle!=null && _description!=null && _predictedCost!=0 &&
                _predictedTime!=0 && _customer.SelectedRecord!=null && 
                _assignedEmployee.SelectedRecord!=null
                );
        }
        #endregion
    }
}
