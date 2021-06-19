using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using System;

namespace LogisticAppTests.DatabaseAccessLayerTests.Enitity.Client
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void TestCreationFromDataReader()
        {
            using (var personReader = EntityDataMock.mockPersonReader())
            {
                using (var addrReader = EntityDataMock.mockAddressReader())
                {
                    string correctString = $"" +
                    $"{personReader["name"]} {personReader["surname"]} - " +
                    $"{addrReader["country"]}, {addrReader["city"]}, " +
                    $"{addrReader["postal_code"]}, {addrReader["street"]} " +
                    $"{addrReader["building_number"]}/{addrReader["apartment_number"]}";

                    Address addr = new Address(addrReader);
                    Person person = new Person(personReader, addr);
                    Assert.AreEqual(person.ToString(), correctString);
                }
            }

        }
    }
}
