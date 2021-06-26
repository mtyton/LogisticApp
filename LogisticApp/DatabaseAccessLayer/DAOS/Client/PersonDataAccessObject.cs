using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using LogisticApp.DatabaseAccessLayer;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace LogisticApp.DatabaseAccessLayer.DAOS.Client
{
    class PersonDataAccessObject
    {
        public static List<Person> getPaginatedPeople(int start = 0, int number = 25)
        {
            List<Person> people = new List<Person>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM person LIMIT {start},{number};",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sbyte addr_id = sbyte.Parse(reader["address_id"].ToString());
                    Address address = AddressDataAccessObject.getAddressById(addr_id);
                    Person person = new Person(reader, address);
                    people.Add(person);
                }
            }
            return people;
        }

        public static Person createPerson(Person person)
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
                addr = AddressDataAccessObject.createAddress(addr);
                if (addr == null)
                {
                    throw new Exception("Company adding address issue");
                }
                person.Addr = addr;
            }
            return person;
        }

        public static Person updatePerson(Person person)
        {
            Address addr = person.Addr;

            // check if has id
            if (person.ID == null)
            {
                return null;
            }

            addr = AddressDataAccessObject.updateAddress(addr);
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

        public static bool deletePerson(Person person)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"DELETE FROM person WHERE id={person.ID}",
                    connection
                    );

                if (!AddressDataAccessObject.deleteAddresss(person.Addr))
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
