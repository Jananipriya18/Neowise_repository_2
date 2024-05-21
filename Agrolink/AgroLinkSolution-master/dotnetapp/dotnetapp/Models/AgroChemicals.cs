using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class AgroChemicals
    {

        [Key]
        public int AgroChemicalId { get; set; }
        [Required(ErrorMessage = "CropName is Required")]

        public string Name { get; set; }
        [Required(ErrorMessage = "Brand is Required")]

        public string Brand { get; set; }
        [Required(ErrorMessage = "Category is Required")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Suitable Crop is Required")]
        [ForeignKey("cropId")]
        public int SuitableCrop { get; set; }
        [Required(ErrorMessage = "Description is Required")]

        public string Description { get; set; }
        [Required(ErrorMessage = "Quantity is Required")]

        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is Required")]

        public float PricePerUnit { get; set; }
        [Required(ErrorMessage = "Image is Required")]
        public string Image { get; set; }

        public virtual Crop? Crop { get; set; }
    }
}
