﻿using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.DatabaseAccessLayer.Entity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Model
{
    class EntityFactory
    {
        // this class implements factory method to get proper entity depends on name and params
        public BaseEntity createEntity(string entityName, object[] entityValues)
        {
            // here we invoke proper method depending on entityName
            // TODO add other entities
            switch (entityName)
            {
                case "address":
                    return this.createAddressEntity(entityValues);
                case "company":
                    return this.createCompanyEntity(entityValues);
            }

            return null;
        }

        private Address createAddressEntity(object[] entityValues)
        {
            string city = entityValues[0].ToString();
            string country = entityValues[1].ToString();
            string street = entityValues[2].ToString();
            string postalCode = entityValues[3].ToString();
            int buildingNumber = int.Parse(entityValues[4].ToString());
            int apartmentNumber = int.Parse(entityValues[5].ToString());
            return new Address(
                city, postalCode, country, street, buildingNumber, apartmentNumber
                );
        }

        private Company createCompanyEntity(object[] entityValues)
        {
            string name = entityValues[0].ToString();
            string taxNumber = entityValues[1].ToString();
            Address address = (Address)entityValues[2];
            return new Company(name, taxNumber, address);
        }

        // TODO create proper methods for person/job/e,ployee also
    }
}