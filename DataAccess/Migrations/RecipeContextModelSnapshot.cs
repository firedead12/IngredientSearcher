﻿// <auto-generated />
using System;
using IngredientSearcher.DataAccess;
using IngredientSearcher.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IngredientSearcher.DataAccess.Migrations
{
    [DbContext(typeof(RecipeContext))]
    partial class RecipeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Api")
                        .HasColumnType("text")
                        .HasColumnName("api");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("last_updated");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("ID")
                        .HasName("pk_providers");

                    b.ToTable("providers");
                });

            modelBuilder.Entity("IngredientSearcher.DataAccess.Model.Recipe", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<Ingredient[]>("Ingredients")
                        .HasColumnType("jsonb")
                        .HasColumnName("ingredients");

                    b.Property<int?>("ProviderID")
                        .HasColumnType("integer")
                        .HasColumnName("provider_id");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<string>("Url")
                        .HasColumnType("text")
                        .HasColumnName("url");

                    b.HasKey("ID")
                        .HasName("pk_recipes");

                    b.HasIndex("ProviderID")
                        .HasDatabaseName("ix_recipes_provider_id");

                    b.ToTable("recipes");
                });

            modelBuilder.Entity("IngredientSearcher.DataAccess.Model.Recipe", b =>
                {
                    b.HasOne("IngredientSearcher.DataAccess.Model.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderID")
                        .HasConstraintName("fk_recipes_providers_provider_id");

                    b.Navigation("Provider");
                });
#pragma warning restore 612, 618
        }
    }
}
