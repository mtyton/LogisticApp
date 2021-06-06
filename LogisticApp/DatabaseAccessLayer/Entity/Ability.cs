using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.DatabaseAccessLayer.Entity
{
    class Ability
    {
        string name;

        Ability()
        {

        }

        Ability(string n)
        {
            name = n;
        }


        public override string ToString()
        {
            return name;
        }

    }
}
