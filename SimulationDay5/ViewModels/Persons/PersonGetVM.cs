using System.ComponentModel.DataAnnotations;

namespace SimulationDay5.ViewModels.Persons
{
    public class PersonGetVM
    {
        public int Id { get; set; } 
        [MinLength(2), MaxLength(20)]
        public string FullName { get; set; }
        public string PositionName { get; set; }
        public string ImageUrl { get; set; }
    }
}
