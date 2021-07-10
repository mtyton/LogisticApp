using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.Model;
using LogisticApp.Model.Pagination;
using LogisticApp.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogisticApp.ViewModel.Base
{
    class BasePaginatedViewModel : BaseViewModel
    {

        protected string _entityName;

        protected OffsetPagination _paginator;

        protected ListModel _listData;

        public ListModel ListData
        {
            get => _listData;
            set
            {
                _listData = value;
                onPropertyChanged(nameof(ListData));
            }
        }

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


        private ICommand _loadData;

        public ICommand LoadData => _loadData ?? (
            _loadData = new RelayCommand(load, canLoad)
        );

        private bool canLoad(object param)
        {
            return true;
        }

        protected void load(object param)
        {
            string entityName = param.ToString();
            this._listData.loadQueryset(
                entityName, _paginator.Pagination
                );
            CurrentQueryset = this._listData.Queryset;
            // if entity changed we should reset paginator
            if (this._entityName != entityName)
            {
                _paginator.reset();
                _paginator.NumberOfRecords = this._listData.TotalCount;
            }
            this._entityName = entityName;
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
            return _paginator.canMove(param.ToString());
        }
        private void move(object param)
        {
            _paginator.move((string)param);
            // after moving, reload data
            this.load(this._entityName);
        }

    }
}
