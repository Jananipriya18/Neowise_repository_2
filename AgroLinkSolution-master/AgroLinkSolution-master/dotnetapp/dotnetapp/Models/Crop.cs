using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Crop
    {
        [Key]
        public int CropId { get; set; }
        [Required(ErrorMessage = "CropName is Required")]
        public string CropName { get; set; }
        [Required(ErrorMessage = "CropType is Required")]

        public string CropType { get; set; }
        [Required(ErrorMessage = "Description is Required")]

        public string Description { get; set; }
        [Required(ErrorMessage = "PlantingDatea is Required")]

        public DateTime PlantingDate { get; set; }
    }
}
