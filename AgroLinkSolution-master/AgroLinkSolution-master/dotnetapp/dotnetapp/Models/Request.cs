using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        [ForeignKey("UserId")]
        [Required(ErrorMessage ="UserId is Required")]
        public int UserId { get; set; }
        [ForeignKey("CropId")]
        [Required(ErrorMessage = "CropId is Required")]
        public int CropId {  get; set; }

        [ForeignKey("AgroChemicalId")]
        [Required(ErrorMessage = "AgroChemicalId is Required")]
        public int AgroChemicalId { get; set; }
        [Required(ErrorMessage = "Quantity is Required")]

        public int Quantity { get; set; }
        [Required(ErrorMessage = "RequestPurpose is Required")]

        public string RequestPurpose { get; set; }
        [Required(ErrorMessage = "Status is Required")]

        public string Status { get; set; }
        [Required(ErrorMessage = "RequestDate is Required")]

        public DateTime RequestDate { get; set; }

        public  AgroChemicals? AgroChemical { get; set;}
        public Crop? Crop { get; set; }

        public  User? User { get; set;}


    }
}
