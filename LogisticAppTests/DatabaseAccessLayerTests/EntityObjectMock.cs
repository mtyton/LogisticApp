using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using LogisticApp.DatabaseAccessLayer.Entity;
using System.Data;

namespace LogisticAppTests.DatabaseAccessLayerTests
{
    public static class EntityObjectMock
    {
        public static Address createAddress()
        {
            Address addr = null;
            using (IDataReader reader = EntityDataMock.mockAddressReader())
            {
                addr = new Address(reader);
            }
            return addr;
            
        }

        public static Person createPerson()
        {
            Person person = null;
            Address addr = EntityObjectMock.createAddress();
            using (IDataReader reader = EntityDataMock.mockPersonReader())
            {
                person = new Person(reader, addr);
            }
            return person;
        }

        public static Company createCompany()
        {
            Company company = null;
            Address addr = EntityObjectMock.createAddress();
            using (IDataReader reader = EntityDataMock.mockCompanyReader())
            {
                company = new Company(reader, addr);
            }
            return company;
        }

        public static Employee createEmployee()
        {
            Employee employee = null;
            List<Ability> abilities = new List<Ability>();
            using (IDataReader reader = EntityDataMock.mockEmployeeReader())
            {
                employee = new Employee(reader, abilities);
            }
            return employee;
        }

    }
}
