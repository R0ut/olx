using System;
using System.Collections.Generic;
using API.Models;
using API.Services.Interfaces;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly OLXDataContext _context;

        public UserService(OLXDataContext context)
        {
            _context = context;
        }

        public void SaveNewAddsToDB(IList<TableUser> ads)
        {
            var date = DateTimeOffset.Now;
            foreach (var ad in ads)
            {
                ad.DateOfCreation = date;
            }
            _context.Users.AddRange(ads);

            _context.SaveChanges();
        }
    }
}
