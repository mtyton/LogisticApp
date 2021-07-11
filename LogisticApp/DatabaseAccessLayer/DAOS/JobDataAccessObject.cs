using LogisticApp.DatabaseAccessLayer.DAOS.Client;
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
                    if (job.ClientCompanyID != 0)
                    {
                        job.clientCompany = CompanyDataAccessObject.getById(job.ClientCompanyID);
                    }
                    else if (job.ClientPersonID != 0)
                    {
                        job.clientPerson = PersonDataAccessObject.getById(job.ClientPersonID);
                    }
                    if(job.AssignedEmployeeID != 0)
                    {
                        job.assignedEmployee = EmployeeDataAccessObject.getById(job.AssignedEmployeeID);
                    }
                    
                }
            }
            return jobs;
        }

        public static int getTotalCount()
        {
            int count = 0;
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"SELECT Count(*) as number FROM job;",
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
                job.id = command.LastInsertedId;

            }
            return job;
        }

        public static Job update(Job job)
        {
            // check if has id
            if (job.id == null)
            {
                return null;
            }
            using (var connection = DatabaseConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(
                    $"UPDATE job SET {job.ToUpdate()} WHERE id={job.id};",
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
                    $"DELETE FROM job WHERE id={job.id}",
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
