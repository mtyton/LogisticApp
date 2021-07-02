using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.Model;
using LogisticApp.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogisticApp.ViewModel
{
    class MainMenuViewModel: BaseViewModel
    {
        ListModel _listData;

        private ObservableCollection<BaseEntity>  _currentQueryset;

        public ObservableCollection<BaseEntity> CurrentQueryset
        {
            get => _currentQueryset;
            set
            {
                _currentQueryset = value;
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
            string entityName = param.ToString();
            // TODO add pagination
            object[] paginationParams = { 0, 25 };

            this._listData.loadQueryset(entityName, paginationParams);
            CurrentQueryset = this._listData.Queryset;
        }

        #endregion

    }
}
