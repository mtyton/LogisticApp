using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Models
{
    class Order
    {
        Client client { get; set; }
        City city;
        Ability[] requiredAbilities;
        Employee employee;

        float predictedCost;
        // TODO add expected duration
        DateTime predictedDeadLine;
    }
}
