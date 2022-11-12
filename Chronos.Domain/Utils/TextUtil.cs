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
    }
}
