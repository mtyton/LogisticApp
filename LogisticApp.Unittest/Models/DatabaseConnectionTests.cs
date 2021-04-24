using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using LogisticApp.Models

namespace LogisticApp.Unittest.Models
{
    /// <summary>
    /// Summary description for DatabaseConnectionTests
    /// </summary>
    [TestClass]
    public class DatabaseConnectionTests
    {
        public DatabaseConnectionTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestcheckConnection_Success()
        {
            string server = "localhost";
            string username = "root";
            string password = "zaq1@WSX";

            DatabaseConnection baseModel = new DatabaseConnection(server, username, password);
            bool connectionWorks = baseModel.checkConnection();
            Assert.IsTrue(connectionWorks);

        }

        [TestMethod]
        public void test_insertData()
        {
            string server = "localhost";
            string username = "root";
            string password = "zaq1@WSX";

            DatabaseConnection baseModel = new DatabaseConnection(server, username, password);

        }
    }
}

