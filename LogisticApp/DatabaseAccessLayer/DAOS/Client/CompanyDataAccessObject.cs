using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using LogisticApp.DatabaseAccessLayer;

namespace LogisticApp.DatabaseAccessLayer.DAOS.Client
{
    static class CompanyDataAccessObject
    {
        public static List<Company> getPaginatedCompanies(int start = 0, int number=25)
        {
            List<Company> companies = new List<Company>();
            using(var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM company LIMIT {start},{number};",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sbyte addr_id = sbyte.Parse(reader["address_id"].ToString());
                    Address address = AddressDataAccessObject.getAddressById(addr_id);
                    Company company = new Company(reader, address);
                    companies.Add(company);
                }
            }
            return companies;
        }

        public static Company createCompany(Company company)
        {
            Address addr = company.Addr;

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"INSERT INTO company {company.ToInsert()};", connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return null;
                }
                company.ID = command.LastInsertedId;
                addr = AddressDataAccessObject.createAddress(addr);
                if(addr == null)
                {
                    throw new Exception("Company adding address issue");
                }
                company.Addr = addr;
            }
            return company;
        }


        public static Company updateCompany(Company company)
        {
            Address addr = company.Addr;

            // check if has id
            if (company.ID == null)
            {
                return null;
            }

            addr = AddressDataAccessObject.updateAddress(addr);
            if (addr == null)
            {
                throw new Exception("Company updating address issue");
            }
            company.Addr = addr;


            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"UPDATE company SET " +
                    $"{company.ToUpdate()} WHERE id={company.ID}",
                    connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return null;
                }
                company.ID = command.LastInsertedId;
            }
            return company;
        }


        public static bool deleteCompany(Company company)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"DELETE FROM company WHERE id={company.ID}",
                    connection
                    );

                if (!AddressDataAccessObject.deleteAddresss(company.Addr))
                {
                    return false;
                }

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return false;
                }
            
            }
            return true;
        }
    }

}

