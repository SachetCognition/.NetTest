namespace Equant.SAV2000.ComponentLibrary.MVC.Extensions
{
    using System.Text;

    public static class StringExtension
    {
        public static string AppendWithBuilder(this string str, params string[] args)
        {
            var sb = new StringBuilder(str);
            foreach (var arg in args)
            {
                sb.Append(arg);
            }
            return sb.ToString();
        }
    }
}
