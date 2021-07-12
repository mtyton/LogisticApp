using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.Model;
using LogisticApp.Model.Pagination;
using LogisticApp.Model.Utils;
using LogisticApp.ViewModel.Base;
using LogisticApp.ViewModel.BaseClass;
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
    class MainMenuViewModel : BasePaginatedViewModel
    {

        MainFormWindow _formWindow = null;

        public MainMenuViewModel()
        {
            _listData = new ListModel();
            _paginator = new OffsetPagination();
        }

        #region{commands}
        #region{list management}



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

        public ICommand OpenEditWindow => _openEditWindow ?? (
            _openEditWindow = new RelayCommand(openWindow, canOpenEditWindow)
        );

        private bool canOpenCreateWindow(object param)
        {
            // we can have only one subwindow opened at the time
            return this._formWindow == null && this.ListData.Queryset!=null;
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
                WindowObserver.getObserver(this)
                );
            this._formWindow.Show();
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
