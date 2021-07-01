using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.DAOS;
using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Base;

namespace LogisticApp.Model
{
    class ListModel
    {
        ObservableCollection<BaseEntity> _queryset = null;

        ObservableCollection<BaseEntity> Queryset
        {
            get => _queryset;
        }

        ListModel()
        {
            // Basically there will be no data loaded
        }

        public void loadQueryset(object[] parameters)
        {
            // TODO This method should load proper data from database
        }

    }
}
