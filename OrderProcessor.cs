using Assignment.Interfaces;
using Assignment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assignment.Model.OrderDestinationModel;

namespace Assignment
{
    enum FlightDestination
    {
        YYZ,
        YYC,
        YVR
    }
    internal class OrderProcessor : IOrderProcessor
    {
        private readonly string _filePath;

        public OrderProcessor(string filePath)
        {
            _filePath = filePath;
        }
        /// <summary>
        /// Read file from filepath
        /// Deserialize json file into disctionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Order> ReadOrderDataJson()
        {
            string orderFile = File.ReadAllText(_filePath);
            return Utility.Utility.DeserializeJsonMethod<Dictionary<string, Order>>(orderFile);
        }
        /// <summary>
        /// organize order data group by destination
        /// </summary>
        /// <param name="orderData"></param>
        /// <returns></returns>
        public Dictionary<string, List<string>> GroupOrdersByDestination(Dictionary<string, Order> orderData)
        {
            return orderData.GroupBy(val => val.Value.Destination)
                           .ToDictionary(g => g.Key, g => g.Select(p => p.Key).ToList());
        }
        /// <summary>
        /// Checking destination is valid or not
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool IsValidDestination(string destination)
        {
            return Enum.IsDefined(typeof(FlightDestination), destination);
        }
        /// <summary>
        /// showing data of not scheduled orders
        /// </summary>
        /// <param name="orders"></param>
        public void HandleInvalidDestination(List<string> orders)
        {
            orders.ForEach(o => Console.WriteLine($"Order: {o}, FlightNumber: Not scheduled"));
        }
        /// <summary>
        /// providing indo of scheduled orders
        /// </summary>
        /// <param name="dayModels"></param>
        /// <param name="orders"></param>
        /// <param name="dest"></param>
        /// <param name="boxSize"></param>
        public void ShowScheduledOrders(IList<DayModel> dayModels, List<string> orders, string dest, int boxSize)
        {
            dayModels?.ToList().ForEach(item =>
            {
                var orderIds = orders.Skip((item.Day - 1) * boxSize).Take(boxSize).ToList();
                FlightInfoModel flightInfoModel = item.FlightInfoModel.First(f => f.Destination == dest);
                if (orderIds.Count > 0)
                {
                    orderIds.ForEach(o =>
                    {
                        Console.WriteLine($"Order: {o}, Flight: {flightInfoModel.Flight}, Departure: {flightInfoModel.Source}, Arrival: {flightInfoModel.Destination}, Day: {item.Day}");
                    });
                }
            });
        }
        /// <summary>
        /// proving info of remaining orders
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="days"></param>
        /// <param name="boxSize"></param>
        public void HandleRemainingOrders(List<string> orders, int days, int boxSize)
        {
            var remainingOrders = orders.Skip((days) * boxSize).Take(boxSize).ToList();
            if (remainingOrders?.Count > 0)
            {
                HandleInvalidDestination(remainingOrders);
            }
        }
    }
}
