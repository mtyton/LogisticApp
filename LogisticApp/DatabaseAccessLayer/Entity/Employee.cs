using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.DatabaseAccessLayer.Entity
{
    class Employee
    {
        int employeeID { get; set; }
        string name { get; set; }
        string surname {get; set;}
        int age { get; set; }
        // employee salary from Salary table
        float salary { get; set; }
        // array of employee abilities, can be filtered
        // during ticket creation
        Ability[] abilities { get; set; }
        // TODO create constructor which will provide data
    }
}
