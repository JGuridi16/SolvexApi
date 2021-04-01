using System.ComponentModel.DataAnnotations;

namespace SolvexApi
{
    public class PointOfInterestDto
    {
        [Required(ErrorMessage = "You should provide an id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "You should provide a name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "You should provide a description")]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}