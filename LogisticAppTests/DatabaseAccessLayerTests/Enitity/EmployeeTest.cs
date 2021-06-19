using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Client;

namespace LogisticAppTests.DatabaseAccessLayerTests.Enitity
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        public void TestCreationFromDataReader()
        {
            using (var reader = EntityDataMock.mockEmployeeReader())
            {
                string correctString = $"" +
                    $"{reader["name"]} {reader["surname"]}";

                Employee employee = new Employee(reader);
                Assert.AreEqual(employee.ToString(), correctString);
            }

        }

        public void testToInsert()
        {

        }

    }
}
