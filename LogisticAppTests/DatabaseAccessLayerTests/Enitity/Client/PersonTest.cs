using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using System;

namespace LogisticAppTests.DatabaseAccessLayerTests.Enitity.Client
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void testToString()
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
                    Person person = new Person(personReader);
                    person.Addr = addr;
                    Assert.AreEqual(person.ToString(), correctString);
                }
            }

        }

        [TestMethod]
        public void testToInsertSuccess()
        {
            using (var personReader = EntityDataMock.mockPersonReader())
            {

                Address addr = new Address(null, null, null, null, 0, 0);
                addr.ID = 67;
                Person person = new Person(personReader);
                person.Addr = addr;

                string correctString = $"(id, name, surname, address_id) " +
                    $"VALUES (NULL, '{personReader["name"]}', '{personReader["surname"]}', " +
                    $"{addr.ID});";

                Assert.AreEqual(person.ToInsert(), correctString);
            }
        }

        [TestMethod]
        public void testToInsertFailure()
        {
            Person person = new Person(null, null, null);
            try
            {
                person.ToInsert();
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
            using (var personReader = EntityDataMock.mockPersonReader())
            {

                Address addr = new Address(null, null, null, null, 0, 0);
                addr.ID = 67;
                Person person = new Person(personReader);
                person.Addr = addr;

                string correctString = $"name='{personReader["name"]}', " +
                    $"surname='{personReader["surname"]}', " + 
                    $"address_id={addr.ID}";

                Assert.AreEqual(person.ToUpdate(), correctString);
            }
        }

        [TestMethod]
        public void testToUpdateFailure()
        {
            Person person = new Person(null, null, null);
            try
            {
                person.ToUpdate();
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
