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
    class EmployeeDataAccessObject
    {

        public static ObservableCollection<BaseEntity> getPaginated(int start = 0, int number = 0)
        {
            ObservableCollection<BaseEntity> employees = new ObservableCollection<BaseEntity>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM employee LIMIT {start},{number};", connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Employee emploee = new Employee(reader);
                    employees.Add(emploee);
                }
                reader.Close();
            }

            return employees;
        }

        public static int getTotalCount()
        {
            int count = 0;
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT Count(*) as number FROM employee;",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    count = int.Parse(reader["number"].ToString());
                }
                reader.Close();
            }
            return count;
        }

        public static Employee getById(long id)
        {
            Employee employee = null;
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM employee WHERE id={id}",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    employee = new Employee(reader);
                }
                reader.Close();
            }

            return employee;
        }

        public static Employee create(Employee employee)
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
                employee.id = command.LastInsertedId;
            }
            return employee;
        }

        public static Employee update(Employee employee)
        {
            // check if has id
            if (employee.id == null)
            {
                return null;
            }
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"UPDATE employee SET {employee.ToUpdate()} WHERE id={employee.id}",
                    connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException)
                {
                    return null;
                }
            }
            return employee;
        }

        public static bool delete(Employee employee)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"DELETE FROM employee WHERE id={employee.id}",
                    connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
