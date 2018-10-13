using System;
using System.Collections.Generic;

namespace EnergizeTerminal.Services
{
    public static class AuthService
    {
        private static Dictionary<string, DateTime> _Sessions = new Dictionary<string, DateTime>();

        public static string GenerateToken()
        {
            string token = string.Empty;
            _Sessions[token] = DateTime.Now.AddHours(2);

            return token;
        }

        public static void EndToken(string token)
        {
            if (_Sessions.ContainsKey(token))
                _Sessions.Remove(token);
        }

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
