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
using System.Windows;

namespace LogisticApp.ViewModel.Forms
{
    class ForeingKeyListBoxViewModel : BasePaginatedViewModel
    {
        private void moveToSelectedRecord(BaseEntity entity)
        {
            if (entity == null)
            {
                return;
            }
            while(!CurrentQueryset.Contains(entity))
            {
                if (MoveStep.CanExecute("+"))
                {
                    MoveStep.Execute("+");
                }
                else
                {
                    throw new NotImplementedException("DONT KNOW WHAT TO DO");
                }
            }
            
        }

        BaseEntity _selectedRecord;
        public BaseEntity SelectedRecord
        {
            get => _selectedRecord;
            set
            {
                moveToSelectedRecord(value);
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
