namespace Grocery.Core.Models
{
    public partial class Client : Model
    {
        private string _emailAddress;
        private string _password;

        public string EmailAddress
        {
            get => _emailAddress;
            private set => _emailAddress = value;
        }

        public string Password
        {
            get => _password;
            private set => _password = value;
        }

        public Client(int id, string name, string emailAddress, string password) : base(id, name)
        {
            _emailAddress = emailAddress;
            _password = password;
        }
    }
}