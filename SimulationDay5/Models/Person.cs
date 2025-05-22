using System.ComponentModel.DataAnnotations;

namespace SimulationDay5.Models
{
    public class Person:BaseEntity
    {
        [MinLength(2),MaxLength(20)]
        public string FullName {  get; set; }
        public int PositionId {  get; set; }
        public Position Position { get; set; }
        public string ImageUrl {  get; set; }
    }
}
