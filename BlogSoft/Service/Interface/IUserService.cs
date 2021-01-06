using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FBAI.Data.Entity;


namespace FBAI.Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUser();
        Task<ApplicationUser> GetUserInfoByUserEmail(string email);
    }
}
