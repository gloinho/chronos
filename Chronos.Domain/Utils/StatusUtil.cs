using System.ComponentModel;
using System.Reflection;

namespace Chronos.Domain.Utils
{
    public static class StatusUtil
    {
        public static string Description(this Enum value)
        {
            return value.GetType()
                   .GetMember(value.ToString())
                   .First()
                   .GetCustomAttribute<DescriptionAttribute>()?
                   .Description ?? string.Empty;
        }

    }
}
