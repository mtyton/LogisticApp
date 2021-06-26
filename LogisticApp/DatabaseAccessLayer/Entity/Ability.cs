using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using LogisticApp.DatabaseAccessLayer.Entity.Base;

namespace LogisticApp.DatabaseAccessLayer.Entity
{
    public class Ability : BaseEntity
    {

        string name;
        private long id;

        public Ability(IDataReader reader)
        {
            this.id = long.Parse(reader["id"].ToString());
            name = reader["ability_name"].ToString();
        }

        public Ability(string n)
        {
            name = n;
        }

        public long ID
        {
            get => id;
            set => id = value;
        }

        public override string ToString()
        {
            return name;
        }

        public override bool checkIfRecordComplete()
        {
            return this.name != null;
        }

        public string ToInsert()
        {
            base.ToInsert();
            return $"(ability_name) VALUES ({this.name})";
        }

        public string ToUpdate()
        {
            base.ToUpdate();
            return $"ability_name={this.name}";
        }
    }
}
