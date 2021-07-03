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
        public void TestToString()
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

        [TestMethod]
        public void TestToInsertSuccess()
        {
            using(var reader = EntityDataMock.mockAddressReader())
            {
                Address addr = new Address(reader);
                string correctString = $"" +
                $"(city, postal_code, country, street, building_number, apartment_number)" +
                $" VALUES ('{reader["city"]}', '{reader["postal_code"]}'," +
                $" '{reader["country"]}', '{reader["street"]}', " +
                $"{reader["building_number"]}, {reader["apartment_number"]});";


                Assert.AreEqual(addr.ToInsert(), correctString);
            }
        }

        [TestMethod]
        public void TestToInsertFailure()
        {

            Address addr = new Address(null, null, null, null, 0, 0);
            try
            {
                addr.ToInsert();
            }
            catch (ArgumentNullException)
            {
                Assert.AreEqual(true, true);
                return;
            }
            Assert.AreEqual(false, true);
        }

        [TestMethod]
        public void TestToUpdateSuccess()
        {
            using (var reader = EntityDataMock.mockAddressReader())
            {
                Address addr = new Address(reader);
                string correctString = $"city='{reader["city"]}', " +
                    $"postal_code='{reader["postal_code"]}', " + 
                    $"country='{reader["country"]}', street='{reader["street"]}', " +
                    $"building_number={reader["building_number"]}, " +
                    $"apartment_number={reader["apartment_number"]}";

                Assert.AreEqual(addr.ToUpdate(), correctString);
            }
        }

        [TestMethod]
        public void TestToUpdateFailure()
        {

            Address addr = new Address(null, null, null, null, 0, 0);
            try
            {
                addr.ToUpdate();
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
