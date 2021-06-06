using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LogisticApp.DatabaseAccessLayer.Entity.Client
{
    class Person: BaseClient
    {
        private string firstname, lastname;


        public Person(
            MySqlDataReader personReader, 
            MySqlDataReader addressReader
            ):base(addressReader)
        {
            firstname = personReader["firstname"].ToString();
            lastname = personReader["lastname"].ToString();
        }



    }
}
