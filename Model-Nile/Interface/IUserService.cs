using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Nile.Interface
{
    public interface IUserService
    {
        public List<UserLogin> GetUsers();
        public bool ValidateUser(string login, string password);
        public UserProfile GetUserProfile(string login);
        public bool CheckIfUserIdExists(string login);
        public int CreateUser(RegisterUser registerUser);

    }
}
