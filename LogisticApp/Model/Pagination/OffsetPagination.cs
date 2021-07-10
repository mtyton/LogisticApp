using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Model.Pagination
{
    class OffsetPagination : BasePagination
    {
        #region{variables and properties}
        private int _numberOfRecords = 0;
        public int NumberOfRecords 
        { 
            get => _numberOfRecords; 
            set => _numberOfRecords = value; 
        }
     
        private int _currentStartIndex = 0;
        private int _offset;

        int[] _pagination;

        public int[] Pagination
        {
            get
            {
                _pagination[0] = this._currentStartIndex;
                _pagination[1] = this._offset;
                return _pagination;
            }
        }

        #endregion

        public OffsetPagination(int offset=10)
        {
            this._offset = offset;
            _pagination = new int[2];
        }

        public void reset()
        {
            _currentStartIndex = 0;
        }

        public bool canMove(string direction)
        {
            int finalIndex;

            if (direction == "-")
            {
                finalIndex = this._currentStartIndex - this._offset;
                return finalIndex  >= 0;
            }
            else if (direction == "+")
            {
                finalIndex = this._currentStartIndex + this._offset;
                return finalIndex < this._numberOfRecords;
            }
            return false;
        }

        public void move(string direction)
        {
            if (direction == "-")
            {
                this._currentStartIndex -= this._offset;
            }
            else if (direction == "+")
            {
                this._currentStartIndex += this._offset;
            }
        }
    }
}
