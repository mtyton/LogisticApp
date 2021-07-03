﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.Entity.Base;
using System.Data;


namespace LogisticApp.DatabaseAccessLayer.Entity
{
    public class Employee: BaseEntity
    {
        public long id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime birthDate { get; set; }
        public DateTime dateOfEmployment { get; set; }
        public int hourlyPayment { get; set; }
        public List<Skillset> abilities { get; set; }

        public Employee(IDataReader reader)
        {
            this.id = long.Parse(reader["id"].ToString());
            this.name = reader["name"].ToString();
            this.surname = reader["surname"].ToString();
            this.birthDate = DateTime.Parse(reader["birth_date"].ToString());
            this.dateOfEmployment = DateTime.Parse(
                reader["date_of_employment"].ToString()
                );
            this.hourlyPayment = int.Parse(reader["hourly_payment"].ToString());
        }

        public Employee(
            string name, string surname, 
            DateTime birthDate, DateTime dateOfEmployment, 
            int hourlyPayment, List<Skillset> abilities
            )
        {
            this.name = name;
            this.surname = surname;
            this.birthDate = birthDate;
            this.dateOfEmployment = dateOfEmployment;
            this.hourlyPayment = hourlyPayment;
            this.abilities = abilities;
        }

        public override bool checkIfRecordComplete()
        {
            return (
                this.name != null && this.surname != null &&
                this.hourlyPayment != 0 && this.abilities!=null
                );
        }

        public override string ToString()
        {
            return $"{this.name} {this.surname}";
        }

        public string ToInsert()
        {
            base.ToInsert();
            return $"(name, surname, birth_date, " +
                $"date_of_employment, hourly_payment)" +
                $"VALUES({this.name}, {this.surname}, " +
                $"{this.birthDate}, " +
                $"{this.dateOfEmployment}, " +
                $"{this.hourlyPayment});";
        }

        public string ToUpdate()
        {
            base.ToUpdate();
            return $"name={this.name}, surname={this.surname}, " +
                $"birth_date={this.birthDate}, " +
                $"date_of_employment={this.dateOfEmployment}," +
                $"hourly_payment={this.hourlyPayment};";
        }
    }
}
