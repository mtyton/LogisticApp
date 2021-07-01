using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Model
{
    class User
    {
        // user is a singleton class which allows to authenticate
        // Currently user is not connected into database, it 
        // may be introduced later
        private string username;
        private bool isAutheticated;

        public bool IsAuthenticated
        {
            get => isAutheticated;
        }

        public static User _instance = null;

        private User(string username, string password)
        {
            this.username = username;
            // password should not be kept in the computer memory
            // so we use it only to authenticate
            this.isAutheticated = this.authenticate(
                username, password
                );
        }

        private bool authenticate(string username, string password)
        {
            // TODO add database authentication
            return true;
        }

        public static User getUser(string username="Kowalski")
        {
            if (User._instance == null)
            {
                // TODO select user by id from database
                return new User(username, "1234567890");
            }
            return User._instance;
        }

    }
}
