namespace Bubbio.Domain.Validation
{
    public static class StringEx
    {
        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static string NullIfEmpty(this string text)
        {
            return text.IsEmpty() ? null : text;
        }
    }
}