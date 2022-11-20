namespace Chronos.Testes.Fakers
{
    public static class ProjetoFaker
    {
        private static Faker fake = new Faker();

        public static Projeto GetProjeto()
        {
            return new Projeto()
            {
                DataFim = fake.Date.Recent(),
                DataInicio = fake.Date.Recent(),
                DataAlteracao = fake.Date.Recent(),
                DataInclusao = fake.Date.Recent(),
                Nome = fake.Name.JobTitle(),
            };
        }

        public static List<Projeto> GetProjetos()
        {
            var lista = new List<Projeto>();
            for (int i = 0; i < 5; i++)
            {
                lista.Add(GetProjeto());
            }
            return lista;
        }
    }
}
