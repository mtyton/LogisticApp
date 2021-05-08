using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using LogisticApp.Models;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace LogisticApp.Unittest.Models
{
    /// <summary>
    /// Summary description for DatabaseConnectionTests
    /// </summary>
    [TestClass]
    public class DatabaseConnectionTests
    {

        [TestMethod]
        public void TestcheckConnection_Opened()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.openConnection();
            bool connectionWorks = dbConnection.checkConnection();
            Assert.AreEqual(connectionWorks, true);

        }

        [TestMethod]
        public void TestcheckConnection_Closed()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            bool connectionWorks = dbConnection.checkConnection();
            Assert.AreEqual(connectionWorks, false);

        }

        [TestMethod]
        public void testCreateDeleteTable_Success()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.openConnection();
            bool connectionWorks = dbConnection.checkConnection();
            Assert.AreEqual(connectionWorks, true);
            bool created = dbConnection.createTableIfNotExists(
                "Test1", "TestID int, TestName varchar(30)"
                );
            Assert.AreEqual(created, true);
            
            bool deleted = dbConnection.dropTableIfExists("Test1");
            Assert.AreEqual(deleted, true);
        }

        [TestMethod]
        public void testExecuteNonQuery_Success()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.openConnection();
            // now we need to create table
            bool created = dbConnection.createTableIfNotExists(
                "Test1", "TestID int, TestName varchar(30)"
            );
            Assert.AreEqual(created, true);
            string queryString = "INSERT INTO TEST1 (TestName) " +
                "VALUES('this is test name')";

            bool inserted = dbConnection.executeNonQuery(queryString);
            Assert.AreEqual(inserted, true);

            // at the end remember to delete
            bool deleted = dbConnection.dropTableIfExists("Test1");
            Assert.AreEqual(deleted, true);
        }

        [TestMethod]
        public void testExecuteReader_Success()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.openConnection();
            // now we need to create table
            bool created = dbConnection.createTableIfNotExists(
                "Test1", "TestID int, TestName varchar(30)"
            );
            Assert.AreEqual(created, true);

            // let's insert some data into database
            string queryString = "INSERT INTO Test1 (TestName) " +
            "VALUES('this is test name')";

            bool inserted = dbConnection.executeNonQuery(queryString);
            Assert.AreEqual(inserted, true);

            // after there are data in database we can select this data
            queryString = "SELECT * FROM Test1";
            string name = "";
            MySqlDataReader reader = dbConnection.executeReader(queryString);
            reader.Read();
            name = reader.GetString("TestName");
            Assert.AreEqual(name, "this is test name");
            // at the end remember to delete
            bool deleted = dbConnection.dropTableIfExists("Test1");
            Assert.AreEqual(deleted, true);
        }
    }
}

