using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.DatabaseAccessLayer.Entity
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

    }
}
