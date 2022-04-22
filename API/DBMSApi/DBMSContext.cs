using DBMSApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DBMSApi
{
    public class DBMSContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<House> houses { get; set; }
        public DbSet<Ingredient> ingredients { get; set; }
        public DbSet<Recipe> recipes { get; set; }
        public DbSet<RecipeIngredient> recipeIngredients { get; set; }
        public DbSet<Roomate> roomates { get; set; }
        public DbSet<RoomateIngredient> roomateIngredients { get; set; }

        public string DbPath { get; }
        public DBMSContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "DBMS.db");
            Console.WriteLine(DbPath);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Set up roomate ingredient connection
            builder.Entity<Roomate>()
                .HasMany(p => p.ingredients)
                .WithMany(p => p.roomates)
                .UsingEntity<RoomateIngredient>(
                    j => j
                        .HasOne(ri => ri.ingredient)
                        .WithMany(i => i.roomateIngredients)
                        .HasForeignKey(ri => ri.ingredientId),
                    j => j
                        .HasOne(ri => ri.roomate)
                        .WithMany(r => r.roomateIngredients)
                        .HasForeignKey(ri => ri.roomateId),
                    j =>
                    {
                        j.HasKey(i => new { i.roomateId, i.ingredientId });
                    }
                );

            // Set up recipe ingredient Connection
            builder.Entity<Recipe>()
                .HasMany(p => p.ingredients)
                .WithMany(p => p.recipes)
                .UsingEntity<RecipeIngredient>(
                    j => j
                        .HasOne(ri => ri.ingredient)
                        .WithMany(i => i.recipeIngredients)
                        .HasForeignKey(ri => ri.ingredientId),
                    j => j
                        .HasOne(ri => ri.recipe)
                        .WithMany(r => r.recipeIngredients)
                        .HasForeignKey(ri => ri.recipeId),
                    j =>
                    {
                        j.HasKey(i => new { i.recipeId, i.ingredientId });
                    }
                );
        }
    }
}
