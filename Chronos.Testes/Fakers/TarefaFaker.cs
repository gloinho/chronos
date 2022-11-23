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
                DataInclusao = DateTime.Today,
            };
        }

        public static Tarefa GetTarefa()
        {
            return new Tarefa
            {
                Id = faker.Random.Int(),
                Usuario_ProjetoId = faker.Random.Int(),
                Descricao = faker.Random.String(),
                DataInicial = DateTime.Today,
                DataFinal = DateTime.Today.AddHours(1),
                DataInclusao = DateTime.Today,
            };
        }

        public static Tarefa GetTarefaAntiga()
        {
            return new Tarefa
            {
                Id = faker.Random.Int(),
                Usuario_ProjetoId = faker.Random.Int(),
                Descricao = faker.Random.String(),
                DataInicial = DateTime.Today,
                DataFinal = DateTime.Today.AddHours(1),
                DataInclusao = faker.Date.Past(1),
            };
        }

        public static List<Tarefa> GetTarefas()
        {
            var lista = new List<Tarefa>();
            for (int i = 0; i < 5; i++)
            {
                lista.Add(GetTarefa());
            }
            return lista;
        }
    }
}
