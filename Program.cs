
using Assignment;
using Assignment.Interfaces;
using Assignment.Model;
using Assignment.Utility;
using Microsoft.Extensions.DependencyInjection;


// Create a service collection
var services = new ServiceCollection();

// Configure dependency injection
string filePath = @"C:\Users\Sachin\source\repos\Assignment\wwwroot\json\coding-assigment-orders.json";
services.AddSingleton<IOrderProcessor>(new OrderProcessor(filePath));
services.AddTransient<IOrderDetails,OrderDetails>();
services.AddTransient<IFlightDetails, FlightDetails>();
// Build the service provider
var serviceProvider = services.BuildServiceProvider();

// Resolve  instance from the service provider
var orderDetails = serviceProvider.GetRequiredService<IOrderDetails>();
var flightDetails = serviceProvider.GetRequiredService<IFlightDetails>();


// order size for one flight
int boxSize = 20;
string flightInfoJson = File.ReadAllText(@"C:\Users\Sachin\source\repos\Assignment\wwwroot\Json\FlightInfoJson.json");
if (!string.IsNullOrEmpty(flightInfoJson))
{
    FlightInfo flightInfo = null;
    try
    {
        // deserialization of FlightInfo json file
        flightInfo = Utility.DeserializeJsonMethod<FlightInfo>(flightInfoJson);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while Desearilize flightInfoJson json: {ex.Message}");
    }
  
    if (flightInfo != null)
    {
        // user Story-1
       
        flightDetails.DisplayFlightInfo(flightInfo);
        // user Story-2
       
        orderDetails.GetOrderDetails(boxSize, flightInfo);
    }
   
}
    Console.ReadLine();




