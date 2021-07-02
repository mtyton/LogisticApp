﻿using System;
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
        public static ObservableCollection<BaseEntity> getAll()
        {
            ObservableCollection<BaseEntity> employees = new ObservableCollection<BaseEntity>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM employee;", connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sbyte addr_id = sbyte.Parse(reader["address_id"].ToString());
                    Employee emploee = new Employee(reader);
                    employees.Add(emploee);
                }
                reader.Close();
            }
            foreach(Employee employee in employees)
            {
                List<Ability> abilities = AbilityDataAccessObject.getEmployeeAbilities(
                        employee.ID
                    );
                employee.Abilities = abilities;
            }

            return employees;
        }

        public static ObservableCollection<BaseEntity> getPaginated(int start = 0, int number = 0)
        {
            return EmployeeDataAccessObject.getAll();
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
                employee.ID = command.LastInsertedId;
            }
            return employee;
        }

    }
}
