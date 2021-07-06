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


        // serializeData, prepares data to be inserted into database
        // This method transfers ViewModel variables into an array which will be passed into factory
        public virtual void updateRecord()
        {
            throw new NotImplementedException("Implement serializeData method for each form!!");
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

    }
}
