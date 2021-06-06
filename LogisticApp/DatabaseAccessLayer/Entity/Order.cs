using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticApp.DatabaseAccessLayer.Entity.Client;

namespace LogisticApp.DatabaseAccessLayer.Entity
{
    class Order
    {
        BaseClient client { get; set; }
        City city;
        Ability[] requiredAbilities;
        Employee employee;

        float predictedCost;
        // TODO add expected duration
        DateTime predictedDeadLine;
    }
}
