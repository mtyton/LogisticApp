using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using System;

namespace LogisticAppTests.DatabaseAccessLayerTests.Enitity.Client
{
    [TestClass]
    public class CompanyTest
    {
        [TestMethod]
        public void TestToString()
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
                    Company company = new Company(companyReader);
                    company.Addr = addr;
                    Assert.AreEqual(company.ToString(), correctString);
                }
            }
        }

        [TestMethod]
        public void TestToInsertSuccess()
        {
            using (var companyReader = EntityDataMock.mockCompanyReader())
            {
                using (var addrReader = EntityDataMock.mockAddressReader())
                {
                    Address addr = new Address(addrReader);
                    addr.ID = 5;
                    Company company = new Company(companyReader);
                    company.Addr = addr;
                    string correctString = $"(id, name, tax_number, address_id) " +
                    $"VALUES (NULL, '{companyReader["name"]}', '{companyReader["tax_number"]}', " +
                    $"{addr.ID});";

                    Assert.AreEqual(company.ToInsert(), correctString);
                }
            }
        }

        [TestMethod]
        public void TestToInsertFailure()
        {
            Company comp = new Company(null, null, null);
            try
            {
                comp.ToInsert();
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
            using (var companyReader = EntityDataMock.mockCompanyReader())
            {
                using (var addrReader = EntityDataMock.mockAddressReader())
                {
                    Address addr = new Address(addrReader);
                    addr.ID = 5;
                    Company company = new Company(companyReader);
                    company.Addr = addr;
                    string correctString = $"name='{companyReader["name"]}', " +
                        $"tax_number='{companyReader["tax_number"]}'," + 
                        $" address_id={addr.ID}";

                    Assert.AreEqual(company.ToUpdate(), correctString);
                }
            }
        }

        [TestMethod]
        public void TestToUpdateFailure()
        {
            Company comp = new Company(null, null, null);
            try
            {
                comp.ToUpdate();
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
