using System.ComponentModel.DataAnnotations;

namespace SimulationDay5.Models
{
    public class Position:BaseEntity
    {
        [MinLength(2),MaxLength(20)]
        public string Name {  get; set; }
        public IEnumerable<Person> Persons { get; set; }
    }
}
