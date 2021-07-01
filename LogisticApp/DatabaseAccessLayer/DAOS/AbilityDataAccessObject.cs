using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.Entity;
using MySql.Data.MySqlClient;


namespace LogisticApp.DatabaseAccessLayer.DAOS
{
    class AbilityDataAccessObject
    {
        public static List<Ability> getAll()
        {
            List<Ability> abilities = new List<Ability>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM ability", connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ability ability = new Ability(reader);
                    abilities.Add(ability);
                }
            }
            return abilities;
        }
        
        public static List<Ability> getPaginated(int start = 0, int number = 0)
        {
            return AbilityDataAccessObject.getAll();
        }

        public static List<Ability> getEmployeeAbilities(long employeeID)
        {
            List<Ability> abilities = new List<Ability>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM employeeability WHERE employee_id={employeeID}",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ability ability = new Ability(reader);
                    abilities.Add(ability);
                }
            }
            return abilities;
        }

    }
}
