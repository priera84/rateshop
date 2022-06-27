using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FedexAPI.Models.Db
{

    public class RatesDBContext : DbContext
    {
        public RatesDBContext(DbContextOptions<RatesDBContext> optionsBuilder) : base(optionsBuilder)
        {

        }
        public DbSet<Rate> Rates { get; set; }
    }
}