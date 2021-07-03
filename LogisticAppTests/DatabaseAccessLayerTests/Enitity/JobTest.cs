using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogisticApp.DatabaseAccessLayer.Entity;

namespace LogisticAppTests.DatabaseAccessLayerTests.Enitity
{
    [TestClass]
    public class JobTest
    {

        [TestMethod]
        public void TestJobToString()
        {
            using (var reader = EntityDataMock.mockJobReader())
            {
                string correctString = $"{reader["title"]}";

                Job job = new Job(reader);
                Assert.AreEqual(job.ToString(), correctString);

            }
        }

        [TestMethod]
        public void TestToInsertPersonSuccess()
        {
            using (var reader = EntityDataMock.mockJobReader())
            {
                Job job = new Job(reader);
                job.ClientPerson = EntityObjectMock.createPerson();
                job.AssignedEmployee = EntityObjectMock.createEmployee();

                string correctString = $"(title, person_id, description, " +
                    $"assigned_employee, predicted_time, predicted_cost) " +
                    $"VALUES ({reader["title"]}, {job.ClientPerson.id}," +
                    $" {reader["description"]}, {job.AssignedEmployee}," +
                    $" {reader["predicted_time"]}, {reader["predicted_cost"]});";

                Assert.AreEqual(job.ToInsert(), correctString);

            }
        }

        [TestMethod]
        public void TestToInsertCompanySuccess()
        {
            using (var reader = EntityDataMock.mockJobReader())
            {

                Job job = new Job(reader);
                job.ClientCompany = EntityObjectMock.createCompany();
                job.AssignedEmployee = EntityObjectMock.createEmployee();
                string correctString = $"(title, company_id, description, " +
                    $"assigned_employee, predicted_time, predicted_cost) " +
                    $"VALUES ({reader["title"]}, {job.ClientCompany.id}," +
                    $" {reader["description"]}, {job.AssignedEmployee}," +
                    $" {reader["predicted_time"]}, {reader["predicted_cost"]});";
                Assert.AreEqual(job.ToInsert(), correctString);

            }
        }

        [TestMethod]
        public void TestToInsertFailure()
        {
            using (var reader = EntityDataMock.mockJobReader())
            {
                string correctString = $"{reader["title"]}";

                Job job = new Job(reader);
                try
                {
                    job.ToInsert();
                }
                catch (Exception error)
                {
                    Assert.AreEqual(true, true);
                    return;
                }
                Assert.AreEqual(false, true);
            }
        }

        [TestMethod]
        public void TestToUpdatePersonSuccess()
        {
            using (var reader = EntityDataMock.mockJobReader())
            {

                Job job = new Job(reader);
                job.ClientPerson = EntityObjectMock.createPerson();
                job.AssignedEmployee = EntityObjectMock.createEmployee();
                string correctString = $"title={reader["title"]}, " +
                    $"description={reader["description"]}, " +
                    $"person_id={job.ClientPerson.id}, " +
                    $"assigned_employee={job.AssignedEmployee.id}, " +
                    $"predicted_time={reader["predicted_time"]}, " +
                    $"predicted_cost={reader["predicted_cost"]};";
                Assert.AreEqual(job.ToUpdate(), correctString);

            }
        }

        [TestMethod]
        public void TestToUpdateCompanySuccess()
        {
            using (var reader = EntityDataMock.mockJobReader())
            {

                Job job = new Job(reader);
                job.ClientCompany = EntityObjectMock.createCompany();
                job.AssignedEmployee = EntityObjectMock.createEmployee();
                string correctString = $"title={reader["title"]}, " +
                    $"description={reader["description"]}, " +
                    $"company_id={job.ClientCompany.id}, " +
                    $"assigned_employee={job.AssignedEmployee.id}, " +
                    $"predicted_time={reader["predicted_time"]}, " +
                    $"predicted_cost={reader["predicted_cost"]};";
                Assert.AreEqual(job.ToUpdate(), correctString);

            }
        }

    }
}
