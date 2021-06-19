using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogisticAppTests.DatabaseAccessLayerTests;
using LogisticApp.DatabaseAccessLayer.Entity.Client;


namespace LogisticAppTests.DatabaseAccessLayerTests.Enitity.Client
{
    [TestClass]
    public class AddressTest
    {
        [TestMethod]
        public void TestCreationFromDataReader()
        {
            using(var reader = EntityDataMock.mockAddressReader())
            {
                string correctString = $"" +
                    $"{reader["country"]}, {reader["city"]}, " +
                    $"{reader["postal_code"]}, {reader["street"]} " +
                    $"{reader["building_number"]}/{reader["apartment_number"]}";

                Address addr = new Address(reader);
                Assert.AreEqual(addr.ToString(), correctString);

            }
        }
    }
}
