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
    class SkillsetDataAccessObject
    {
        public static ObservableCollection<BaseEntity> getAll()
        {
            ObservableCollection<BaseEntity> abilities = new ObservableCollection<BaseEntity>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM employee_skillset", connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skillset ability = new Skillset(reader);
                    abilities.Add(ability);
                }
                reader.Close();
            }
            return abilities;
        }
        
        public static ObservableCollection<BaseEntity> getPaginated(int start = 0, int number = 0)
        {
            return SkillsetDataAccessObject.getAll();
        }

        public static List<Skillset> getEmployeeAbilities(long employeeID)
        {
            List<Skillset> abilities = new List<Skillset>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"select * from skillset where id IN (select id from employee_skillset Where employee_id={employeeID})",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Skillset ability = new Skillset(reader);
                    abilities.Add(ability);
                }
            }
            return abilities;
        }

    }
}
