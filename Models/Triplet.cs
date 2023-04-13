using Microsoft.AspNetCore.Mvc.Rendering;

namespace Andrii_Mykyta_Lab3_ToP.Models
{
    public class Triplet
    {
        public List<SelectListItem>? ClubsNames { get; set; }
        public List<Student>? Students { get; set; }
        public string? ClubName { get; set; }
    }
}
