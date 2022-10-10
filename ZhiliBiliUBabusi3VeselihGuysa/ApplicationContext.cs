using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;

namespace ZhiliBiliUBabusi3VeselihGuysa
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<MathScore> Math => Set<MathScore>();
        public DbSet<MatchScore> Match => Set<MatchScore>();
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public static void SaveRes(string name, int time, bool math)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (name == "")
                    return;
                User plr = db.Users.Where(b => b.Name == name).FirstOrDefault();
                if (plr == null)
                {
                    /*User.CreatePlayer(name, db);
                    plr = db.Users.Where(b => b.Name == name).FirstOrDefault();*/
                }
                if(plr != null)
                {
                    if (math)
                        db.Math.Add(new MathScore(plr, time));
                    else
                        db.Match.Add(new MatchScore(plr, time));
                    db.SaveChanges();
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=game.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Name)
            .IsUnique();
        }
    }
}
