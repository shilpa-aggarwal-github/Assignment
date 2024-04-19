using Assignment.Interfaces;
using Assignment.Model;

namespace Assignment
{
    internal class FlightDetails:IFlightDetails
    {
        /// <summary>
        /// This method is used to show the flight deatils
        /// </summary>
        /// <param name="flightInfo"></param>
        public void DisplayFlightInfo(FlightInfo flightInfo)
        {
            try
            {
                if (flightInfo!=null)
                {
                    flightInfo.DayModel?.ToList().ForEach(d =>
                    {
                        d.FlightInfoModel?.ToList().ForEach(f =>
                        {
                            Console.WriteLine($"Flight: {f.Flight}, departure: {f.Source}, arrival: {f.Destination}, day: {d.Day}");
                        });
                    });
                   
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while displaying flight information: {ex.Message}");
            }
        }
    }
}
