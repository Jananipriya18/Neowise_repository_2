using System.Collections.Generic;
using dotnetapp.Models;

namespace dotnetapp.Services
{
    public interface IMobilePhoneService
    {
        IEnumerable<MobilePhone> GetAllMobilePhones();
        MobilePhone GetMobilePhoneById(int id);
        void AddMobilePhone(MobilePhone mobilePhone);
        void UpdateMobilePhone(MobilePhone mobilePhone);
        void DeleteMobilePhone(int id);
    }
}
