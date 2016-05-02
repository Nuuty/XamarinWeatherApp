using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XamarinWeatherApp
{
    public class DataService
    {
        public static async Task<dynamic> getDataFromService(string queryString)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(queryString);

            dynamic data = null;
            if (response != null)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }

            return data;
        }
    }
}