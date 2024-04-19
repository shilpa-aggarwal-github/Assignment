using Assignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assignment.Model.OrderDestinationModel;

namespace Assignment.Interfaces
{
    internal interface IOrderProcessor
    {
        Dictionary<string, Order> ReadOrderDataJson();
        Dictionary<string, List<string>> GroupOrdersByDestination(Dictionary<string, Order> orderData);
        bool IsValidDestination(string destination);
        void HandleInvalidDestination(List<string> orders);
        void ShowScheduledOrders(IList<DayModel> dayModels, List<string> orders, string dest, int boxSize);
        void HandleRemainingOrders(List<string> orders,int days, int boxSize);

    }
}
