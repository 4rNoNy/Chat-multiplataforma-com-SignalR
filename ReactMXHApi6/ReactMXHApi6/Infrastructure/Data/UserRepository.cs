﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReactMXHApi6.Core.Entities;
using ReactMXHApi6.Core.Interfaces;
using ReactMXHApi6.Dtos;

namespace ReactMXHApi6.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtém um usuário pelo nome de usuário
        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.UserName == username);
        }

        // Obtém um usuário pelo ID
        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Obtém um objeto MemberDto (informações do membro) com base no nome de usuário
        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users.Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        // Obtém uma lista de objetos MemberDto (informações de membros) para usuários online
        public async Task<List<MemberDto>> GetUsersOnlineAsync(string currentUsername, string[] userOnline)
        {
            var listUserOnline = new List<MemberDto>();
            foreach (var u in userOnline)
            {
                var user = await GetMemberAsync(u);
                listUserOnline.Add(user);
            }
            return await Task.Run(() => listUserOnline.Where(x => x.UserName != currentUsername).ToList());
        }
    }
}
