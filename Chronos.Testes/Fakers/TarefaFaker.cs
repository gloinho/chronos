namespace Chronos.Testes.Fakers
{
    public static class TarefaFaker
    {
        private static Faker faker = new Faker();

        public static Tarefa GetTarefaAntiga(int id)
        {
            return new Tarefa
            {
                Usuario_ProjetoId = id,
                Descricao = faker.Random.String(),
                DataInicial = faker.Date.Past(),
                DataFinal = null,
                DataAlteracao = faker.Date.Past(),
                DataInclusao = faker.Date.Past(1),
            };
        }

        public static Tarefa GetTarefa(int id)
        {
            return new Tarefa
            {
                Usuario_ProjetoId = id,
                Descricao = faker.Random.String(),
                DataInicial = faker.Date.Past(),
                DataFinal = null,
                DataAlteracao = faker.Date.Past(),
                DataInclusao = DateTime.Today,
            };
        }

        public static Tarefa GetTarefaAddTresDias(int id)
        {
            return new Tarefa
            {
                Usuario_ProjetoId = id,
                Descricao = faker.Random.String(),
                DataInicial = faker.Date.Past(),
                DataFinal = null,
                DataAlteracao = faker.Date.Past(),
                DataInclusao = DateTime.Today.AddDays(3),
            };
        }
    }
}
