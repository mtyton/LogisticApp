using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticApp.Models
{
    class Client
    {
        string name { get; set; }
        //rethink should we create a class for
        //(typ działalności)
        string companyType { get; set; }
        string city { get; set; }
        string postalCode { get; set; }
        string address { get; set; }
        bool isCompany { get; set; }
    }
}
