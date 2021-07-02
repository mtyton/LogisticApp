using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LogisticApp.DatabaseAccessLayer
{
    public class DatabaseConnection
    {
        /*
         Database connection is a simple implementation of Singletion design pattern.
        There suppose to be only one instance of this class in whole project.
         */

        private static DatabaseConnection instance = null;
            
        public static DatabaseConnection Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DatabaseConnection();
                }
                return instance;
            }
        }

        private MySqlConnection connection;

        public MySqlConnection Connection
        {
            get
            {
                connection.Close();
                connection.Open();
                return connection;
            }
        }

        // TODO move this to some kind of settings file
        private string getConnectionString()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = Properties.Settings.Default.dbServer;
            builder.UserID = Properties.Settings.Default.dbUsername;
            builder.Password = Properties.Settings.Default.dbPassword;
            builder.Database = Properties.Settings.Default.databaseName;
            builder.Port = Properties.Settings.Default.dbPort;
            return builder.ConnectionString;
        }

        private DatabaseConnection()
        {
            string connectionString = getConnectionString();
            this.connection = new MySqlConnection(connectionString);
        }

        ~DatabaseConnection()
        {
            this.connection.Close();
        }



    }
}
