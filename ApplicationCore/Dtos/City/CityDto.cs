using BusinessLayer.Dtos.PointOfInterestEntity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Dtos.CityEntity
{
    public class CityDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You should provide a name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "You should provide a description")]
        [MaxLength(200)]
        public string Description { get; set; }
        public int NumberOfPointOfInterest
        {
            get => PointsOfInterest.Count;
        }

        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; }
        = new List<PointOfInterestDto>();
    }
}
