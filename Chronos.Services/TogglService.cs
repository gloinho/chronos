using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Chronos.Services
{
    public class TogglService : ITogglService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TogglService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<TogglDetailedResponse> ObterHorasToggl(TogglDetailedRequest request)
        {
            string user = "c243dca599f88afd2ed63eeb13f155b0" + ":api_token";
            user = Convert.ToBase64String(Encoding.ASCII.GetBytes(user));
            var inicio = request.DataInicio.Date;
            var fim = request.DataFim.Date;
            string uri = $"https://api.track.toggl.com/reports/api/v2/details?workspace_id={request.Id}&since=2022-11-01&until=2022-11-15&user_agent=api_test";

            HttpClient httpClient = new HttpClient();
            HttpRequestMessage req = new HttpRequestMessage();

            req.RequestUri = new Uri(uri);
            req.Method = HttpMethod.Get;
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            req.Headers.TryAddWithoutValidation("Authorization", "Basic " + user);
            HttpResponseMessage response = await httpClient.SendAsync(req);
           
            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TogglDetailedResponse>(responseString);

            return result;
        }
    }
}
