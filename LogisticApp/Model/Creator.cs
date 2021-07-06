using LogisticApp.DatabaseAccessLayer.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Model
{
    class Creator
    {
        EntityFactory _factory = new EntityFactory();
        BaseEntity _record = null;

        public BaseEntity Record
        {
            get => _record;
            set => _record = value;
        }

        public void createOrUpdate (string entityName)
        {
            if(this._record != null)
            {
                this._record = DataAccessFacade.update(entityName, this._record);
            }
            else
            {
                this._record = DataAccessFacade.create(entityName, this._record);
            }

        }

        ~Creator()
        {
            _factory = null;
            _record = null;
        }

    }
}
