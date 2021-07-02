﻿using LogisticApp.DatabaseAccessLayer.Entity.Base;
using System.Data;

namespace LogisticApp.DatabaseAccessLayer.Entity
{
    public class Skillset : BaseEntity
    {

        string name;
        private long id;

        public Skillset(IDataReader reader)
        {
            this.id = long.Parse(reader["id"].ToString());
            name = reader["skill_name"].ToString();
        }

        public Skillset(string n)
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
            return $"(skill_name) VALUES ({this.name})";
        }

        public string ToUpdate()
        {
            base.ToUpdate();
            return $"skill_name={this.name}";
        }
    }
}
