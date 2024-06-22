// using System.Collections.Generic;
// using dotnetapp.Models;

// namespace dotnetapp.Services
// {
//     public class GiftServiceImpl : GiftService
//     {
//         private readonly GiftRepository _giftRepository;

//         public GiftServiceImpl(GiftRepository giftRepository)
//         {
//             _giftRepository = giftRepository;
//         }

//         public Gift addGift(Gift gift)
//         {
//             return _giftRepository.addGift(gift);
//         }

//         // Corrected method signature without parameters
//         public List<Gift> viewAllGifts()
//         {
//             // Update to use the modified repository method
//             return _giftRepository.viewAllGifts();
//         }

//         public Gift updateGift(long giftId, Gift updatedGift)
//         {
//             return _giftRepository.updateGift(giftId, updatedGift);
//         }

//         public Gift deleteGift(long giftId)
//         {
//             return _giftRepository.deleteGift(giftId);
//         }

//         public Gift getGiftById(long giftId)
//         {
//             var gift = _giftRepository.viewAllGifts()
//                                     .FirstOrDefault(c => c.GiftId == giftId);
//             return gift;
//         }

//     }
// }



//new
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
