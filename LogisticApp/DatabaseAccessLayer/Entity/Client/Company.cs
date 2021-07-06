using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.Entity.Base;


namespace LogisticApp.DatabaseAccessLayer.Entity.Client
{
    public class Company : BaseEntity
    {
        public string companyName { get; set; }
        public string taxNumber { get; set; }
        public Address address { get; set; }

        public long AddrId{ get; set; }

        public Company(IDataReader companyReader)
        {
            this.id = long.Parse(companyReader["id"].ToString());
            this.companyName = companyReader["name"].ToString();
            this.taxNumber = companyReader["tax_number"].ToString();
            this.AddrId = long.Parse(companyReader["address_id"].ToString());
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
                $"VALUES (NULL, '{this.companyName}', '{this.taxNumber}', " +
                $"{this.address.id});";
        }

        public string ToUpdate()
        {
            base.ToUpdate();
            return $"name='{this.companyName}', tax_number='{this.taxNumber}'," +
                $" address_id={this.address.id}";
        }

        public override string ToString()
        {
            return $"{companyName} - {address?.ToString()}";
        }

    }
}
