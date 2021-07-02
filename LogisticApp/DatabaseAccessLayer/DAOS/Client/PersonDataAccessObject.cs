﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using LogisticApp.DatabaseAccessLayer;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using LogisticApp.DatabaseAccessLayer.Entity.Base;

namespace LogisticApp.DatabaseAccessLayer.DAOS.Client
{
    class PersonDataAccessObject
    {
        public static ObservableCollection<BaseEntity> getPaginated(int start = 0, int number = 25)
        {
            ObservableCollection<BaseEntity> people = new ObservableCollection<BaseEntity>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM person LIMIT {start},{number};",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Person person = new Person(reader);
                    people.Add(person);
                }
                reader.Close();

                foreach(Person person in people)
                {
                    Address address = AddressDataAccessObject.getAddressById(person.AddrId);
                    person.Addr = address;
                }
            }
            return people;
        }

        public static Person create(Person person)
        {
            Address addr = person.Addr;

            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"INSERT INTO person {person.ToInsert()};", connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return null;
                }
                person.ID = command.LastInsertedId;
                addr = AddressDataAccessObject.create(addr);
                if (addr == null)
                {
                    throw new Exception("Company adding address issue");
                }
                person.Addr = addr;
            }
            return person;
        }

        public static Person update(Person person)
        {
            Address addr = person.Addr;

            // check if has id
            if (person.ID == null)
            {
                return null;
            }

            addr = AddressDataAccessObject.update(addr);
            if (addr == null)
            {
                throw new Exception("Company updating address issue");
            }
            person.Addr = addr;


            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"UPDATE person SET {person.ToUpdate()} WHERE id={addr.ID}",
                    connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return null;
                }
                person.ID = command.LastInsertedId;
            }
            return person;
        }

        public static bool delete(Person person)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"DELETE FROM person WHERE id={person.ID}",
                    connection
                    );

                if (!AddressDataAccessObject.delete(person.Addr))
                {
                    return false;
                }

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    return false;
                }

            }
            return true;
        }


    }
}
