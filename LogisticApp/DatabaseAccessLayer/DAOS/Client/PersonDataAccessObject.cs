using System;
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
                    person.address = address;
                }
            }
            return people;
        }

        public static int getTotalCount()
        {
            int count = 0;
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT Count(*) as number FROM person;",
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

        public static Person getById(long id)
        {
            Person person = null;
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM person WHERE id={id}",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    person = new Person(reader);
                }
                reader.Close();
            }
            Address address = AddressDataAccessObject.getAddressById(person.AddrId);
            person.address = address;

            return person;
        }

        public static Person create(Person person)
        {
            Address addr = person.address;

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
                person.id = command.LastInsertedId;
                addr = AddressDataAccessObject.create(addr);
                if (addr == null)
                {
                    throw new Exception("Company adding address issue");
                }
                person.address = addr;
            }
            return person;
        }

        public static Person update(Person person)
        {
            Address addr = person.address;

            // check if has id
            if (person.id == null)
            {
                return null;
            }

            addr = AddressDataAccessObject.update(addr);
            if (addr == null)
            {
                throw new Exception("Company updating address issue");
            }
            person.address = addr;


            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"UPDATE person SET {person.ToUpdate()} WHERE id={addr.id}",
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
                person.id = command.LastInsertedId;
            }
            return person;
        }

        public static bool delete(Person person)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"DELETE FROM person WHERE id={person.id}",
                    connection
                    );

                if (!AddressDataAccessObject.delete(person.address))
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
