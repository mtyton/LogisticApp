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
        public string username { get; set; }

        public User(IDataReader reader)
        {
            username = reader["username"].ToString();
        }

        public User(string name, string mail)
        {
            username = name;
        }

        public override string ToString()
        {
            return username;
        }

    }
}
