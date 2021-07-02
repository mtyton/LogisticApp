using LogisticApp.DatabaseAccessLayer.Entity;
using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.DatabaseAccessLayer.DAOS
{
    class JobDataAccessObject
    {

        public static ObservableCollection<BaseEntity> getPaginated(int start=0, int number_of_records=25)
        {
            ObservableCollection<BaseEntity> jobs = new ObservableCollection<BaseEntity>();
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT * FROM job LIMIT {start},{number_of_records};",
                    connection
                    );
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Job job = new Job(reader);
                    jobs.Add(job);
                }
                reader.Close();

                foreach (Job job in jobs)
                {
                    if (job.ClientCompanyID!=0)
                    {
                        Company company = CompanyDataAccessObject.getById()
                    }
                    else if (job.ClientPersonID!=0)
                    {

                    }
                    Address address = AddressDataAccessObject.getAddressById(person.AddrId);
                    person.Addr = address;
                }
            }
            return people;
        }

        public static Job create(Job job)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"INSERT INTO job {job.ToInsert()}", connection
                    );
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException)
                {
                    return null;
                }
                job.ID = command.LastInsertedId;

            }
            return job;
        }

        public static Job update(Job job)
        {
            // check if has id
            if (job.ID == null)
            {
                return null;
            }
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"UPDATE address SET {job.ToUpdate()} WHERE id={job.ID}",
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
            return job;
        }

        public static bool delete(Job job)
        {
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"DELETE FROM address WHERE id={job.ID}",
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
                job.ID = command.LastInsertedId;

            }
            return true;
        }

    }
}
