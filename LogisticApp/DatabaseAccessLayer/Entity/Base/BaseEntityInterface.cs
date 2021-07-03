using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.DatabaseAccessLayer.Entity.Base
{
    interface BaseEntityInterface
    {
        bool checkIfRecordComplete();
        string ToInsert();
        string ToUpdate();
        string ToString();

    }
}
