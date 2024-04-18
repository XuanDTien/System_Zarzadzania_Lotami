
using Microsoft.EntityFrameworkCore;
using System_Zarzadzania_Lotami.Data;

namespace System_Zarzadzania_Lotami.Services
{
    public class AuthService : IAuthService
    {
        private readonly FlightSystemContext _context;

        public AuthService(FlightSystemContext context)
        {
            _context = context;
        }
        public async Task<bool> ValidateCredentials(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return false;

            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
