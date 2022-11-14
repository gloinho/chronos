using Chronos.Domain.Exceptions;

namespace Chronos.Domain.Utils
{
    public static class TextUtil
    {
        public static int? ToInt(this string value)
        {
            int result;
            if (int.TryParse(value, out result))
                return result;
            return null;
        }

        public static int GetStatusCode(this StatusException status)
        {
            switch (status)
            {
                case StatusException.Nenhum:
                    return 200;
                case StatusException.FormatoIncorreto:
                case StatusException.Obrigatoriedade:
                    return 400;
                case StatusException.NaoAutorizado:
                    return 401;
                case StatusException.AcessoProibido:
                    return 403;
                case StatusException.NaoEncontrado:
                    return 404;
                case StatusException.NaoProcessado:
                    return 422;
                default:
                    return 500;
            }
        }
    }
}
