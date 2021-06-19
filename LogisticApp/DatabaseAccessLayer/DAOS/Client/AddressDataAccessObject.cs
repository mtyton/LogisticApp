using System;
using System.Collections.Generic;
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
        public static Address getAddressById(sbyte id)
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
            }
            return addr;
        }

        public static Address createAddress(Address addr)
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
                catch (MySqlException e)
                {
                    return null;
                }
                addr.ID = command.LastInsertedId;
                
            }
            return addr;
        }

        public static Address updateAddress(Address addr)
        {
            // check if has id
            if (addr.ID == null)
            {
                return null;
            }
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"UPDATE address SET {addr.ToUpdate()} WHERE id={addr.ID}", 
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
            }
            return addr;
        }

        public static bool deleteAddresss(Address addr)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"DELETE FROM address WHERE id={addr.ID}",
                    connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return false;
                }
                addr.ID = command.LastInsertedId;

            }
            return true;
        }

    }
}
