using LogisticApp.DatabaseAccessLayer.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.DatabaseAccessLayer.DAOS
{
    static class UserDataAccessObject
    {
        public static User getUserByUsername(string username)
        {
            User user = null;
            if(username == null)
            {
                return user;
            }
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM user WHERE username='{username}';", connection
                    );
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new User(reader);
                }
                reader.Close();
            }
            return user;
        }

        public static string getUserPassword(string username)
        {
            string password = null;
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT password FROM user WHERE username='{username}';", connection
                    );
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    password = reader["password"].ToString();
                }
                reader.Close();
            }
            return password;
        }

    }
}
