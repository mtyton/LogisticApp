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
        UpdateOrCreateModel _creator = new UpdateOrCreateModel();

        public UpdateOrCreateModel Creator
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
        public virtual object[] serializeData()
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

        }

    }
}
