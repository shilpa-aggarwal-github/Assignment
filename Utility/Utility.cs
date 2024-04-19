using Newtonsoft.Json;

namespace Assignment.Utility
{
    internal class Utility
    {
        public static T DeserializeJsonMethod<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json)!;           
        }
    }
}
