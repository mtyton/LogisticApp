using LogisticApp.DatabaseAccessLayer.DAOS;
using LogisticApp.DatabaseAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Model
{
    static class AuthModel
    {
        // This class suppose to only authenticate user
        public static bool authenticate(string username, string password)
        {
            string dbPassword = UserDataAccessObject.getUserPassword(username);
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] hashedPassword;

            hashedPassword = algorithm.ComputeHash(Encoding.ASCII.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hashedPassword.Length; i++)
            {
                builder.Append(hashedPassword[i].ToString("x2"));
            }
            password = builder.ToString();

            return dbPassword == password;
        }
    }
}
