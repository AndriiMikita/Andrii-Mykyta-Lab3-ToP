namespace Andrii_Mykyta_Lab3_ToP.Models
{
    public interface ICSVParser<T>
    {
        public static abstract T Parse(string line, string separator = ";");
    }
}
