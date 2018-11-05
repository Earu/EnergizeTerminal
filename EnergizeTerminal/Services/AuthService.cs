using System;
using System.Collections.Generic;

namespace EnergizeTerminal.Services
{
    public static class AuthService
    {
        private static Dictionary<string, DateTime> _Sessions = new Dictionary<string, DateTime>();

        public static string GenerateToken()
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            _Sessions[token] = DateTime.Now.AddMinutes(5);

            return token;
        }

        public static void EndToken(string token)
        {
            if (_Sessions.ContainsKey(token))
                _Sessions.Remove(token);
        }

        public static void RefreshToken(string token)
            => _Sessions[token] = DateTime.Now.AddMinutes(5);

        public static bool IsAuthorized(string token)
        {
            if (_Sessions.ContainsKey(token))
            {
                DateTime expiration = _Sessions[token];
                if (expiration > DateTime.Now) return true;
                else EndToken(token);
            }

            return false;
        }
    }
}
