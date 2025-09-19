using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;

        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public Client? Login(string email, string password)
        {
            var client = _clientService.Get(email);

            // Eerst controleren of client bestaat, dan kijken of het wachtwoord juist is.
            if (client != null && PasswordHelper.VerifyPassword(password, client.Password))
            {
                return client;
            }

            return null;
        }
    }
}
