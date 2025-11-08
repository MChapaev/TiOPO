namespace LR4_White_Box_Testing.Extensions
{
    public static class StringExtensions
    {
        public static string MyTrim(this string str)
        {
            if (str == null) throw new ArgumentNullException(nameof(str));
            if (str.Length < 3) return string.Empty;
            char[] chars = new char[str.Length - 1];
            for (int i = 1; i < chars.Length; i++) chars[i] = str[i];
            return new string(chars);
        }
    }
}
