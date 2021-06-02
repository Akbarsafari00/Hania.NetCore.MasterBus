namespace Hania.NetCore.MasterBus.Core.Extensions
{
    public static class StringExtensions
    {
        public static byte[] ToByteArray(this string input)
        {
            return System.Text.Encoding.UTF8.GetBytes(input);
        }
    }
}