using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class RequestService
    {
        private readonly ApplicationDbContext _context;

        public RequestService(ApplicationDbContext context)
        {
            _context = context;
        }


        //getRequestByUserId
        public async Task<Request> getRequestByUserId(int userId)
        {
            return await _context.Requests.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        //addRequest
        public async Task<bool> AddRequest(Request newRequest)
        {
            _context.Requests.Add(newRequest);
            await _context.SaveChangesAsync();
            return true;
        }
        //deleteRequestByRequestId
        public async Task<bool> DeleteRequestByRequestId(int requestId)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(c => c.RequestId == requestId);
            if (request == null)
            {
                return false;
            }
            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }

        //getAllRequest
        public async Task<IEnumerable<Request>> GetAllRequest()
        {
            return await _context.Requests.ToListAsync();
        }
        //getRequestByRequestId
        public async Task<Request> GetRequestByRequestId(int requestId)
        {
            return await _context.Requests.FirstOrDefaultAsync(c => c.RequestId == requestId);
        }
        //updateRequestByRequestId
        public async Task<bool> UpdateRequestByRequestId(int requestId, Request request)
        {
            var existingRequest = await _context.Requests.FirstOrDefaultAsync(c => c.RequestId == requestId);
            if (existingRequest == null)
            {
                return false;
            }
            _context.Entry(existingRequest).CurrentValues.SetValues(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
