using BEApi.Model;
using Microsoft.EntityFrameworkCore;

namespace BEApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> refreshToken { get; set; }
        public DbSet<Diary> Diary { get; set; }
        public DbSet<DiaryDetails> DiaryDetails { get; set; }
    }
}