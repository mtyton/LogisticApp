using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.DatabaseAccessLayer.Entity.Client;

namespace LogisticApp.DatabaseAccessLayer.Entity
{
    public class Job : BaseEntity
    {
        public long id { get; set; }
        public string title { get; set; }
        public Person clientPerson { get; set; }
        public Company clientCompany { get; set; }
        public string description { get; set; }
        public Employee assignedEmployee { get; set; }
        public int predictedTime { get; set; }
        public int predictedCost { get; set; }


        public long ClientPersonID { get; set; }
        public long ClientCompanyID { get; set; }
        public long AssignedEmployeeID { get; set; }
        

        public Person ClientPerson
        {
            get=> clientPerson; 
            set=> clientPerson = value;
        }
        public Company ClientCompany 
        { 
            get => clientCompany; 
            set => clientCompany = value; 
        }
        public Employee AssignedEmployee 
        { 
            get=> assignedEmployee; 
            set=> assignedEmployee = value;
        }

        private void loadForeignKeys(IDataReader reader)
        {
            this.AssignedEmployeeID = 0;
            this.ClientCompanyID = 0;
            this.ClientPersonID = 0;


            if (reader["person_id"].ToString() != "")
            {
                this.AssignedEmployeeID = long.Parse(reader["person_id"].ToString());
            }
            if (reader["company_id"].ToString() != "")
            {
                this.ClientCompanyID = long.Parse(reader["company_id"].ToString());
            }
            if (reader["assigned_employee"].ToString() != "")
            {
                this.ClientPersonID = long.Parse(reader["assigned_employee"].ToString());
            }
        }

        public Job(IDataReader reader)
        {
            this.id = long.Parse(reader["id"].ToString());
            this.title = reader["title"].ToString();
            this.description = reader["description"].ToString();
            this.predictedTime = int.Parse(reader["predicted_time"].ToString());
            this.predictedCost = int.Parse(reader["predicted_cost"].ToString());
            this.loadForeignKeys(reader);
        }

        public Job(string title, string description, 
            int predictedTime, int predictedCost)
        {
            this.title = title;
            this.description = description;
            this.predictedCost = predictedCost;
            this.predictedTime = predictedTime;
        }

        public override bool checkIfRecordComplete()
        {
            return (
                (clientPerson != null || clientCompany != null)
                && assignedEmployee != null
                );
        }

        public override string ToString()
        {
            return $"{this.title}";
        }

        public string ToInsert()
        {
            base.ToInsert();
            if (this.clientCompany != null)
            {
                return $"(title, company_id, description, " +
                    $"assigned_employee, predicted_time, predicted_cost) " +
                    $"VALUES ({this.title}, {this.clientCompany.id}," +
                    $" {this.description}, {this.assignedEmployee}," +
                    $" {this.predictedTime}, {this.predictedCost});";
            }

            return $"(title, person_id, description, " +
                    $"assigned_employee, predicted_time, predicted_cost) " +
                    $"VALUES ({this.title}, {this.clientPerson.id}," +
                    $" {this.description}, {this.assignedEmployee}," +
                    $" {this.predictedTime}, {this.predictedCost});";
        }

        public string ToUpdate()
        {
            base.ToUpdate();
            if (this.clientCompany != null)
            {
                return $"title={this.title}, description={this.description}, " +
                    $"company_id={this.clientCompany.id}, " +
                    $"assigned_employee={this.assignedEmployee.id}, " +
                    $"predicted_time={this.predictedTime}, " +
                    $"predicted_cost={this.predictedCost};";
            }

            return $"title={this.title}, description={this.description}, " +
                    $"person_id={this.clientPerson.id}, " +
                    $"assigned_employee={this.assignedEmployee.id}, " +
                    $"predicted_time={this.predictedTime}, " +
                    $"predicted_cost={this.predictedCost};";
        }
    }
}
