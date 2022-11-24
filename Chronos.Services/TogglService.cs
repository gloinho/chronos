using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;
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
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProjetoService _projetoService;
        private readonly ITarefaService _tarefaService;
        public TogglService(TogglSettings togglSettings, IUsuarioRepository usuarioRepository, IProjetoService projetoService, ITarefaService tarefaService)
        {
            _togglSettings = togglSettings;
            _usuarioRepository = usuarioRepository;
            _projetoService = projetoService;
            _tarefaService = tarefaService;
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

            await CadastrarHorasToggl(result);

            return result;
        }

        public async Task CadastrarHorasToggl(TogglDetailedResponse toggl)
        {
            var usuarioTogg = toggl.data.FirstOrDefault()?.user;
            var usuarioChronos = await _usuarioRepository.ObterAsync(u => u.Nome == usuarioTogg);

            if(usuarioChronos == null)
            {
                throw new BaseException(StatusException.NaoEncontrado, "Usuario não cadastrado, favor realizar cadastro");
            }

            foreach(var dados in toggl.data)
            {
                var tarefa = new TarefaRequest
                {
                    ProjetoId = 1,// alterar
                    Descricao = dados.description,
                    DataInicial = dados.start,
                    DataFinal = dados.end
                };

                await _tarefaService.CadastrarAsync(tarefa);

            }
        }
    }
}
