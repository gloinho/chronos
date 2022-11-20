using Chronos.Domain.Entities;
using Chronos.Domain.Exceptions;
using Chronos.Domain.Interfaces.Repository;

namespace Chronos.Domain.Shared
{
    public class Verificador<T> where T : BaseEntity
    {
        private readonly IBaseRepository<T> _repository;

        public Verificador(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> Id(int id)
        {
            var entity = await _repository.ObterPorIdAsync(id);
            if (entity == null)
            {
                throw new BaseException(StatusException.NaoEncontrado, $"Id {id} não cadastrado.");
            }
            return entity;
        }
    }
}
