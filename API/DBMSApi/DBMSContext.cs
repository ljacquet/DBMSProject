using DBMSApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBMSApi
{
    public class DBMSContext : DbContext
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
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RecipeIngredient>()
                .HasKey(c => new { c.ingredientId, c.recipeId });

            builder.Entity<RoomateIngredient>()
                .HasKey(c => new { c.roomateId, c.ingredientId });
        }
    }
}
