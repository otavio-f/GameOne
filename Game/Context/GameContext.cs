using Game.Models;
using Microsoft.EntityFrameworkCore;

namespace Game.Context
{
    public class GameContext : DbContext
    {
        private const int NAME_SIZE = 128;
        private const int DESCRIPTION_SIZE = 1024;

        public GameContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Skill> Habilities { get; set; }
        public DbSet<Item> Items { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // *** The Character config ***
            
            // Make the DB ignore the Character's Hability, Item or Stats
            builder.Entity<Character>()
                .Ignore(c => c.Skills);
            builder.Entity<Character>()
                .Ignore(c => c.Items);
            builder.Entity<Character>()
                .Ignore(c => c.Stats);

            // Character attributes in the same table
            builder.Entity<Character>()
                .OwnsOne(c => c.Attributes);

            // Each Character has a unique and short name
            builder.Entity<Character>()
                .HasIndex(c => c.Name)
                    .IsUnique();
            builder.Entity<Character>()
                .Property(c => c.Name)
                    .HasMaxLength(NAME_SIZE);

            // Each Character can have a moderately lengthy description
            builder.Entity<Character>()
                .Property(c => c.Description)
                    .HasMaxLength(DESCRIPTION_SIZE);
            
            // *** The Item config ***

            // Each Item has a unique and short name
            builder.Entity<Item>()
                .HasIndex(i => i.Name)
                    .IsUnique();
            builder.Entity<Item>()
                .Property(i => i.Name)
                    .HasMaxLength(NAME_SIZE);

            // Each Item has a moderately lengthy description
            builder.Entity<Item>()
                .Property(i => i.Description)
                    .HasMaxLength(DESCRIPTION_SIZE);

            // Item Attributes in the same table
            builder.Entity<Item>()
                .OwnsOne(i => i.Attributes);

            // *** The Hability config ***

            // Each Hability has a unique and short name
            builder.Entity<Skill>()
                .HasIndex(h => h.Name)
                    .IsUnique();
            builder.Entity<Skill>()
                .Property(h => h.Name)
                    .HasMaxLength(NAME_SIZE);

            // Each Hability has a moderately lengthy description
            builder.Entity<Skill>()
                .Property(h => h.Description)
                    .HasMaxLength(DESCRIPTION_SIZE);
        }
    }
}
