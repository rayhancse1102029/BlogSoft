using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSoft.Data;
using BlogSoft.Data.Entity;
using BlogSoft.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogSoft.Service
{
    public class UserService : IUserService
    {

        private readonly BlogDbContext _context;
        //private readonly UserManager<ApplicationUser> _userManager;

        public UserService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUser()
        {
            var model = await _context.Users.ToListAsync();

            return model;
        }

        public async Task<ApplicationUser> GetUserInfoByUserEmail(string email)
        {

            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

           return user;

        }
    }
}
