using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Model.Pagination
{
    interface BasePagination
    {
        int NumberOfRecords { get; set; }

        void reset();
        void move(string param);
        bool canMove(string param);

    }
}
