using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LogisticApp.Models
{
    public class DatabaseConnection : DB
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

        public bool insertData(string[] data, string[] fields, string table)
        {
            throw new NotImplementedException();
        }

        public bool selectData(string[] fields, string table)
        {
            throw new NotImplementedException();
        }

        public bool updateData(string[] data, string[] fields, string table)
        {
            throw new NotImplementedException();
        }

        public bool createTable(string tableName)
        {
            string queryString = $"CREATE TABLE {tableName};";
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
    }
}
