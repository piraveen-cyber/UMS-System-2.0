using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS_System_2._0.Repositories;

namespace UMS_System_2._0.Controllers
{
    public static class LoginController
    {
        public static async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await DatabaseManager.Instance.GetUserAsync(username, password);
            return user?.Role;
        }
    }
}
