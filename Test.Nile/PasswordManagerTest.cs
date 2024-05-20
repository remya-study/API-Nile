using Core_Nile.Common;
using System.ComponentModel.DataAnnotations;
namespace Test.Nile
{
    [TestClass]
    public class PasswordManagerTest
    {
        PasswordManager passwordManager;
        string password = "",hash="9jdh",salt="not1my2salt";
        [TestInitialize()]
        public void Initialize()
        {
            password = "verylongboringpassword123";
            passwordManager = new PasswordManager();
        }
        
        [TestMethod]
      
        public void PasswordManager_HashPassword_NullPassword_ThrowsArgumentException()
        {
            //Arrange
             
            Assert.ThrowsException<ArgumentException>(() => passwordManager.HashPasword(null));
        }
        [TestMethod]
        public void PasswordManager_HashPassword_Return_Non_empty_Hash_And_Salt()
        {
            //Arrange
            var response= passwordManager.HashPasword(password);
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.hash);
            Assert.IsNotNull(response.hash);
        }


        [TestMethod]
        public void PasswordManager_VerifyPassword_NullPassword_ThrowsArgumentException()
        {
            //Arrange

            Assert.ThrowsException<ArgumentException>(() => passwordManager.VerifyPassword(null, hash,salt));
        }
    }
}