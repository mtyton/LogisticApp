using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using LogisticApp.DatabaseAccessLayer;

namespace LogisticApp.DatabaseAccessLayer.Repos.Client
{
    static class CompanyRepository
    {
        public static List<Company> getAllCompanies()
        {
            List<Company> companies = new List<Company>();
            using(var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM company;", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // get addr here
                }
            }
            return companies;
        }

    }
}
