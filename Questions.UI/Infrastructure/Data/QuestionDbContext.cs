using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Configuration;

namespace Infrastructure.Data
{
    public class QuestionDbContext : DbContext
    {

        public QuestionDbContext(DbContextOptions<QuestionDbContext> options) : base(options)
        {
        }

        public class DbContextFactory : IDesignTimeDbContextFactory<QuestionDbContext>
        {
            public QuestionDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<QuestionDbContext>();

                // Get the connection string from configuration
                string connectionString = "Host= ::1; Port=5432 ;Database = TestDB; UserId = postgres; Password = 2244;";

                optionsBuilder.UseNpgsql(connectionString);

                return new QuestionDbContext(optionsBuilder.Options);
            }
        }


      

        public DbSet<DocxFile> DocxFiles { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<User> Users { get; set; }


    }

}
