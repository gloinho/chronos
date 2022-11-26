using Chronos.Domain.Contracts.Request;
using Chronos.Domain.Contracts.Response;
using Chronos.Domain.Entities;
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
        private readonly IProjetoRepository _projetoRepository;
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUsuario_ProjetoRepository _usuarioProjetoRepository;
        public TogglService(TogglSettings togglSettings, IUsuarioRepository usuarioRepository, IProjetoRepository projetoRepository,
            ITarefaRepository tarefaRepository, IUsuario_ProjetoRepository usuarioProjetoRepository)
        {
            _togglSettings = togglSettings;
            _usuarioRepository = usuarioRepository;
            _projetoRepository = projetoRepository;
            _tarefaRepository = tarefaRepository;
            _usuarioProjetoRepository = usuarioProjetoRepository;
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
            var usuarioTogg = toggl.data.FirstOrDefault()?.uid;
            var usuarioChronos = await _usuarioRepository.ObterAsync(u => u.TogglId == usuarioTogg.ToString());

            if (usuarioChronos == null)
            {
                throw new BaseException(StatusException.NaoEncontrado, "Usuario não cadastrado, favor realizar cadastro com mesmo nome cadastrado no Toggl");
            }

            foreach (var dados in toggl.data)
            {
                var find = await _projetoRepository.ObterAsync(p => p.Nome == dados.project);

                var usuarioProjeto = new Usuario_Projeto();

                if (find == null)
                {
                    Projeto novoProjeto = new();
                    await _projetoRepository.CadastrarAsync(novoProjeto);

                    usuarioProjeto.ProjetoId = novoProjeto.Id;
                    usuarioProjeto.UsuarioId = usuarioChronos.Id;
                }
                else
                {
                    usuarioProjeto.ProjetoId = find.Id;
                    usuarioProjeto.UsuarioId = usuarioChronos.Id;
                }

                await _usuarioProjetoRepository.CadastrarAsync(usuarioProjeto);

                var tarefa = new Tarefa
                {
                   Usuario_ProjetoId = usuarioProjeto.Id,
                    Descricao = dados.description,
                    DataInicial = dados.start,
                    DataFinal = dados.end
                };

                await _tarefaRepository.CadastrarAsync(tarefa);
            }
        }
    }
}
