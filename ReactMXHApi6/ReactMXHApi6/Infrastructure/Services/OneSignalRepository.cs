using Microsoft.EntityFrameworkCore;
using ReactMXHApi6.Core.Entities;
using ReactMXHApi6.Core.Interfaces;
using ReactMXHApi6.Infrastructure.Data;

namespace ReactMXHApi6.Infrastructure.Services
{
    public class OneSignalRepository : IOneSignalRepository
    {
        private readonly DataContext _context;

        public OneSignalRepository(DataContext context)
        {
            _context = context;
        }

        // Adiciona um PlayerId ao contexto
        public void AddPlayerId(PlayerIds player)
        {
            _context.PlayerIds.Add(player);
        }

        // Obtém um usuário pelo nome de usuário, incluindo os seus PlayerIds associados
        public async Task<AppUser> GetUserByUsername(string username)
        {
            var appUser = await _context.Users
                .Include(u => u.PlayerIds)
                .SingleOrDefaultAsync(x => x.UserName == username);
            return appUser;
        }
    }
}
