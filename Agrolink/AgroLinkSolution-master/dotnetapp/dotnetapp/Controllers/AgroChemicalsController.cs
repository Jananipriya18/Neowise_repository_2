using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetapp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AgroChemicalsController : ControllerBase
    {
        private readonly AgrochemicalService _agrochemicalService;

        public AgroChemicalsController(AgrochemicalService agrochemicalService)
        {
            _agrochemicalService = agrochemicalService;
        }
        //addAgrochemical
        [HttpPost("addAgrochemical")]
        public async Task<ActionResult> AddAgrochemical(AgroChemicals agroChemicals)
        {
            try
            {
                var sucess = await _agrochemicalService.AddAgrochemical(agroChemicals);
                if (sucess)
                {
                    return Ok(new { message = "AgroChemical added sucessfully" });
                }
                else
                {
                    return StatusCode(500, new { message = "Failed to add AgroChemical" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }
       
        //getAgrochemicalByAgrochemicalID
        [HttpGet("getAgrochemicalByAgrochemicalID/:agroChemicalID")]
        public async Task<ActionResult> GetAgrochemicalByAgrochemicalID(int agroChemicalID)
        {
            var agroChemical = await _agrochemicalService.GetAgrochemicalByAgrochemicalID(agroChemicalID);
            if (agroChemical == null)
                return NotFound(new { message = "Cannot Find any AgroChemicals" });
            return Ok(agroChemical);
        }
        //updateAgrochemicalByAgrochemicalID
        [HttpPut("updateAgrochemicalByAgrochemicalID/:agrochemicalId")]
        public async Task<ActionResult> UpdateAgrochemicalByAgrochemicalID(int agroChemicalID, AgroChemicals agroChemicals)
        {
            try
            {
                var sucess = await _agrochemicalService.UpdateAgrochemicalByAgrochemicalID(agroChemicalID, agroChemicals);

                if (sucess)
                    return Ok(new { message = "AgroChemical updated sucessfully" });
                else
                    return StatusCode(500, new { message = "Cannot find any AgroChemical" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        //deleteAgrochemicalByAgrochemicalID
        [HttpDelete("deleteAgrochemicalByAgrochemicalID/:agrochemicalId")]
        public async Task<ActionResult> DeleteAgrochemicalByAgrochemicalID(int agroChemicalID)
        {

            try
            {
                var sucess = await _agrochemicalService.DeleteAgrochemicalByAgrochemicalID(agroChemicalID);
                if (sucess)
                    return Ok(new { message = "AgroChemical deleted sucessfully" });
                else
                    return StatusCode(500, new { message = "Cannot find any AgroChemical" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        //getAllAgrochemicals
        [HttpGet("getAllAgrochemicals")]
        public async Task<ActionResult<IEnumerable<AgroChemicals>>> GetAllAgrochemicals()
        {
            var agroChemicalas = await _agrochemicalService.GetAllAgrochemicals();
            if (agroChemicalas == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(agroChemicalas);
            }
        }
        // getAgrochemicalsByUserId

    }
}
