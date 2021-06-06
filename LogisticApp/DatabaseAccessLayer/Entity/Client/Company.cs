using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LogisticApp.DatabaseAccessLayer.Entity.Client
{
    class Company:BaseClient
    {
        private string companyName;
        Company(
            MySqlDataReader companyReader, 
            MySqlDataReader addressReader
            ) : base(addressReader)
        {
            companyName = companyReader["companyName"].ToString();
        }

    }
}
