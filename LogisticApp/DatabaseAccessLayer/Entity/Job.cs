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
        private long id;
        private string title;
        private Person clientPerson;
        private Company clientCompany;
        private string description;
        private Employee assignedEmployee;
        private int predictedTime;
        private int predictedCost;

        public long ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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

        public Job(IDataReader reader)
        {
            this.id = long.Parse(reader["id"].ToString());
            this.title = reader["title"].ToString();
            this.description = reader["description"].ToString();
            this.predictedTime = int.Parse(reader["predicted_time"].ToString());
            this.predictedCost = int.Parse(reader["predicted_cost"].ToString());
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
                    $"VALUES ({this.title}, {this.clientCompany.ID}," +
                    $" {this.description}, {this.assignedEmployee}," +
                    $" {this.predictedTime}, {this.predictedCost});";
            }

            return $"(title, person_id, description, " +
                    $"assigned_employee, predicted_time, predicted_cost) " +
                    $"VALUES ({this.title}, {this.clientPerson.ID}," +
                    $" {this.description}, {this.assignedEmployee}," +
                    $" {this.predictedTime}, {this.predictedCost});";
        }

        public string ToUpdate()
        {
            base.ToUpdate();
            if (this.clientCompany != null)
            {
                return $"title={this.title}, description={this.description}, " +
                    $"company_id={this.clientCompany.ID}, " +
                    $"assigned_employee={this.assignedEmployee.ID}, " +
                    $"predicted_time={this.predictedTime}, " +
                    $"predicted_cost={this.predictedCost};";
            }

            return $"title={this.title}, description={this.description}, " +
                    $"person_id={this.clientPerson.ID}, " +
                    $"assigned_employee={this.assignedEmployee.ID}, " +
                    $"predicted_time={this.predictedTime}, " +
                    $"predicted_cost={this.predictedCost};";
        }
    }
}
