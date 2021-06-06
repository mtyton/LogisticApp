using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Client;


namespace LogisticApp.Unittest.Entity
{
    [TestClass]
    public class BaseClientTest
    {
        private IDataReader mockCompanyDataReader()
        {
            var moq = new Mock<IDataReader>();
            moq.Setup(x => x.Read()).Returns(true);
            moq.Setup(x => x.Read()).Returns(false);
            moq.SetupGet<object>(x => x["city"]).Returns("testCity1");
            moq.SetupGet<object>(x => x["postal-code"]).Returns("000");
            moq.SetupGet<object>(x => x["street"]).Returns("str");
            return moq.Object;
        }

        [TestMethod]
        public void TestClientCreation()
        {
            using(var reader = mockCompanyDataReader())
            {
                BaseClient c = new BaseClient((MySqlDataReader)reader);
            }

        }
    }
}
