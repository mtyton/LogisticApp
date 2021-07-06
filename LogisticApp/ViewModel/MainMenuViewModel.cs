using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.Model;
using LogisticApp.ViewModel.BaseClass;
using LogisticApp.ViewModel.Utils;
using LogisticApp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LogisticApp.ViewModel
{
    class MainMenuViewModel: BaseViewModel
    {

        private string _entityName;
        private int _start = 0;
        private int _offset = 10;

        ListModel _listData;
        MainFormWindow _formWindow = null;

        private ObservableCollection<BaseEntity> _currentQueryset;

        public ObservableCollection<BaseEntity> CurrentQueryset
        {
            get => this._currentQueryset;
            set
            {
                this._currentQueryset = value;
                onPropertyChanged(nameof(CurrentQueryset));
            }
        }

        public ListModel ListData
        {
            get => _listData;
            set
            {
                _listData = value;
                onPropertyChanged(nameof(ListData));
            }
        }

        public MainMenuViewModel()
        {
            _listData = new ListModel();
        }

        #region{commands}
        #region{list management}
        private ICommand _loadData;

        public ICommand LoadData => _loadData ?? (
            _loadData = new RelayCommand(load, canLoad)
        );

        private bool canLoad(object param)
        {
            return true;
        }

        private void load(object param)
        {
            // if thos two differs start from begining
            if (this._entityName != param.ToString())
            {
                this._start = 0;
            }
            this._entityName = param.ToString();
            // _start and _offset are responsible for pagination
            object[] paginationParams = { this._start, this._offset };
            this._listData.loadQueryset(this._entityName, paginationParams);
            CurrentQueryset = this._listData.Queryset;
        }

        // move step is responsible for paginating in each direction
        private ICommand _moveStep;

        public ICommand MoveStep => _moveStep ?? (
            _moveStep = new RelayCommand(move, canMove)
        );

        private bool canMove(object param)
        {
            // obviously we can't paginate empty queryset
            if (this._listData.Queryset == null)
            {
                return false;
            }
            string direction = param.ToString();
            if (direction == "-")
            {
                return this._start - this._offset >= 0;
            }
            else if (direction == "+")
            {
                return this._start + this._offset < this._listData.TotalCount;
            }
            return false;
        }
        private void move(object param)
        {
            string direction = param.ToString();
            if (direction == "-")
            {
                this._start -= this._offset;
            }
            else if (direction == "+")
            {
                this._start += this._offset;
            }
            // if we modified our record range, now we simpply need to load the data
            this.load(this._entityName);
        }

        private ICommand _deleteRecord;
        public ICommand DeleteRecord => _deleteRecord ?? (
            _deleteRecord = new RelayCommand(delete, canDelete)
        );

        private bool canDelete(object param)
        {
            return param != null;
        }

        private void delete(object param)
        {
            if(this._listData.deleteRecord(this._entityName, param))
            {
                this.load(this._entityName);
            }
            else
            {
                MessageBox.Show("This record can't be deleted!", "Integrity Issue",
                    MessageBoxButton.OK, MessageBoxImage.Error
                    );
            }

        }

        #endregion
        #region{windowManager}

        private ICommand _openCreateWindow;

        public ICommand OpenCreateWindow => _openCreateWindow ?? (
            _openCreateWindow = new RelayCommand(openWindow, canOpenCreateWindow)
        );

        private ICommand _openEditWindow;

        public ICommand OpenEditWindow => _openCreateWindow ?? (
            _openCreateWindow = new RelayCommand(openWindow, canOpenEditWindow)
        );

        private bool canOpenCreateWindow(object param)
        {
            // we can have only one subwindow opened at the time
            return this._formWindow == null;
        }

        private bool canOpenEditWindow(object param)
        {
            return this._formWindow == null && param!=null && this._listData.Queryset!=null;
        }

        private void openWindow(object param)
        {
            this._formWindow = new MainFormWindow();
            this._formWindow.MainFormViewModel.setViewModel(this._entityName);
            // TODO add checking if param is not null for update
            if (param != null)
            {
                BaseEntity entity = (BaseEntity)param;
                this._formWindow.MainFormViewModel.SelectedViewModel.loadData(entity);
            }
            this._formWindow.MainFormViewModel.addMediator(
                WindowMediator.getMediator(this)
                );
            this._formWindow.ShowDialog();
        }

        private ICommand _closeSubWindow;

        public ICommand CloseSubWindow => _closeSubWindow ?? (
            _closeSubWindow = new RelayCommand(closeWindow, canCloseSubWindow)
        );
        private bool canCloseSubWindow(object param=null)
        {
            // we can close window only if it exists
            return this._formWindow != null;
        }

        public void closeWindow(object param = null)
        {
            if (this._formWindow.IsLoaded)
            {
                this._formWindow.Hide();
            }
            this._formWindow = null;
            this.load(this._entityName);
        }
        #endregion
        #endregion

    }
}
