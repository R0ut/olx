using System.Collections.Generic;
using API.Models;

namespace API.Services.Interfaces
{
    public interface IUserService
    {
        void SaveNewAddsToDB(IList<TableUser> ads);

    }
}
