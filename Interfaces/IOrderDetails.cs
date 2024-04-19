using Assignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Interfaces
{
    internal interface IOrderDetails
    {
        public void GetOrderDetails(int boxSize, FlightInfo flightInfo);
    }
}
