using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Base;
using MySql.Data.MySqlClient;


namespace LogisticApp.DatabaseAccessLayer.DAOS
{
    class AbilityDataAccessObject
    {
        public static ObservableCollection<BaseEntity> getAll()
        {
            ObservableCollection<BaseEntity> abilities = new ObservableCollection<BaseEntity>();
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
                reader.Close();
            }
            return abilities;
        }
        
        public static ObservableCollection<BaseEntity> getPaginated(int start = 0, int number = 0)
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
