using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Services;
using Chronos.Domain.Settings;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Chronos.Services
{
    public class TogglService : ITogglService
    {
        private readonly TogglSettings _togglSettings;
        public TogglService(TogglSettings togglSettings)
        {            
            _togglSettings = togglSettings;
        }
        public async Task<TogglDetailedResponse> ObterHorasToggl(TogglDetailedRequest request)
        {
            string user = $"{request.TokenToggl}:api_token";
            user = Convert.ToBase64String(Encoding.ASCII.GetBytes(user));

            var inicio = request.DataInicio.ToString("yyyy-MM-dd");
            var fim = request.DataFim.ToString("yyyy-MM-dd");

            string url = _togglSettings.BaseUrl + _togglSettings.Detailed + _togglSettings.WorkSpace + request.Id + _togglSettings.Since + 
                inicio + _togglSettings.Until + fim + _togglSettings.FinalUrl;

            var httpClient = new HttpClient();
            var req = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            req.Headers.TryAddWithoutValidation("Authorization", "Basic " + user);
            HttpResponseMessage response = await httpClient.SendAsync(req);

            if (!response.IsSuccessStatusCode)
            {
                throw new BaseException(StatusException.Erro, "Erro na requisição");
            }


            var responseString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TogglDetailedResponse>(responseString);

            return result;
        }
    }
}
