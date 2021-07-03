using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using MySql.Data.MySqlClient;

namespace LogisticApp.DatabaseAccessLayer.DAOS.Client
{
    // DAO - Database Access Object
    static class AddressDataAccessObject
    {
        public static ObservableCollection<Address> getPaginated(int start=0, int number_of_records = 25)
        {
            throw new NotImplementedException();
        }

        public static Address getAddressById(long id)
        {
            Address addr = null;
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM address WHERE id={id};", connection
                    );
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    addr = new Address(reader);
                }
                reader.Close();
            }
            return addr;
        }

        public static Address create(Address addr)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"INSERT INTO address {addr.ToInsert()}", connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException)
                {
                    return null;
                }
                addr.id = command.LastInsertedId;
                
            }
            return addr;
        }

        public static Address update(Address addr)
        {
            // check if has id
            if (addr.id == null)
            {
                return null;
            }
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"UPDATE address SET {addr.ToUpdate()} WHERE id={addr.id}", 
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
            }
            return addr;
        }

        public static bool delete(Address addr)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"DELETE FROM address WHERE id={addr.id}",
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
            return true;
        }

    }
}
