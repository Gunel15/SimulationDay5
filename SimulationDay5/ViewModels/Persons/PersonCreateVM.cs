using System.ComponentModel.DataAnnotations;

namespace SimulationDay5.ViewModels.Persons
{
    public class PersonCreateVM
    {
        [MinLength(2), MaxLength(20)]
        public string FullName { get; set; }
        public int PositionId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
