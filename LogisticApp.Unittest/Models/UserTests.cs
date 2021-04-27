using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogisticApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Models.Tests
{
    [TestClass()]
    public class UserTests
    {
        [TestMethod()]
        public void testToString_Success()
        {
            User user = new User("Test01", "Test@test.pl");
            string strUser = user.ToString();
            Assert.AreEqual(strUser, "Test01");
        }

    }
}