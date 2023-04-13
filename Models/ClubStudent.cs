using System.Numerics;

namespace Andrii_Mykyta_Lab3_ToP.Models
{
    public class ClubStudent : ICSVParser<ClubStudent>
    {
        public string? ClubName { get; set; }
        public int StudentId { get; set; }

        public static ClubStudent Parse(string line, string separator = ";")
        {
            var words = line.Split(new[] { separator }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (words.Length != 2 || !int.TryParse(words[1], out var studentId))
                throw new FormatException("String cannot be parsed to the instance of CircleStudent type");
            return new ClubStudent { ClubName = words[0], StudentId = studentId };
        }
    }
}
