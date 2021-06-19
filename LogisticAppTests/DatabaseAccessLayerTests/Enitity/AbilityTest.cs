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
        public void TestAbilityReaderCreate()
        {
            using (var reader = EntityDataMock.mockAbilityReader())
            {
                string correctString = $"{reader["ability_name"]}";

                Ability ability = new Ability(reader);
                Assert.AreEqual(ability.ToString(), correctString);
            }
        }

    }
}
