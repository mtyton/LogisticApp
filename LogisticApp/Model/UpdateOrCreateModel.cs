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
        }

        public void create (string entityName, object[] entityValues)
        {
            this._record = _factory.createEntity(entityName, entityValues);
            this._record = DataAccessFacade.create(entityName, this._record);
        }

        public void update()
        {

        }

        ~UpdateOrCreateModel()
        {
            _factory = null;
            _record = null;
        }

    }
}
