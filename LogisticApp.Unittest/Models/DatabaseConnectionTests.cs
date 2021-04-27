using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using LogisticApp.Models;

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
        public void testCreateTable_Success()
        {
            DatabaseConnection dbConnection = new DatabaseConnection();
            dbConnection.openConnection();
            bool connectionWorks = dbConnection.checkConnection();
            Assert.AreEqual(connectionWorks, true);
            bool created = dbConnection.createTable("Test1");
            Assert.AreEqual(created, true);
        }


    }
}

