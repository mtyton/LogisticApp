using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.DAOS;
using LogisticApp.DatabaseAccessLayer.DAOS.Client;
using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.DatabaseAccessLayer.Entity.Client;

namespace LogisticApp.Model
{
    class ListModel
    {

        ObservableCollection<BaseEntity> _queryset = null;

        public ObservableCollection<BaseEntity> Queryset
        {
            get => _queryset;
        }

        private int _totalCount;

        public int TotalCount
        {
            get => _totalCount;
        }


        public ListModel()
        {
            // Basically there will be no data loaded
        }

        public void loadQueryset(string entityName, object[] parameters)
        {
            if (parameters == null)
            {
                this._queryset = DataAccessFacade.getAll(entityName);
            }
            else
            {
                int start = int.Parse(parameters[0].ToString());
                int number_of_records = int.Parse(parameters[1].ToString());
                this._queryset = DataAccessFacade.getPaginated(entityName, start, number_of_records);
                this._totalCount = DataAccessFacade.getTotalCount(entityName);
            }
        }

        public bool deleteRecord(string entityName, object obj)
        {
            BaseEntity entity = (BaseEntity)obj;
            return DataAccessFacade.delete(entityName, entity);
        }

    }
}
