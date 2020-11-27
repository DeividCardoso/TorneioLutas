using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace TorneioLutas.Service.Helpers
{
    public static class HttpHelper
    {

        public static async Task<T> GetAsync<T>(string url) where T : class
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResult = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<T>(strResult);
                }

                return null;
            }
        }
    }
}
