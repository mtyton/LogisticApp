using LogisticApp.DatabaseAccessLayer.DAOS;
using LogisticApp.DatabaseAccessLayer.DAOS.Client;
using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Model
{
    static class DataAccessFacade
    {
        public static ObservableCollection<BaseEntity> getPaginated(string entityName, int start, int number_of_records)
        {
            switch (entityName)
            {
                case "company":
                    return CompanyDataAccessObject.getPaginated(start, number_of_records);
                case "person":
                    return PersonDataAccessObject.getPaginated(start, number_of_records);
                case "employee":
                    return EmployeeDataAccessObject.getPaginated(start, number_of_records);
                case "job":
                    return JobDataAccessObject.getPaginated(start, number_of_records);
                case "default":
                    throw new TypeLoadException("Entity name not recognized");
            }
            throw new TypeLoadException("Entity name not recognized");
        }

        public static int getTotalCount(string entityName)
        {
            switch (entityName)
            {
                case "company":
                    return CompanyDataAccessObject.getTotalCount();
                case "person":
                    return PersonDataAccessObject.getTotalCount();
                case "employee":
                    return EmployeeDataAccessObject.getTotalCount();
                case "job":
                    return JobDataAccessObject.getTotalCount();
                case "default":
                    throw new TypeLoadException("Entity name not recognized");
            }
            throw new TypeLoadException("Entity name not recognized");
        }

        public static BaseEntity create(string entityName, BaseEntity record)
        {
            switch (entityName)
            {
                case "company":
                    return CompanyDataAccessObject.create((Company)record);
                case "person":
                    return PersonDataAccessObject.create((Person)record);
                case "employee":
                    return EmployeeDataAccessObject.create((Employee)record);
                case "job":
                    return JobDataAccessObject.create((Job)record);
                case "address":
                    return AddressDataAccessObject.create((Address)record);
            }
            throw new TypeLoadException("Entity name not recognized");
        }

        public static BaseEntity update(string entityName, BaseEntity record)
        {
            switch (entityName)
            {
                case "company":
                    return CompanyDataAccessObject.update((Company)record);
                case "person":
                    return PersonDataAccessObject.update((Person)record);
                case "employee":
                    //return EmployeeDataAccessObject.update((Employee)record);
                    return null;
                case "job":
                    return JobDataAccessObject.update((Job)record);
                case "address":
                    return AddressDataAccessObject.update((Address)record);
            }
            throw new TypeLoadException("Entity name not recognized");
        }

    }
}
