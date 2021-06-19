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
    class Job : BaseEntityInterface
    {
        private long id;
        private string title;
        private Person person;
        private Company company;
        private string description;
        private Employee assignedEmployee;
        private int predictedTime;
        private int predictedCost;

        public long ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Job(IDataReader reader)
        {

        }

        public string ToInsert()
        {
            throw new NotImplementedException();
        }

        public string ToUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
