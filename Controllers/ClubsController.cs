using Andrii_Mykyta_Lab3_ToP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Andrii_Mykyta_Lab3_ToP.Controllers
{
    public class ClubsController : Controller
    {

        public IActionResult Index()
        {
            var clubs = DataProvider.Clubs;
            var students = DataProvider.StudentsDictionary;
            List<(string, string)> clubStudents = new List<(string, string)>();
            foreach (var club in clubs)
                clubStudents.Add((club.ClubName, students[club.StudentId].Surname));
            return View(clubStudents);
        }
    }
}
