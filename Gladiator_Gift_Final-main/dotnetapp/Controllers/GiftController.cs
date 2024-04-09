//new
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dotnetapp.Models;
using dotnetapp.Services;
using Microsoft.AspNetCore.Authorization;

// [Authorize]

[Route("api/gift")]
[ApiController]
public class GiftController : ControllerBase
{
    private readonly GiftService _giftService;

    public GiftController(GiftService giftService)
    {
        _giftService = giftService;
    }

    [Authorize(Roles = "admin")] 
    [HttpPost]
    public IActionResult AddGift([FromBody] Gift gift)
    {
        var addedGift = _giftService.AddGift(gift);
        return Ok(addedGift);
    }

    [Authorize(Roles = "admin,customer")] 
    [HttpGet]
    public IActionResult GetAllGifts()
    {
        var allGifts = _giftService.GetAllGifts();
        return Ok(allGifts);
    }

    [Authorize(Roles = "admin,customer")] 
    [HttpPut("{giftId}")]
    public IActionResult EditGift(long giftId, [FromBody] Gift updatedGift)
    {
        var editedGift = _giftService.EditGift(giftId, updatedGift);
        if (editedGift != null)
        {
            return Ok(editedGift);
        }
        return NotFound("Gift not found");
    }

    [Authorize(Roles = "admin")] 
    [HttpDelete("{giftId}")]
    public IActionResult DeleteGift(long giftId)
    {
        var deletedGift = _giftService.DeleteGift(giftId);
        if (deletedGift != null)
        {
            return Ok(deletedGift);
        }
        return NotFound("Gift not found");
    }
}
