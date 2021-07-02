using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using System.Collections.Generic;
using System.Data;


namespace LogisticAppTests.DatabaseAccessLayerTests.Enitity
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        public void testCreationFromDataReader()
        {
            using (var reader = EntityDataMock.mockEmployeeReader())
            {
                string correctString = $"" +
                    $"{reader["name"]} {reader["surname"]}";

                List<Skillset> abilities = new List<Skillset>();
                Employee employee = new Employee(reader, abilities);
                Assert.AreEqual(employee.ToString(), correctString);
            }

        }

        [TestMethod]
        public void testToInsertSuccess()
        {
            using (var reader = EntityDataMock.mockEmployeeReader())
            {
                string correctString = $"(name, surname, birth_date, " +
                    $"date_of_employment, hourly_payment)" +
                    $"VALUES({reader["name"]}, " +
                    $"{reader["surname"]}, " +
                    $"{DateTime.Parse(reader["birth_date"].ToString())}, " +
                    $"{DateTime.Parse(reader["date_of_employment"].ToString())}, " +
                    $"{reader["hourly_payment"]});";
                List<Skillset> abilities = new List<Skillset>();
                Employee employee = new Employee(reader, abilities);
                Assert.AreEqual(employee.ToInsert(), correctString);
            }
        }

        [TestMethod]
        public void testToInsertFailure()
        {
            Employee employee = new Employee(
                null, null, DateTime.Parse("202-10-05"),
                DateTime.Parse("202-10-05"), 0, null
                );
            try
            {
                employee.ToInsert();
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(true, true);
                return;
            }
            Assert.AreEqual(false, true);
        }

        [TestMethod]
        public void testToUpdateSuccess()
        {
            using (var reader = EntityDataMock.mockEmployeeReader())
            {
                string correctString = $"name={reader["name"]}, surname={reader["surname"]}, " +
                    $"birth_date={DateTime.Parse(reader["birth_date"].ToString())}, " +
                    $"date_of_employment={DateTime.Parse(reader["date_of_employment"].ToString())}," +
                    $"hourly_payment={reader["hourly_payment"]};";
                List<Skillset> abilities = new List<Skillset>();
                Employee employee = new Employee(reader, abilities);
                Assert.AreEqual(employee.ToUpdate(), correctString);
            }
        }

        [TestMethod]
        public void testToUpdateFailure()
        {
            Employee employee = new Employee(
                null, null, DateTime.Parse("202-10-05"),
                DateTime.Parse("202-10-05"), 0, null
                );
            try
            {
                employee.ToInsert();
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(true, true);
                return;
            }
            Assert.AreEqual(false, true);
        }

    }
}
