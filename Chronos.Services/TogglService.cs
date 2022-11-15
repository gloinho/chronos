using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Interfaces.Services;
using System.Text.Json;

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
            var httpCliente = _httpClientFactory.CreateClient();

            //httpCliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue();

            var response = await httpCliente.GetAsync($"https://api.track.toggl.com/reports/api/v2/details?workspace_id={request.Id}&since={request.DataInicio}&until={request.DataFim}&user_agent=api_test");

            var content = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<TogglDetailedResponse>(content);

            return result;
        }
    }
}
