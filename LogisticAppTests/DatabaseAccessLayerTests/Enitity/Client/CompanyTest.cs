using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using System;

namespace LogisticAppTests.DatabaseAccessLayerTests.Enitity.Client
{
    [TestClass]
    public class CompanyTest
    {
        [TestMethod]
        public void TestCreationFromDataReader()
        {
            using(var companyReader = EntityDataMock.mockCompanyReader())
            {
                using (var addrReader = EntityDataMock.mockAddressReader())
                {
                    string correctString = $"" +
                    $"{companyReader["name"]} - " +
                    $"{addrReader["country"]}, {addrReader["city"]}, " +
                    $"{addrReader["postal_code"]}, {addrReader["street"]} " +
                    $"{addrReader["building_number"]}/{addrReader["apartment_number"]}";

                    Address addr = new Address(addrReader);
                    Company company = new Company(companyReader, addr);
                    Assert.AreEqual(company.ToString(), correctString);
                }
            }
        }
    }
}
