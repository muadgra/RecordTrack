﻿using Microsoft.EntityFrameworkCore;
using RecordTrack.Domain.Entities;
using RecordTrack.Domain.Entities.Common;
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //Entityler üzerinden yapılan değişikliklerin veya yeni yaratılan verinin yakalanmasını sağlar.
            //Update operasyonlarında track edilen verileri yakalar.
            var data = ChangeTracker.Entries<BaseEntity>();
            foreach (var item in data)
            {
                _ = item.State switch
                {
                    EntityState.Added => item.Entity.CreateDate = DateTime.UtcNow,
                    EntityState.Modified => item.Entity.UpdateDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
