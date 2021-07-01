using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.Entity;
using MySql.Data.MySqlClient;

namespace LogisticApp.DatabaseAccessLayer.DAOS
{
    class EmployeeDataAccessObject
    {
        public static List<Employee> getAll()
        {
            List<Employee> employees = new List<Employee>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM employee;", connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sbyte addr_id = sbyte.Parse(reader["address_id"].ToString());
                    List<Ability> abilities = AbilityDataAccessObject.getEmployeeAbilities(
                        long.Parse(reader["id"].ToString())
                        );
                    Employee emploee = new Employee(reader, abilities);
                    employees.Add(emploee);
                }
            }
            return employees;
        }
        
        public static Employee createEmployee(Employee employee)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"INSERT INTO employee {employee.ToInsert()};", connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return null;
                }
                employee.ID = command.LastInsertedId;
            }
            return employee;
        }

    }
}
