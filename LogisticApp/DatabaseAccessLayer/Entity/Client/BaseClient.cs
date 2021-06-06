using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LogisticApp.DatabaseAccessLayer.Entity.Client
{
    public class BaseClient
    {
        protected string city;
        protected string postalCode;
        protected string street;
        public BaseClient(MySqlDataReader reader)
        {
            city = reader["city"].ToString();
            postalCode = reader["postal-code"].ToString();
            street = reader["street"].ToString();
        }

    }
}
