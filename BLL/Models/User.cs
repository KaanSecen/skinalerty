namespace BLL.Models
{
    public class User
    {
        public User(string name, string email, string password, int id = 0)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        public int Id { get; private set; }
        public string Name { get;  private set; }
        public string Email { get;  private set; }
        public string Password { get;  private set; }

        public bool IsPasswordLengthValid()
        {
            return Password.Length >= 8;
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