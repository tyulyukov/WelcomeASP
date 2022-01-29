using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WelcomeASP.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Entities.Faq> Faqs { get; set; }

        public DbSet<Entities.Portfolio.PortfolioCategory> PortfolioCategories { get; set; }
        public DbSet<Entities.Portfolio.PortfolioItem> PortfolioItems { get; set; }
        
        public DbSet<Entities.Trello.Card> Cards { get; set; }
        public DbSet<Entities.Trello.Board> Boards { get; set; }
        public DbSet<Entities.Trello.Column> Columns { get; set; }
        public DbSet<Entities.Trello.TrelloTag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Entities.Trello.Board>()
                .HasMany(b => b.Tags)
                .WithMany(t => t.Boards)
                .UsingEntity(j => j.ToTable("BoardTags"));
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
