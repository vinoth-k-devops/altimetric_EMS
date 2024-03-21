using System;
namespace EMS_Domain.Model
{
	public class AuthenticatedResponse
	{
        public string? UName { get; set; }

        public string? Token { get; set; }

        public string? RefreshToken { get; set; }
    }
}

