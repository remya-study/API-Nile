using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Nile.Interface
{
    public interface INileRepo
    {
        public List<UserLogin> GetUsers();
        public UserLogin GetUserByLogin(string login);
        public UserProfile GetUserProfileByLogin(string login);
        public int CreateUser(RegisterUser registerUser,string salt);
        public bool CheckIfUserIdExists(string login);
    }
}
