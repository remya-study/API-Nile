using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Nile.Interface
{
    public interface IMicrosoftCalender
    {
        public Task<string> GetCelender(string email);
        public Task<string> GetUserProfileDetails(string email);
        public Task<string> GetAllUsers();
    }
}
