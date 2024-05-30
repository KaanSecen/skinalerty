namespace BLL.Models
{
    public class User(int id, string name, string email, string password) : Domain.Entities.User(id, name, email, password)
    {
        public bool IsPasswordLengthValid()
        {
            return Password.Length >= 8;
        }

        public bool IsEmailFormatValid()
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(Email);
        }

        public void HashPassword()
        {
            Password = BCrypt.Net.BCrypt.HashPassword(Password);
        }

        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
}