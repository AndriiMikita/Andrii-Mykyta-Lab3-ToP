using Andrii_Mykyta_Lab3_ToP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Andrii_Mykyta_Lab3_ToP.Controllers
{
    public class Task2Controller : Controller
    {
        private static List<int> grades = new List<int>() { 1, 3 };
        private static List<SelectListItem> clubsNames = new List<SelectListItem>(); 

        public ActionResult Index(string? clubName = null)
        {
            var usedStudents = new HashSet<int>();
            var students = DataProvider.StudentsDictionary;
            var studentsInTask = new List<Student>();
            var studentsInClubs = DataProvider.StudentsInClub;
            if (clubName is null)
            {
                foreach (var club in studentsInClubs.Values)
                {
                    foreach (var studentId in club)
                    {
                        if (grades.Contains(students[studentId].Grade) && !usedStudents.Contains(studentId))
                        {
                            usedStudents.Add(studentId);
                            studentsInTask.Add(students[studentId]);
                        }
                    }
                }
            }
            else
            {
                foreach (var studentId in studentsInClubs[clubName])
                {
                    if (grades.Contains(students[studentId].Grade) && !usedStudents.Contains(studentId))
                    {
                        usedStudents.Add(studentId);
                        studentsInTask.Add(students[studentId]);
                    }
                }
            }

            clubsNames = DataProvider.StudentsInClub
                .Select(v => new SelectListItem { Value = v.Key, Text = v.Key })
                .ToList();
            clubsNames.Insert(0, new SelectListItem { Value = "-", Text = "-" });
            
            var triplet = new Triplet()
            {
                ClubsNames = clubsNames,
                Students = studentsInTask,
                ClubName = clubName,
            };

            return View(triplet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Triplet triplet)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("State is not valid");
                return RedirectToAction("Index");
            }
            if (triplet.ClubName == "-")
                triplet.ClubName = null;
            return Index(triplet.ClubName);
        }
    }
}
