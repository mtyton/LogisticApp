using LogisticApp.DatabaseAccessLayer.Entity.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.DatabaseAccessLayer.Entity
{
    public class User : BaseEntity
    {
        private string _username;
        private long _id;

        public long ID
        {
            get => _id;
            set => _id = value;
        }
        // TODO add privlages

        public User(IDataReader reader)
        {
            _username = reader["username"].ToString();
        }

        public User(string name, string mail)
        {
            _username = name;
        }

        public override string ToString()
        {
            return _username;
        }

    }
}
