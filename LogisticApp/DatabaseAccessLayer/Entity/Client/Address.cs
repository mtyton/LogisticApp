using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using LogisticApp.DatabaseAccessLayer.Entity.Base;

namespace LogisticApp.DatabaseAccessLayer.Entity.Client
{
    public class Address: BaseEntity
    {
        public string city { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public string street { get; set; }
        public int buildingNumber { get; set; }
        public int apartmentNumber { get; set; }



        // constructor for data loading from db
        public Address(IDataReader reader)
        {
            this.id = long.Parse(reader["id"].ToString());
            this.city = reader["city"].ToString();
            this.postalCode = reader["postal_code"].ToString();
            this.country = reader["country"].ToString();
            this.street = reader["street"].ToString();
            this.buildingNumber = int.Parse(
                reader["building_number"].ToString()
            );

            if (reader["apartment_number"].ToString() != "")
            {
                this.apartmentNumber = int.Parse(
                    reader["apartment_number"].ToString()
                 );
            }
            else
            {
                this.apartmentNumber = 0;
            }

        }

        // constructor to create object in app
        public Address(
            string city, string postalCode, 
            string country, string street,
            int buildingNumber, int apartmentNumber
            )
        {
            this.city = city;
            this.postalCode = postalCode;
            this.country = country;
            this.street = street;
            this.buildingNumber = buildingNumber;
            this.apartmentNumber = apartmentNumber;

        }

        public override bool checkIfRecordComplete()
        {
            return (
                this.city != null && 
                this.country != null && 
                this.street != null && 
                this.buildingNumber != 0
                );
        }

        public string ToInsert()
        {
            base.ToInsert();
            return $"" +
                $"(city, postal_code, country, street, building_number, apartment_number)" +
                $" VALUES ('{this.city}', '{this.postalCode}', '{this.country}', '{this.street}', " +
                $"{this.buildingNumber}, {this.apartmentNumber});";
        }

        public string ToUpdate()
        {
            base.ToUpdate();
            return $"city='{this.city}', postal_code='{this.postalCode}', " +
                $"country='{this.country}', street='{this.street}', " +
                $"building_number={this.buildingNumber}, apartment_number={this.apartmentNumber}";
        }

        public override string ToString()
        {
            return $"{country}, {city}, {postalCode}, " +
                $"{street} {buildingNumber}/{apartmentNumber}";
        }
    }
}
