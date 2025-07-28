using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.Models.Entities;

namespace TestApplication.BLL.Interface
{
    public interface ITokenService
    {
        string GenerateJwtToken(ApplicationUser user);
    }
}
