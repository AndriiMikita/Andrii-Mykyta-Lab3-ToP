using Andrii_Mykyta_Lab3_ToP.Models;
using Microsoft.AspNetCore.Mvc;

namespace Andrii_Mykyta_Lab3_ToP.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            var students = DataProvider.Students;
            return View(students);
        }
    }
}
