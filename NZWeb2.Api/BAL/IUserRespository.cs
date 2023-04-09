using Microsoft.EntityFrameworkCore;
using NZWeb2.Api.Data;
using NZWeb2.Api.Models.Domain;
using System.Data;

namespace NZWeb2.Api.BAL
{
    //Step - 5
    public interface IUserRespository
    {
        Task<User> AuthenticationAsync(string username, string password);
    }
    public class UserRespository : IUserRespository
    {
        //Jwt.IO to check token values

        private readonly NZWalksDBContext _DBContext;
        
        public UserRespository(NZWalksDBContext DBContext) {
            _DBContext = DBContext;
        }
        public async Task<User> AuthenticationAsync(string username, string password)
        {
            var _Users = await _DBContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower() && x.Password == password);
            if (_Users == null)
            {
                return null;
            }
            var UserRoles = await _DBContext.User_Roles.Where(x => x.UserId == _Users.Id).ToListAsync();
            if (UserRoles.Any()) {

                _Users.Roles = new List<string>();

                foreach (var userrole in UserRoles) 
                {
                  var role=  await _DBContext.Roles.FirstOrDefaultAsync(x => x.Id == userrole.RoleId);
                    if (role != null) 
                    {
                        _Users.Roles.Add(role.Name);
                    }
                }
            }
            _Users.Password = null;
            return _Users;
        }
    }
    //
}
