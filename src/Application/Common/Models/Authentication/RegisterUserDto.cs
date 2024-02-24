namespace clean.Application.Common.Models.Authentication
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }

    }
}
