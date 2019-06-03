using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class OLXDataContext: DbContext
    {
        public OLXDataContext(DbContextOptions<OLXDataContext> options) : base(options)
        {

        }

        public DbSet<TableAd> Ads { get; set; }
        public DbSet<TableUser> Users { get; set; }
    }
}
