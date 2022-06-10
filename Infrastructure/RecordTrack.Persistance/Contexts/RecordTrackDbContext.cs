using Microsoft.EntityFrameworkCore;
using RecordTrack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Persistance.Contexts
{
    public class RecordTrackDbContext : DbContext
    {
        public RecordTrackDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
