using System.Numerics;

namespace Andrii_Mykyta_Lab3_ToP.Models
{
    public class Student : ICSVParser<Student>
    {
        public int Id { get; set; }

        public string? Surname { get; set; }

        public int Group{ get; set; }

        public int Grade { get; set; }

        public static Student Parse(string? line, string separator = ";")
        {
            var words = line?.Split(new[] { separator }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (words is null || words.Length != 4 || !int.TryParse(words[0], out var id) || !int.TryParse(words[2], out var group) || !int.TryParse(words[3], out var grade))
                throw new FormatException("String cannot be parsed to the instance of Student type");
            return new Student { Id = id, Surname = words[1], Group = group, Grade = grade };
        }
    }
}
