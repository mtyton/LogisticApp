using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Client;

namespace LogisticAppTests.DatabaseAccessLayerTests.Enitity
{
    [TestClass]
    public class AbilityTest
    {
        [TestMethod]
        public void testAbilityReaderCreate()
        {
            using (var reader = EntityDataMock.mockAbilityReader())
            {
                string correctString = $"{reader["ability_name"]}";

                Ability ability = new Ability(reader);
                Assert.AreEqual(ability.ToString(), correctString);
            }
        }


        [TestMethod]
        public void testToInsertSuccess()
        {
            using (var reader = EntityDataMock.mockAbilityReader())
            {
                string correctString = $"(ability_name) " +
                    $"VALUES ({reader["ability_name"]})";

                Ability ability = new Ability(reader);
                Assert.AreEqual(ability.ToInsert(), correctString);
            }
        }

        [TestMethod]
        public void testToInsertFailure()
        {
            Ability ability = new Ability((string)null);
            try
            {
                ability.ToInsert();
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
            using (var reader = EntityDataMock.mockAbilityReader())
            {
                string correctString = $"" +
                    $"ability_name={reader["ability_name"]}";

                Ability ability = new Ability(reader);
                Assert.AreEqual(ability.ToUpdate(), correctString);
            }
        }

        [TestMethod]
        public void testToUpdateFailure()
        {
            Ability ability = new Ability((string)null);
            try
            {
                ability.ToUpdate();
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
