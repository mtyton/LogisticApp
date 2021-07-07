using LogisticApp.DatabaseAccessLayer.Entity.Base;
using LogisticApp.Model;
using LogisticApp.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.ViewModel.Forms
{
    class BaseFormViewModel : BaseViewModel
    {
        Creator _creator = new Creator();

        public Creator Creator
        {
            get => _creator;
            set
            {
                _creator = value;
                onPropertyChanged(nameof(Creator));
            }
        }


        public virtual void createRecord()
        {
            throw new NotImplementedException("Implement createRecord method for each form!!");
        }

        public virtual void updateRecord()
        {
            throw new NotImplementedException("Implement updateRecord method for each form!!");
        }

        // load data, takes serialized data and write this to form
        public virtual void loadData(BaseEntity entity)
        {
            throw new NotImplementedException("Implement loadData method for each form!!");
        }

        public virtual void save()
        {
            throw new NotImplementedException("Implement save method for each form!!");
        }

        public virtual bool canSave()
        {
            throw new NotImplementedException("Make sure each form has canSave method!!");
        }

    }
}
