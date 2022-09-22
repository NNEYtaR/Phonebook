using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<Account> Accounts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}