using Newtonsoft.Json;

namespace Lowcode.Domain.Shared
{
    public class JsonMap
    {
        public string lat { set; get; }
        public string lng { set; get; }
    }

    public static class MapLocationHelper
    {
        /// <summary>
        /// Validate map format data
        /// </summary>
        /// <param name="mapLocation"></param>
        /// <returns></returns>
        public static bool IsValidMap(string mapLocation)
        {
            dynamic json = JsonConvert.DeserializeObject(mapLocation);
            if (json.lat != null && json.lng != null)
                return true;
            return false;
        }

        public static JsonMap Parse(string mapLocation)
        {
            dynamic json = JsonConvert.DeserializeObject(mapLocation);
            return new JsonMap() { lat = json.lat, lng = json.lng };
        }
    }
}