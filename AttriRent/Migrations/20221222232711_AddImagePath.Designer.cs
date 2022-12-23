﻿// <auto-generated />
using System;
using AttriRent.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AttriRent.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221222232711_AddImagePath")]
    partial class AddImagePath
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AttriRent.Models.Attribute", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("image_path")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<int>("price")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("attributes");
                });

            modelBuilder.Entity("AttriRent.Models.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<bool>("activity_status")
                        .HasColumnType("boolean");

                    b.Property<int>("attribute_id")
                        .HasColumnType("integer");

                    b.Property<string>("description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("order_end_day")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("order_start_day")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("user_id")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("attribute_id");

                    b.HasIndex("user_id");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("AttriRent.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("user_role")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("AttriRent.Models.Order", b =>
                {
                    b.HasOne("AttriRent.Models.Attribute", "Attribute")
                        .WithMany("orders")
                        .HasForeignKey("attribute_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AttriRent.Models.User", "User")
                        .WithMany("orders")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AttriRent.Models.Attribute", b =>
                {
                    b.Navigation("orders");
                });

            modelBuilder.Entity("AttriRent.Models.User", b =>
                {
                    b.Navigation("orders");
                });
#pragma warning restore 612, 618
        }
    }
}