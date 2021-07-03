using LogisticApp.ViewModel.BaseClass;
using LogisticApp.ViewModel.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogisticApp.ViewModel.Forms
{
    class FormManager : BaseViewModel
    {
        BaseFormViewModel _selectedViewModel = null;
        WindowMediator _mediator = null;

        public BaseFormViewModel SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                this.onPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public void setViewModel(string name)
        {
            switch (name)
            {
                //TODO add other ViewModels
                case "company":
                    SelectedViewModel = new CompanyFormViewModel();
                    break;
                case "person":
                    break;
                case "employee":
                    SelectedViewModel = new EmployeeCreateFormViewModel();
                    break;
                case "job":
                    break;
                case "default":
                    throw new TypeLoadException("Entity name not recognized");
            }
        }

        public void addMediator(WindowMediator mediator)
        {
            _mediator = mediator;
        }

        #region{command}
        ICommand _saveData;
        public ICommand SaveData => _saveData ?? (
            _saveData = new RelayCommand(save, canSave)
        );

        private bool canSave(object param)
        {
            return true;
        }

        private void save(object param)
        {
            _selectedViewModel.save();
            if (this.WindowClosed.CanExecute(null))
            {
                this.WindowClosed.Execute(null);
            }
        }

        ICommand _windowClosed;
        public ICommand WindowClosed => _windowClosed ?? (
            _windowClosed = new RelayCommand(close, p => true)
        );

        private void close(object param)
        {
            this._mediator.closeSubWindow();
        }

        #endregion
    }
}
