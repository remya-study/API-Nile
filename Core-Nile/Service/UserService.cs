using Core_Nile.Common;
using Core_Nile.Repo;
using Model.Nile;
using Model.Nile.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Nile.Service
{
    public  class UserService : IUserService
    {
        private INileRepo _repo;
        public UserService(INileRepo NileRepo) {
            _repo = NileRepo;
              
        }

        public bool CheckIfUserIdExists(string login)
        {
           return _repo.CheckIfUserIdExists(login);
        }

        public int CreateUser(RegisterUser registerUser)
        { 
            PasswordManager passwordManager = new PasswordManager();
            var(hash, salt) = passwordManager.HashPasword(registerUser.Password);
            registerUser.Password = hash;

            return _repo.CreateUser(registerUser, salt);
        }

        public UserProfile GetUserProfile(string login)
        {
        return _repo.GetUserProfileByLogin(login);
        }

        public List<UserLogin> GetUsers()
        {
            return _repo.GetUsers();
        }

        public bool ValidateUser(string login, string password)
        {

            var user = _repo.GetUserByLogin(login);

            if (user == null)
            {
                return false;
            }
            PasswordManager passwordManager = new PasswordManager();
            var isPasswordvalid = passwordManager.VerifyPassword(password, user.Password, user?.Salt);
            return isPasswordvalid;
        }
        
    }
}
