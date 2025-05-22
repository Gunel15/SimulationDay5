using System.ComponentModel.DataAnnotations;

namespace SimulationDay5.ViewModels.Positions
{
    public class PositionUpdateVM
    {
        public int Id { get; set; }
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }
    }
}
