using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LogisticApp.Models
{
    public class DatabaseConnection : DB
    {
        private SqlConnection connection;
        
        private string getConnectionString(
            string server, string username, string password, string database
            )
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = server;
            builder.UserID = username;
            builder.Password = password;
            builder.InitialCatalog = database;
            return builder.ConnectionString;
        }

        public DatabaseConnection(string server, string username, 
            string password, string database="LogisticApp")
        {
            string connectionString = this.getConnectionString(server, username,
                password, database);
            this.connection = new SqlConnection(connectionString);
        }

        public bool checkConnection()
        {
            /*This method checks if connection is still available*/
            return (this.connection != null && 
                this.connection.State != ConnectionState.Closed);
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
    }
}
