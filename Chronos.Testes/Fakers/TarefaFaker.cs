namespace Chronos.Testes.Fakers
{
    public static class TarefaFaker
    {
        private static Faker faker = new Faker();

        public static Tarefa GetTarefaDeHoje(int id)
        {
            return new Tarefa
            {
                Usuario_ProjetoId = id,
                Descricao = faker.Random.String(),
                DataInicial = DateTime.Today,
                DataFinal = DateTime.Today,
                DataAlteracao = faker.Date.Past(),
                DataInclusao = DateTime.Today,
            };
        }

        public static Tarefa GetTarefaDeAmanha(int id)
        {
            return new Tarefa
            {
                Usuario_ProjetoId = id,
                Descricao = faker.Random.String(),
                DataInicial = DateTime.Today.AddDays(1),
                DataFinal = DateTime.Today.AddDays(1),
                DataAlteracao = faker.Date.Past(),
                DataInclusao = DateTime.Today,
            };
        }
    }
}
