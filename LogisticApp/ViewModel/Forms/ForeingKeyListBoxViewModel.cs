using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.Model;
using LogisticApp.Model.Pagination;
using LogisticApp.ViewModel.Base;
using LogisticApp.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.ViewModel.Forms
{
    class ForeingKeyListBoxViewModel : BasePaginatedViewModel
    {

        BaseEntity _selectedRecord;
        public BaseEntity SelectedRecord
        {
            get => _selectedRecord;
            set
            {
                _selectedRecord = value;
                onPropertyChanged(nameof(SelectedRecord));
            }
        }

        public ForeingKeyListBoxViewModel()
        {
            _listData = new ListModel();
            _paginator = new OffsetPagination();

        }
        #region{commands}


        #endregion
    }
}
