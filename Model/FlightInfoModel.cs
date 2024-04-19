using Newtonsoft.Json;

namespace Assignment.Model
{
    internal class FlightInfoModel
    {

        public int Flight {  get; set; }
        public int Day { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("destination")]
        public string Destination { get; set; }
    }

    internal class FlightInfo
    {
        public IList<DayModel>? DayModel { get; set; }
        
    }

    internal class DayModel
    {
        public int Day { get; set; }
        public IList<FlightInfoModel> FlightInfoModel { get; set; }
    }
}
