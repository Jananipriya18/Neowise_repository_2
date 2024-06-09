using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Repository
{
    public class MobilePhoneRepository
    {
        private static List<MobilePhone> _mobilePhones = new List<MobilePhone>()
        {
            new MobilePhone { MobilePhoneId = 1, Brand = "Brand A", Model = "Model X", Price = 699.99m, StockQuantity = 50 },
            new MobilePhone { MobilePhoneId = 2, Brand = "Brand B", Model = "Model Y", Price = 799.99m, StockQuantity = 30 },
            new MobilePhone { MobilePhoneId = 3, Brand = "Brand C", Model = "Model Z", Price = 899.99m, StockQuantity = 20 }
        };

        public List<MobilePhone> GetMobilePhones() => _mobilePhones;

        public MobilePhone GetMobilePhone(int id) => _mobilePhones.FirstOrDefault(mp => mp.MobilePhoneId == id);

        public MobilePhone SaveMobilePhone(MobilePhone mobilePhone)
        {
            mobilePhone.MobilePhoneId = _mobilePhones.Count > 0 ? _mobilePhones.Max(mp => mp.MobilePhoneId) + 1 : 1;
            _mobilePhones.Add(mobilePhone);
            return mobilePhone;
        }

        public MobilePhone UpdateMobilePhone(int id, MobilePhone mobilePhone)
        {
            var existingMobilePhone = GetMobilePhone(id);
            if (existingMobilePhone == null)
            {
                return null;
            }
            existingMobilePhone.Brand = mobilePhone.Brand;
            existingMobilePhone.Model = mobilePhone.Model;
            existingMobilePhone.Price = mobilePhone.Price;
            existingMobilePhone.StockQuantity = mobilePhone.StockQuantity;
            return existingMobilePhone;
        }

        public bool DeleteMobilePhone(int id)
        {
            var mobilePhone = GetMobilePhone(id);
            if (mobilePhone == null)
            {
                return false;
            }
            _mobilePhones.Remove(mobilePhone);
            return true;
        }
    }
}
