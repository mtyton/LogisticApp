using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Moq;

namespace LogisticAppTests.DatabaseAccessLayerTests
{
    class EntityDataMock
    {
        public static IDataReader mockAddressReader()
        {
            var moq = new Mock<IDataReader>();
            moq.Setup(x => x.Read()).Returns(true);
            moq.Setup(x => x.Read()).Returns(false);
            moq.SetupGet<object>(x => x["id"]).Returns("2");
            moq.SetupGet<object>(x => x["city"]).Returns("testCity1");
            moq.SetupGet<object>(x => x["postal_code"]).Returns("000");
            moq.SetupGet<object>(x => x["country"]).Returns("testCountry");
            moq.SetupGet<object>(x => x["street"]).Returns("str");
            moq.SetupGet<object>(x => x["apartment_number"]).Returns("12");
            moq.SetupGet<object>(x => x["building_number"]).Returns("123");
            return moq.Object;
        }

        public static IDataReader mockCompanyReader()
        {
            var moq = new Mock<IDataReader>();
            moq.Setup(x => x.Read()).Returns(true);
            moq.Setup(x => x.Read()).Returns(false);
            moq.SetupGet<object>(x => x["id"]).Returns("2");
            moq.SetupGet<object>(x => x["name"]).Returns("testCompany");
            moq.SetupGet<object>(x => x["tax_number"]).Returns("457");
            moq.SetupGet<object>(x => x["address_id"]).Returns("1");
            return moq.Object;
        }
        
        public static IDataReader mockPersonReader()
        {
            var moq = new Mock<IDataReader>();
            moq.Setup(x => x.Read()).Returns(true);
            moq.Setup(x => x.Read()).Returns(false);
            moq.SetupGet<object>(x => x["id"]).Returns("14");
            moq.SetupGet<object>(x => x["name"]).Returns("John");
            moq.SetupGet<object>(x => x["surname"]).Returns("Doe");
            moq.SetupGet<object>(x => x["address_id"]).Returns("1");
            return moq.Object;
        }

        public static IDataReader mockSkillReader()
        {
            var moq = new Mock<IDataReader>();
            moq.Setup(x => x.Read()).Returns(true);
            moq.Setup(x => x.Read()).Returns(false);
            moq.SetupGet<object>(x => x["id"]).Returns("2");
            moq.SetupGet<object>(x => x["skill_name"]).Returns("welding");
            return moq.Object;
        }

        public static IDataReader mockEmployeeReader()
        {
            var moq = new Mock<IDataReader>();
            moq.Setup(x => x.Read()).Returns(true);
            moq.Setup(x => x.Read()).Returns(false);
            moq.SetupGet<object>(x => x["id"]).Returns("22");
            moq.SetupGet<object>(x => x["name"]).Returns("testName");
            moq.SetupGet<object>(x => x["surname"]).Returns("testSurname");
            moq.SetupGet<object>(x => x["birth_date"]).Returns("1998-10-01 07:30:00");
            moq.SetupGet<object>(x => x["date_of_employment"]).Returns("2015-10-01 07:30:00");
            moq.SetupGet<object>(x => x["hourly_payment"]).Returns("15");
            return moq.Object;
        }

        public static IDataReader mockJobReader()
        {
            var moq = new Mock<IDataReader>();
            moq.Setup(x => x.Read()).Returns(true);
            moq.Setup(x => x.Read()).Returns(false);
            moq.SetupGet<object>(x => x["id"]).Returns("22");
            moq.SetupGet<object>(x => x["title"]).Returns("testTitle");
            moq.SetupGet<object>(x => x["description"]).Returns("testDesc");
            moq.SetupGet<object>(x => x["predicted_time"]).Returns("30");
            moq.SetupGet<object>(x => x["predicted_cost"]).Returns("30");
            moq.SetupGet<object>(x => x["person_id"]).Returns("245");
            moq.SetupGet<object>(x => x["company_id"]).Returns("23");
            moq.SetupGet<object>(x => x["assigned_employee"]).Returns("121");
            return moq.Object;
        }

    }
}
