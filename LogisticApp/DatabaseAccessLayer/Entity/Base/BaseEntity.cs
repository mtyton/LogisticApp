﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.DatabaseAccessLayer.Entity.Base
{
    public class BaseEntity : BaseEntityInterface
    {
        public long ID 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        virtual public bool checkIfRecordComplete()
        {
            return true;
        }

        public string ToInsert()
        {
            if (!this.checkIfRecordComplete())
            {
                throw new ArgumentNullException(
                    $"Instance of {this.GetType().Name} " +
                    $"can't be inserted, it is incomplete"
                );
            }
            return $"";
        }

        public string ToUpdate()
        {
            if (!this.checkIfRecordComplete())
            {
                throw new ArgumentNullException(
                    $"Instance of {this.GetType().Name} " +
                    $"can't be updated, it is incomplete"
                );
            }
            return $"";
        }
    }
}
