﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.Entity.Base;


namespace LogisticApp.DatabaseAccessLayer.Entity.Client
{
    public class Company: BaseEntity
    {
        private long id;
        private string companyName;
        private string taxNumber;
        private Address address;


        public long ID
        {
            get => id;
            set => id = value;
        }

        public Address Addr
        {
            get => address;
            set => address=value;
        }

        public Company(
            IDataReader companyReader,
            Address addr
            )
        {
            this.id = long.Parse(companyReader["id"].ToString());
            this.companyName = companyReader["name"].ToString();
            this.taxNumber = companyReader["tax_number"].ToString();
            this.address = addr;
        }

        public Company(string companyName, string taxNumber, Address addr)
        {
            this.companyName = companyName;
            this.taxNumber = taxNumber;
            this.address = addr;
        }

        public override bool checkIfRecordComplete()
        {
            return (
                this.companyName != null && 
                this.taxNumber != null && 
                this.address != null
                );
        }

        public string ToInsert()
        {
            base.ToInsert();
            return $"(id, name, tax_number, address_id) " +
                $"VALUES (NULL, {this.companyName}, {this.taxNumber}, " +
                $"{this.address.ID});";
        }

        public string ToUpdate()
        {
            base.ToUpdate();
            return $"name={this.companyName}, tax_number={this.taxNumber}," +
                $" address_id={this.address.ID}";
        }

        public override string ToString()
        {
            return $"{companyName} - {address?.ToString()}";
        }

    }
}