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

        public ListModel()
        {
            // Basically there will be no data loaded
        }

        public void loadQueryset(string entityName, object[] parameters)
        {
            int start = int.Parse(parameters[0].ToString());
            int number_of_records = int.Parse(parameters[1].ToString());

            switch (entityName)
            {
                case "company":
                    this._queryset = CompanyDataAccessObject.getPaginated(start, number_of_records);
                    break;
                case "person":
                    this._queryset = PersonDataAccessObject.getPaginated(start, number_of_records);
                    break;
                case "employee":
                    this._queryset = EmployeeDataAccessObject.getPaginated(start, number_of_records);
                    break;
                case "job":
                    this._queryset = JobDataAccessObject.getPaginated(start, number_of_records);
                    break;
                case "default":
                    throw new TypeLoadException("Entity name not recognized");
            }

        }

    }
}
