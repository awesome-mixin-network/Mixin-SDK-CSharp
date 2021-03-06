#region

using System.Net.Http;
using System.Text;

#endregion

namespace Mixin.Network
{
    public partial class User
    {
        private string sendGetRequest(string uri, string token = "")
        {
            if (string.IsNullOrEmpty(token))
                token = signAuthToken("GET", uri);
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                return httpClient.GetStringAsync(baseUrl + uri).Result;
            }
        }

        private string sendPostRequest(string uri, string body, string token = "")
        {
            if (string.IsNullOrEmpty(token))
                token = signAuthToken("POST", uri, body);
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var content = new StringContent(body, Encoding.UTF8, "application/json");
                return httpClient.PostAsync(baseUrl + uri, content).Result.Content.ReadAsStringAsync().Result;
            }
        }
    }
}