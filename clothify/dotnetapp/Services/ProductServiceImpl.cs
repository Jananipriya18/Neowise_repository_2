using System.Collections.Generic;
using dotnetapp.Models;
using dotnetapp.Services;
public class GiftServiceImpl : GiftService
{
    private readonly GiftRepository _giftRepository;

    public GiftServiceImpl(GiftRepository giftRepository)
    {
        _giftRepository = giftRepository;
    }

    public Gift AddGift(Gift gift)
    {
        return _giftRepository.addGift(gift);
    }

    public List<Gift> GetAllGifts()
    {
        return _giftRepository.getAllGifts();
    }

    public Gift EditGift(long giftId, Gift updatedGift)
    {
        return _giftRepository.editGift(giftId, updatedGift);
    }

    public Gift DeleteGift(long giftId)
    {
        return _giftRepository.deleteGift(giftId);
    }
}
