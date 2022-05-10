﻿using ProEventos.Domain.Identity;
using ProEventos.Persistence.Contexto;
using ProEventos.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class UserPersistence : GeralPersistence, IUserPersistence
    {
        private readonly ProEventosContext _context;
        public UserPersistence(ProEventosContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.UserName == userName.ToLower());
        }
    }
}
