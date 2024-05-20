using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class AgrochemicalService
    {
        private readonly ApplicationDbContext _context;

        public AgrochemicalService(ApplicationDbContext context)
        {
            _context = context;
        }


        //addAgrochemical
        public async Task<bool> AddAgrochemical(AgroChemicals agroChemicals)
        {
            _context.AgroChemicals.Add(agroChemicals);
            await _context.SaveChangesAsync();
            return true;
        }
        //getAgrochemicalByAgrochemicalID
        public async Task<AgroChemicals> GetAgrochemicalByAgrochemicalID(int agrochemicalID)
        {
            return await _context.AgroChemicals.FirstOrDefaultAsync(ac => ac.AgroChemicalId == agrochemicalID);
        }
        //updateAgrochemicalByAgrochemicalID
        public async Task<bool> UpdateAgrochemicalByAgrochemicalID(int agrochemicalID, AgroChemicals agroChemicals)
        {
            var existingAgroChemical = await _context.AgroChemicals.FirstOrDefaultAsync(c => c.AgroChemicalId == agrochemicalID);
            if (existingAgroChemical == null)
            {
                return false;
            }
            _context.Entry(existingAgroChemical).CurrentValues.SetValues(agroChemicals);
            await _context.SaveChangesAsync();
            return true;
        }
        //deleteAgrochemicalByAgrochemicalID
        public async Task<bool> DeleteAgrochemicalByAgrochemicalID(int agrochemicalID)
        {
            var agroChemical = await _context.AgroChemicals.FirstOrDefaultAsync(c => c.AgroChemicalId == agrochemicalID);
            if (agroChemical == null)
            {
                return false;
            }
            _context.AgroChemicals.Remove(agroChemical);
            await _context.SaveChangesAsync();
            return true;
        }
        //getAllAgrochemicals
        public async Task<IEnumerable<AgroChemicals>> GetAllAgrochemicals()
        {
            return await _context.AgroChemicals.ToListAsync();
        }
        // getAgrochemicalsByUserId
        
        public async Task<IEnumerable<AgroChemicals>> GetAgrochemicalsByUserId(int userId)
        {
            return await _context.AgroChemicals.Include(x=>x.Equals(userId)).ToListAsync();
        }

    }
}
