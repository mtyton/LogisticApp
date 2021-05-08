using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LogisticApp.Models
{
    public class DatabaseConnection
    {
        private MySqlConnection connection;
        
        private string getConnectionString()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "zaq1@WSX";
            builder.Database = "logisticapp";
            return builder.ConnectionString;
        }

        public DatabaseConnection()
        {
            string connectionString = getConnectionString();
            this.connection = new MySqlConnection(connectionString);
        }

        ~DatabaseConnection()
        {
            connection.Close();
        }

        public bool checkConnection()
        {
            /*This method checks if connection is still available*/
            return (this.connection != null && this.connection.State.ToString() != "Closed");
        }

        public void openConnection()
        {
            if (!checkConnection())
            {
                connection.Open();
            }
            
        }

        public void closeConnection()
        {
            connection.Close();
        }

        public object executeScalar(string queryString)
        {
            MySqlCommand command = new MySqlCommand(queryString, connection);
            try
            {
                command.ExecuteScalar();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
        
        public MySqlDataReader executeReader(string queryString)
        {
            /*This methodn return reader instance*/
            MySqlCommand command = new MySqlCommand(queryString, connection);
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (MySqlException)
            {
                // if something went wrong return null, because there is no data
                return null;
            }
        }

        public bool executeNonQuery(string queryString)
        {
            MySqlCommand command = new MySqlCommand(queryString, connection);
            try
            {
                command.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public bool checkIfTableExists(string tableName)
        {
            string queryString = $"SELECT * FROM {tableName};";
            return executeNonQuery(queryString);
        }


        public bool createTableIfNotExists(string tableName, string columns)
        {
            string queryString = $"CREATE TABLE {tableName} ({columns});";
            // check if table exists and than return true if it exists
            if (checkIfTableExists(tableName))
            {
                return true;
            }

            return executeNonQuery(queryString);
        }

        public bool dropTableIfExists(string tableName)
        {
            string queryString = $"DROP TABLE {tableName}";
            // check if table do not exist, if it does not, return true
            if (!checkIfTableExists(tableName))
            {
                return true;
            }
            return executeNonQuery(queryString);
        }

    }
}
