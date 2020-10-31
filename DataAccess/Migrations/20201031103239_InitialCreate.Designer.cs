﻿// <auto-generated />
using System;
using IngredientSearcher.DataAccess;
using IngredientSearcher.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IngredientSearcher.DataAccess.Migrations
{
    [DbContext(typeof(RecipeContext))]
    [Migration("20201031103239_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("IngredientSearcher.DataAccess.Model.Provider", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Api")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("IngredientSearcher.DataAccess.Model.Recipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<Ingredient[]>("Ingredients")
                        .HasColumnType("jsonb");

                    b.Property<int?>("ProviderID")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("ProviderID");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("IngredientSearcher.DataAccess.Model.Recipe", b =>
                {
                    b.HasOne("IngredientSearcher.DataAccess.Model.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderID");

                    b.Navigation("Provider");
                });
#pragma warning restore 612, 618
        }
    }
}