using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class CropService
    {
        public readonly ApplicationDbContext _context;
        public CropService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCrop(Crop newCrop)
        {
            _context.Crops.Add(newCrop);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCropByCropID(int cropId)
        {
            var crop = await _context.Crops.FirstOrDefaultAsync(c => c.CropId == cropId);
            if (crop == null)
            {
                return false;
            }
            _context.Crops.Remove(crop);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Crop>> GetAllCrop()
        {
            return await _context.Crops.ToListAsync();
        }
        public async Task<Crop> GetGropByCropId(int cropId)
        {
            return await _context.Crops.FirstOrDefaultAsync(c => c.CropId == cropId);
        }

        public async Task<bool> UpdateCropByCropId(int cropId, Crop crop)
        {
            var existingCrop = await _context.Crops.FirstOrDefaultAsync(c => c.CropId == cropId);
            if (existingCrop == null)
            {
                return false;
            }
            _context.Entry(existingCrop).CurrentValues.SetValues(crop);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
