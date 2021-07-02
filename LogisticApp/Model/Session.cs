using LogisticApp.DatabaseAccessLayer.DAOS;
using LogisticApp.DatabaseAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Model
{
    class Session
    {
        // keeps info about user
        // session is a singleton because only one session is allowed at the time
        private User _user;
        private bool _isAutheticated;

        public bool IsAuthenticated
        {
            get => _isAutheticated;
        }

        private static Session _instance = null;

        private Session(string username, string password)
        {
            this._user = UserDataAccessObject.getUserByUsername(username);
            if (this._user == null)
            {
                throw new AuthenticationException("Username or password was incorrect");
            }

            this._isAutheticated = AuthModel.authenticate(username, password);
        }

        public static Session getOrCreateSession(string username, string password)
        {
            if(_instance == null)
            {
                return new Session(username, password);
            }
            return _instance;
        }
    }
}
