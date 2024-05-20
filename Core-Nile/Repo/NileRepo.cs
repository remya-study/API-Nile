using Dapper;
using Microsoft.Data.SqlClient;
using Model.Nile;
using Model.Nile.Interface;
using Model.Nile.Options;
using System.Data;

namespace Core_Nile.Repo
{
    public class NileRepo : INileRepo
    {     
        SqlConnection sqlConnection;
        public NileRepo(ConnectionStringOptions options) {
             
               sqlConnection = new  SqlConnection(options?.Nile);
        }

        public bool CheckIfUserIdExists(string login)
        {
          var result=  sqlConnection?.Query($"select 1 from userlogin where UserName=@login", new { login }).FirstOrDefault();
            return result != null;
        }

        public int CreateUser(RegisterUser registerUser,string salt)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@UserName", registerUser.UserName);
            parameters.Add("@password", registerUser.Password);
            parameters.Add("@salt", salt);
            parameters.Add("@FirstName", registerUser.FirstName);
            parameters.Add("@LastName", registerUser.LastName);

            int result = (int)sqlConnection?.ExecuteScalar<int>("usp_SaveUser", parameters, commandType: CommandType.StoredProcedure);
            return result;


        }

        public UserLogin GetUserByLogin(string login)
        {
            return sqlConnection?.Query<UserLogin>($"select * from userlogin where UserName=@login",new { login }).FirstOrDefault();
        }

        public UserProfile GetUserProfileByLogin(String login)
        {
            return sqlConnection?.Query<UserProfile>($"Select FirstName,LastName,UserName,Password from userlogin WHERE UserName=@login", new { login }).FirstOrDefault();
        }
        public List<UserLogin> GetUsers()
        {
           return sqlConnection.Query<UserLogin>("select * from userlogin").ToList() ;
        }

       

        
    }
}
