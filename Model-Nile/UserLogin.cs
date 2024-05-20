namespace Model.Nile
{
    public class UserLogin
    {
        public int ID { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public required string CreatedBy { get; set; }
        public required string ModifiedBy { get; set; }

        public string? Salt { get; set; }
        

    }
}
