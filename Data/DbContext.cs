using System.Data.Entity;
using Zencareservice.Models;

namespace Zencareservice.Data
{
    public class DbContext 
    {
        public DbSet<Signup> Signup { get; set; }
    }
}
