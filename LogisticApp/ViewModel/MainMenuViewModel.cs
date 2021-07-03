using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.Model;
using LogisticApp.ViewModel.BaseClass;
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
        ListModel _listData;

        private ObservableCollection<BaseEntity> _currentQueryset;

        public ObservableCollection<BaseEntity> CurrentQueryset
        {
            get => _currentQueryset;
            set
            {
                _currentQueryset = value;
                onPropertyChanged(nameof(CurrentQueryset));
            }
        }

        private int _start = 0;
        private int _offset = 25;

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
            object[] paginationParams = { _start, _offset };

            this._listData.loadQueryset(entityName, paginationParams);
            CurrentQueryset = this._listData.Queryset;
        }

        // move step is responsible for paginating in each direction
        private ICommand _moveStep;

        public ICommand MoveStep => _moveStep ?? (
            _moveStep = new RelayCommand(load, canMove)
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
                return this._start - this._offset > 0;
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
                this._start += this._offset;
            }
            else if (direction == "+")
            {
                this._start -= this._offset;
            }
            this.load(new object());
        }

        #endregion

    }
}
