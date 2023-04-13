using Andrii_Mykyta_Lab3_ToP.Models;
using Microsoft.AspNetCore.Mvc;

namespace Andrii_Mykyta_Lab3_ToP.Controllers
{
    public class Task1Controller : Controller
    {
        public IActionResult Index()
        {
            var clubsCount = DataProvider.ClubsCount;
            var students = DataProvider.StudentsDictionary;
            var top4 = clubsCount.OrderByDescending(x => x.Value).Take(4);
            List<(Student, int)> result = new List<(Student, int)>();
            foreach (var student in top4)
            {
                result.Add((students[student.Key], student.Value));
            }
            return View(result);
        }
    }
}
