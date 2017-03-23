namespace eBiB.Models
{
    public class Client
    {
        public string Email { get; private set; }

        public string Name { get; set; }

        public Client(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}