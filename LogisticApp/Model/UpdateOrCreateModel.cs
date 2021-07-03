using LogisticApp.DatabaseAccessLayer.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Model
{
    class UpdateOrCreateModel
    {
        EntityFactory _factory = new EntityFactory();
        BaseEntity _record = null;

        public BaseEntity Record
        {
            get => _record;
            set => _record = value;
        }

        public void createOrUpdate (string entityName, object[] entityValues)
        {
            if(this._record != null)
            {
                this.update(entityName);
            }
            else
            {
                this._record = _factory.createEntity(entityName, entityValues);
                this._record = DataAccessFacade.create(entityName, this._record);
            }

        }

        private void update(string entityName)
        {
            this._record = DataAccessFacade.update(entityName, this._record);
        }

        ~UpdateOrCreateModel()
        {
            _factory = null;
            _record = null;
        }

    }
}
