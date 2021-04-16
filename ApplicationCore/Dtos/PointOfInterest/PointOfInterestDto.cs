using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Dtos.PointOfInterestEntity
{
    public class PointOfInterestDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You should provide a name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "You should provide a description")]
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
