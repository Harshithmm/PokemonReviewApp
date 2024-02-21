using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
    public class DataContext(DbContextOptions<DataContext> options): DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pokemon> Pokemon { get; set; }

        public DbSet<PokemonOwner> PokemonOwners { get; set; }
        public DbSet<PokemonCategory> PokemonCategories { get; set; }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        public DbSet<Country> Countries { get; set; }

        //used to configure the relationship between the tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PokemonCategory>().HasKey(pc => new { pc.PokemonId, pc.CategoryId });

            modelBuilder.Entity<PokemonCategory>()
                .HasOne(pc => pc.Pokemon)
                .WithMany(p => p.PokemonCategories)
                .HasForeignKey(pc => pc.PokemonId);

            modelBuilder.Entity<PokemonCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(p => p.PokemonCategories)
            .HasForeignKey(pc => pc.CategoryId);


            modelBuilder.Entity<PokemonOwner>().HasKey(po => new { po.PokemonId, po.OwnerId });

            modelBuilder.Entity<PokemonOwner>()
                .HasOne(pc => pc.Pokemon)
                .WithMany(p => p.PokemonOwners)
                .HasForeignKey(pc => pc.PokemonId);

            modelBuilder.Entity<PokemonOwner>()
            .HasOne(pc => pc.Owner)
            .WithMany(p => p.PokemonOwners)
            .HasForeignKey(pc => pc.OwnerId);

            //can add data to the database using the following code

            //modelBuilder.Entity<Category>().HasData(
            //                   new Category { CategoryId = 1, Name = "Grass" },
            //                                  new Category { CategoryId = 2, Name = "Fire" },
            //                                  new Category { CategoryId = 3, Name = "Water" }
            //                              );

            //modelBuilder.Entity<Pokemon>().HasData(
            //                   new Pokemon { PokemonId = 1, Name = "Bulbasaur", CategoryId = 1 },
            //                                  new Pokemon { PokemonId = 2, Name = "Charmander", CategoryId = 2 },
            //                                  new Pokemon { PokemonId = 3, Name = "Squirtle", CategoryId = 3 }
            //                              );

            //modelBuilder.Entity<Review>().HasData(
            //                   new Review { ReviewId = 1, PokemonId = 1, Rating = 5, ReviewText = "Bulbasaur is the best!" },
            //                                  new Review { ReviewId = 2, PokemonId = 2, Rating = 4, ReviewText = "Charmander is pretty good." },
            //                                  new Review { ReviewId = 3, PokemonId = 3, Rating = 3, ReviewText = "Squirtle is okay." }
            //                              );
        }
    }
}
