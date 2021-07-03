using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using LogisticApp.DatabaseAccessLayer;
using System.Collections.ObjectModel;
using LogisticApp.DatabaseAccessLayer.Entity.Base;

namespace LogisticApp.DatabaseAccessLayer.DAOS.Client
{
    static class CompanyDataAccessObject
    {
        public static ObservableCollection<BaseEntity> getPaginated(int start = 0, int number=25)
        {
            ObservableCollection<BaseEntity> companies = new ObservableCollection<BaseEntity>();
            using(var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM company LIMIT {start},{number};",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Company company = new Company(reader);
                    companies.Add(company);
                }
                reader.Close();
            }
            foreach(Company company in companies)
            {
                Address address = AddressDataAccessObject.getAddressById(company.AddrId);
                company.address = address;
            }
            

            return companies;
        }

        public static int getTotalCount()
        {
            int count = 0;
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT Count(*) as number FROM company;",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    count = int.Parse(reader["number"].ToString());
                }
                reader.Close();
            }
            return count;
        }

        public static Company getById(long id)
        {
            Company company = null;
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM company WHERE id={id}",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    company = new Company(reader);
                }
                reader.Close();
            }

            Address address = AddressDataAccessObject.getAddressById(company.AddrId);
            company.address = address;
            return company;
        }

        public static Company create(Company company)
        {
            Address addr = company.address;

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"INSERT INTO company {company.ToInsert()};", connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException)
                {
                    return null;
                }
                company.id = command.LastInsertedId;
                addr = AddressDataAccessObject.create(addr);
                if(addr == null)
                {
                    throw new Exception("Company adding address issue");
                }
                company.address = addr;
            }
            return company;
        }


        public static Company update(Company company)
        {
            Address addr = company.address;

            // check if has id
            if (company.id == null)
            {
                return null;
            }

            addr = AddressDataAccessObject.update(addr);
            if (addr == null)
            {
                throw new Exception("Company updating address issue");
            }
            company.address = addr;


            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"UPDATE company SET " +
                    $"{company.ToUpdate()} WHERE id={company.id}",
                    connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException)
                {
                    return null;
                }
                company.id = command.LastInsertedId;
            }
            return company;
        }


        public static bool delete(Company company)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"DELETE FROM company WHERE id={company.id}",
                    connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException)
                {
                    return false;
                }
            
            }

            if (!AddressDataAccessObject.delete(company.address))
            {
                return false;
            }

            return true;
        }
    }

}

