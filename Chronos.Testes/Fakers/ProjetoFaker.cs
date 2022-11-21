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
                DataInclusao = fake.Date.Recent(),
                Nome = fake.Name.JobTitle(),
            };
        }
    }
}
