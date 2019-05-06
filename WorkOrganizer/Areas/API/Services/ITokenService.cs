using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkOrganizer.Areas.API.Services
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync(string username, string password);
    }
}
