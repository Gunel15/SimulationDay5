using System.ComponentModel.DataAnnotations;

namespace SimulationDay5.ViewModels.Positions
{
    public class PositionCreateVM
    {
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }
    }
}
