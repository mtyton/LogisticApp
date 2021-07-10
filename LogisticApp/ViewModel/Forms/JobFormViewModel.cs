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

        private ForeingKeyListBoxViewModel _employees;

        public ForeingKeyListBoxViewModel Employees
        {
            get => _employees;
            set
            {
                _employees = value;
                onPropertyChanged(nameof(Employees));
            }
        }

        #endregion

        public JobFormViewModel()
        {
            Employees = new ForeingKeyListBoxViewModel();
            if (Employees.LoadData.CanExecute("employee"))
            {
                Employees.LoadData.Execute("employee");
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
            string entityName = param.ToString();
            ForeingKeyListBoxViewModel customer = new ForeingKeyListBoxViewModel();
            if (customer.LoadData.CanExecute(entityName))
            {
                customer.LoadData.Execute(entityName);
            }
            Customer = customer;
        }


        #endregion

    }
}
