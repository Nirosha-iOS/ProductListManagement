using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductListManagement.Models;

namespace ProductListManagement.Data
{
    public class AppDBContect:DbContext
    {
        public AppDBContect(DbContextOptions<AppDBContect> options): base(options)        
        {


        }

        public DbSet<Brand> Brands { get; set; }

    }
}
