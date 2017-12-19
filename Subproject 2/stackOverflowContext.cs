using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Subproject_2
{
    class stackOverflowContext : DbContext 
    {
        public DbSet<Post> posts { get; set; }
        public DbSet<Combination> combinations { get; set; }
        public DbSet<User> user { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<History> history { get; set; }
        public DbSet<Marking> marking { get; set; }
        public DbSet<Tag> tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(
                "Server = wt-220.ruc.dk; Port = 3306; Database = raw1; Uid = raw1; Pwd = raw1;" //put your own UserID and Password here
                );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Marking>()
                .HasKey(o => new { o.userID, o.postId });
            modelBuilder.Entity<Combination>()
                .HasKey(k => new { k.post_id, k.tags_id });

        }
    }
}
