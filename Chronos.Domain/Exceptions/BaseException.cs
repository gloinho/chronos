using Chronos.Domain.Utils;

namespace Chronos.Domain.Exceptions
{
    public class BaseException : Exception
    {
        public StatusException Codigo { get;}
        public List<string> Mensagens { get;  }
        public BaseException(StatusException status, List<string> mensagens, Exception exception = null)
            :base(status.Description(), exception)
        {
            Codigo = status;
            Mensagens = mensagens;
        }
        public BaseException(StatusException status, string mensagens, Exception exception = null)
            : base(status.Description(), exception)
        {
            Codigo = status;
            Mensagens = new List<string>() { mensagens };
        }

    }
}
