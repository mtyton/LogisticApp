using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LogisticApp.DatabaseAccessLayer.Entity.Base;

namespace LogisticApp.DatabaseAccessLayer.Entity.Client
{
    public class Person: BaseEntity
    {
        public long id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public Address address { get; set; }

        public long AddrId { get; set; }

        public Person(
            IDataReader personReader
            )
        {
            this.id = long.Parse(personReader["id"].ToString());
            this.name = personReader["name"].ToString();
            this.surname = personReader["surname"].ToString();
            this.AddrId = long.Parse(personReader["address_id"].ToString());
        }

        public Person(string name, string surname, Address addr)
        {
            this.name = name;
            this.surname = surname;
            this.address = addr;
        }

        public override bool checkIfRecordComplete()
        {
            return (
                this.name != null && this.surname != null &&
                this.address != null
                );
        }

        public string ToInsert()
        {
            base.ToInsert();
            return $"(id, name, surname, address_id) " +
                $"VALUES (NULL, '{this.name}', '{this.surname}', " +
                $"{this.address.id});";
        }


        public string ToUpdate()
        {
            base.ToUpdate();
            return $"name='{this.name}', surname='{this.surname}', " +
                $"address_id={this.address.id}";
        }

        public override string ToString()
        {
            return $"{name} {surname} - {address?.ToString()}";
        }

    }
}
