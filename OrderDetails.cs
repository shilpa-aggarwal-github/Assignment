using Assignment.Interfaces;
using Assignment.Model;
using static Assignment.Model.OrderDestinationModel;

namespace Assignment
{
    internal class OrderDetails: IOrderDetails
    {
        private readonly IOrderProcessor _orderProcessor;
        public OrderDetails(IOrderProcessor orderProcessor)
        {
            _orderProcessor = orderProcessor;
        }
       
        public void GetOrderDetails(int boxSize, FlightInfo flightInfo)
        {
            try
            {
                //read order data json
                var orderData = _orderProcessor.ReadOrderDataJson();
                // grouping the order deatils by destination
                var groupByDestination = _orderProcessor.GroupOrdersByDestination(orderData);

                foreach (var destGroup in groupByDestination)
                {
                    // checking destination is scheduled or not
                    if (!_orderProcessor.IsValidDestination(destGroup.Key))
                    {
                        _orderProcessor.HandleInvalidDestination(destGroup.Value);
                    }
                    else
                    {
                        // showing orders scheduled with flight and day
                        _orderProcessor.ShowScheduledOrders(flightInfo.DayModel, destGroup.Value, destGroup.Key, boxSize);
                        // proving information of remaining orders not scheduled
                        _orderProcessor.HandleRemainingOrders(destGroup.Value, flightInfo.DayModel.Count,  boxSize);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Order file not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting order details: {ex.Message}");
            }
        }

    }
}