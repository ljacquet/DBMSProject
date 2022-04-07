﻿// <auto-generated />
using System;
using DBMSApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DBMSApi.Migrations
{
    [DbContext(typeof(AppContext))]
    partial class AppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("DBMSApi.Models.House", b =>
                {
                    b.Property<int>("houseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("houseName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ownerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("houseId");

                    b.ToTable("houses");
                });

            modelBuilder.Entity("DBMSApi.Models.Ingredient", b =>
                {
                    b.Property<int>("ingredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("estimatedPrice")
                        .HasColumnType("REAL");

                    b.Property<string>("ingredientName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("substituteNames")
                        .HasColumnType("TEXT");

                    b.HasKey("ingredientId");

                    b.ToTable("ingredients");
                });

            modelBuilder.Entity("DBMSApi.Models.Recipe", b =>
                {
                    b.Property<int>("recipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("link")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("recipeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("recipeId");

                    b.ToTable("recipes");
                });

            modelBuilder.Entity("DBMSApi.Models.RecipeIngredient", b =>
                {
                    b.Property<int>("ingredientId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("recipeId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ingredientAmount")
                        .HasColumnType("REAL");

                    b.Property<string>("ingredientUnit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ingredientId", "recipeId");

                    b.HasIndex("recipeId");

                    b.ToTable("recipeIngredients");
                });

            modelBuilder.Entity("DBMSApi.Models.Roomate", b =>
                {
                    b.Property<int>("roomateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("fName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("houseId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("lName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("role")
                        .HasColumnType("INTEGER");

                    b.HasKey("roomateId");

                    b.HasIndex("houseId");

                    b.ToTable("roomates");
                });

            modelBuilder.Entity("DBMSApi.Models.RoomateIngredient", b =>
                {
                    b.Property<int>("roomateId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ingredientId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("expiredDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("price")
                        .HasColumnType("REAL");

                    b.Property<string>("priceUnit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("quantity")
                        .HasColumnType("REAL");

                    b.Property<string>("quantityUnit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("roomateId", "ingredientId");

                    b.HasIndex("ingredientId");

                    b.ToTable("roomateIngredients");
                });

            modelBuilder.Entity("DBMSApi.Models.RecipeIngredient", b =>
                {
                    b.HasOne("DBMSApi.Models.Ingredient", "ingredient")
                        .WithMany()
                        .HasForeignKey("ingredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DBMSApi.Models.Recipe", "recipe")
                        .WithMany("ingredients")
                        .HasForeignKey("recipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ingredient");

                    b.Navigation("recipe");
                });

            modelBuilder.Entity("DBMSApi.Models.Roomate", b =>
                {
                    b.HasOne("DBMSApi.Models.House", "house")
                        .WithMany()
                        .HasForeignKey("houseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("house");
                });

            modelBuilder.Entity("DBMSApi.Models.RoomateIngredient", b =>
                {
                    b.HasOne("DBMSApi.Models.Ingredient", "ingredient")
                        .WithMany()
                        .HasForeignKey("ingredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DBMSApi.Models.Roomate", "roomate")
                        .WithMany("ingredients")
                        .HasForeignKey("roomateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ingredient");

                    b.Navigation("roomate");
                });

            modelBuilder.Entity("DBMSApi.Models.Recipe", b =>
                {
                    b.Navigation("ingredients");
                });

            modelBuilder.Entity("DBMSApi.Models.Roomate", b =>
                {
                    b.Navigation("ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
