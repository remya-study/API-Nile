namespace Model.Nile
{
    public class RegisterUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
       
        public  string ConfirmPassword { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public  string CreatedBy { get; set; }
        public  string ModifiedBy { get; set; }

       
    }
}
