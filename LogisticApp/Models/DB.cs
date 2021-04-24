using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LogisticApp
{
    interface DB
    {
        void closeConnection();

        bool checkConnection();
        bool insertData(string[] data, string[] fields, string table);
        bool updateData(string[] data, string [] fields, string table);
        bool selectData(string[] fields, string table);
    }
}
