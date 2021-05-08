using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Models
{
    public class User
    {
        private string username;
        private string email;
        // TODO add privlages

        public User(string name, string mail)
        {
            username = name;
            email = mail;
        }

        public override string ToString()
        {
            return username;
        }

        public void getData()
        {
            // This method should get data from database and set it to user
        }

        public void login()
        {
            // this method should provide login
            // possibly that this will be mocked currently
        }

    }
}
