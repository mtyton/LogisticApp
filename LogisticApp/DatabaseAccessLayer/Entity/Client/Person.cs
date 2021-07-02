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
        private long id;
        private string name, surname;
        private Address address;

        public long AddrId { get; set; }
        public long ID
        {
            get => id;
            set => id = value;
        }

        public Address Addr
        {
            get => address;
            set => address = value;
        }


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
                this.Addr != null
                );
        }

        public string ToInsert()
        {
            base.ToInsert();
            return $"(id, name, surname, address_id) " +
                $"VALUES (NULL, {this.name}, {this.surname}, " +
                $"{this.address.ID});";
        }


        public string ToUpdate()
        {
            base.ToUpdate();
            return $"name={this.name}, surname={this.surname}, " +
                $"address_id={this.address.ID}";
        }

        public override string ToString()
        {
            return $"{name} {surname} - {address?.ToString()}";
        }

    }
}
