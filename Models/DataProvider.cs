using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Numerics;

namespace Andrii_Mykyta_Lab3_ToP.Models
{
    public class DataProvider
    {
        private static Dictionary<int, Student>? _studentsDic;
        private static Dictionary<int, int>? _clubsCount;
        private static List<ClubStudent>? _clubs;
        private static List<Student>? _students;
        private static Dictionary<string, HashSet<int>>? _studInClub;

        private const string DefaultDataDir = ".\\App_Data";
        private const string StudentsFileName = "Students.csv";
        private const string ClubsFileName = "Clubs.csv";
        private static object _lock = new();

        public static List<ClubStudent> Clubs
        {
            get
            {
                if (_clubs is null)
                {
                    lock (_lock)
                    {
                        _clubs ??= ReadData<ClubStudent>(ClubsFileName);
                    }
                }

                return _clubs;
            }
        }

        public static List<Student> Students
        {
            get
            {
                if (_students is null)
                {
                    lock (_lock)
                    {
                        _students ??= ReadData<Student>(StudentsFileName);
                    }
                }

                return _students;
            }
        }

        public static Dictionary<int, Student> StudentsDictionary =>
    _studentsDic ??= Students.ToDictionary(stud => stud.Id, stud => stud);

        public static Dictionary<int, int> ClubsCount
        {
            get
            {
                var temp = new Dictionary<int, int>();
                foreach (var club in Clubs) 
                {
                    if (temp.TryGetValue(club.StudentId, out _))
                    {
                        temp[club.StudentId]++;
                    }
                    else
                    {
                        temp.Add(club.StudentId, 1);
                    }
                }
                return _clubsCount ??= temp;
            }
        }

        public static Dictionary <string, HashSet<int>> StudentsInClub
        {
            get
            {
                var temp = new Dictionary<string, HashSet<int>>();
                foreach (var club in Clubs)
                {
                    if (temp.TryGetValue(club.ClubName, out _))
                    {
                        temp[club.ClubName].Add(club.StudentId);
                    }
                    else
                    {
                        temp.Add(club.ClubName, new HashSet<int> { club.StudentId });
                    }
                }
                return _studInClub ??= temp;
            }
        }

        public static List<T> ReadData<T>(string fileName, string? dataDir = null, string separator = ";")
        where T : ICSVParser<T>
        {
            var items = new List<T>();
            dataDir ??= DefaultDataDir;

            int lineNumber = 0;
            var fullName = Path.Combine(dataDir, fileName);
            Trace.WriteLine($"{DateTime.Now:HH:mm:ss}: {fullName} load started");
            try
            {
                foreach (var line in File.ReadAllLines(fullName))
                {
                    lineNumber++;
                    try
                    {
                        var item = T.Parse(line, separator);
                        items.Add(item);
                    }
                    catch (Exception)
                    {
                        Trace.WriteLine($"{fullName}: inconsistent data in line #{lineNumber}");
                    }
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine($"{fullName}: exception {e.Message}");
            }
            finally
            {
                Trace.WriteLine($"{DateTime.Now:HH:mm:ss}: {fullName} load finished");
            }

            return items;
        }
    }
}
