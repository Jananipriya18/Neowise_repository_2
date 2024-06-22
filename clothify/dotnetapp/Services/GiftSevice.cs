// Services/GiftService.cs
using System.Collections.Generic;
using dotnetapp.Models;

namespace dotnetapp.Services
{
    public interface GiftService
    {
        // Gift addGift(Gift gift);
        // List<Gift> viewAllGifts();
        // Gift updateGift(long giftId, Gift updatedGift);
        // Gift deleteGift(long giftId);

        Gift AddGift(Gift gift);
        List<Gift> GetAllGifts();
        Gift EditGift(long giftId, Gift updatedGift);
        Gift DeleteGift(long giftId);
    }
}

