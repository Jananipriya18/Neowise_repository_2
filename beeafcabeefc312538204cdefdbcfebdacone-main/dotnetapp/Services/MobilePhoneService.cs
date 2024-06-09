using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Services
{
    public class MobilePhoneService : IMobilePhoneService
    {
        private static List<MobilePhone> _mobilePhones = new List<MobilePhone>
        {
            new MobilePhone { MobilePhoneId = 1, Brand = "Brand A", Model = "Model X", Price = 699.99m, StockQuantity = 50 },
            new MobilePhone { MobilePhoneId = 2, Brand = "Brand B", Model = "Model Y", Price = 799.99m, StockQuantity = 30 }
        };

        public IEnumerable<MobilePhone> GetAllMobilePhones()
        {
            return _mobilePhones;
        }

        public MobilePhone GetMobilePhoneById(int id)
        {
            return _mobilePhones.FirstOrDefault(mp => mp.MobilePhoneId == id);
        }

        public void AddMobilePhone(MobilePhone mobilePhone)
        {
            mobilePhone.MobilePhoneId = _mobilePhones.Count > 0 ? _mobilePhones.Max(mp => mp.MobilePhoneId) + 1 : 1;
            _mobilePhones.Add(mobilePhone);
        }

        public void UpdateMobilePhone(MobilePhone mobilePhone)
        {
            var existingMobilePhone = _mobilePhones.FirstOrDefault(mp => mp.MobilePhoneId == mobilePhone.MobilePhoneId);
            if (existingMobilePhone != null)
            {
                existingMobilePhone.Brand = mobilePhone.Brand;
                existingMobilePhone.Model = mobilePhone.Model;
                existingMobilePhone.Price = mobilePhone.Price;
                existingMobilePhone.StockQuantity = mobilePhone.StockQuantity;
            }
        }

        public void DeleteMobilePhone(int id)
        {
            var mobilePhoneToRemove = _mobilePhones.FirstOrDefault(mp => mp.MobilePhoneId == id);
            if (mobilePhoneToRemove != null)
            {
                _mobilePhones.Remove(mobilePhoneToRemove);
            }
        }
    }
}
