using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CropController : ControllerBase
    {
        private readonly CropService _cropService;
        public CropController(CropService cropService)
        {
            _cropService = cropService;
        }

        // getAllCrop
        [HttpGet("GetAllCrop")]
        public async Task<ActionResult<IEnumerable<Crop>>> GetAllCrop()
        {
            var crops = await _cropService.GetAllCrop();
            if (crops == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(crops);
            }
        }
        //getCropByCropID
        [HttpGet("getCropByCropID/:cropId")]
        public async Task<ActionResult<Crop>> GetCropByCropID(int cropId)
        {
            var crop = await _cropService.GetGropByCropId(cropId);
            if (crop == null)
                return NotFound(new { message = "Cannot Find any Crob" });
            return Ok(crop);
        }

        //addCrops

        [HttpPost("addCrop")]
        public async Task<ActionResult> AddCrop([FromBody] Crop crop)
        {
            try
            {
                var sucess = await _cropService.AddCrop(crop);
                if (sucess)
                {
                    return Ok(new { message = "Crop added sucessfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to add crop" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // updateCropByCropID

        [HttpPut("updateCropByCropID/:cropId")]
        public async Task<ActionResult> UpdateCropByCropId(int cropId, [FromBody] Crop crop)
        {
            try
            {
                var sucess = await _cropService.UpdateCropByCropId(cropId, crop);

                if (sucess)
                    return Ok(new { message = "crop updated sucessfully" });
                else
                    return StatusCode(500, new { message = "Cannot find any crop" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new {message = ex.Message});
            }
        }

        //deleteCropByCropID
        [HttpDelete("deleteCropByCropID/:cropId")]
        public async Task<ActionResult> DeleteCropByCropID(int cropId)
        {
            try
            {
                var sucess= await _cropService.DeleteCropByCropID(cropId);
                if (sucess)
                    return Ok(new { message = "crop deleted sucessfully" });
                else
                    return StatusCode(500, new { message = "Cannot find any crop" });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
