using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinWeatherApp
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string zipCode)
        {
            string queryString =
              "https://query.yahooapis.com/v1/public/yql?q=select+*+from+weather.forecast+where+woeid+in+(select+woeid+from+geo.places(1)+where+text=" +
               "\""+zipCode+"\""+ ")and u='c'" + "&format=json";

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            dynamic weatherOverview = results["query"]["results"]["channel"];

            if ((string)weatherOverview["description"] != "Yahoo! Weather Error")
            {
                Weather weather = new Weather();
                dynamic location = weatherOverview["location"];
                weather.Title = (string) location["city"]+", "+location["country"];

                dynamic wind = weatherOverview["wind"];
                weather.Temperature = ((int)wind["chill"]-32)/1.8 + " C";
                weather.Wind = (string)wind["speed"] + " km/t";

                dynamic atmosphere = weatherOverview["atmosphere"];
                weather.Humidity = (string)atmosphere["humidity"] + " %";
                weather.Visibility = (string)atmosphere["visibility"] + " kilometer";

                dynamic astronomy = weatherOverview["astronomy"];
                weather.Sunrise = (string)astronomy["sunrise"];
                weather.Sunset = (string)astronomy["sunset"];

                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}
