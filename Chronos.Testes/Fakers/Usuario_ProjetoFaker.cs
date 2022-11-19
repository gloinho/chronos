namespace Chronos.Testes.Fakers
{
    public static class Usuario_ProjetoFaker
    {
        public static Usuario_Projeto GetRelacao(Projeto projeto, Usuario usuario)
        {
            return new Usuario_Projeto()
            {
                Usuario = usuario,
                UsuarioId = usuario.Id,
                Projeto = projeto,
                ProjetoId = projeto.Id
            };
        }
    }
}
