﻿// <auto-generated />
using System;
using Backend.DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.DataLayer.Migrations
{
    [DbContext(typeof(MainerWomanContext))]
    partial class MainerWomanContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backend.Core.DTOs.CoinDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CoinName")
                        .HasColumnType("text")
                        .HasColumnName("coin_name");

                    b.Property<int>("CoinType")
                        .HasColumnType("integer")
                        .HasColumnName("coin_type");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id");

                    b.Property<string>("Quantity")
                        .HasColumnType("text")
                        .HasColumnName("quantity");

                    b.HasKey("Id")
                        .HasName("pk_coins");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("ix_coins_owner_id");

                    b.ToTable("coins", (string)null);
                });

            modelBuilder.Entity("Backend.Core.DTOs.DeviceDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("DeviceName")
                        .HasColumnType("text")
                        .HasColumnName("device_name");

                    b.Property<int>("DeviceType")
                        .HasColumnType("integer")
                        .HasColumnName("device_type");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("owner_id");

                    b.HasKey("Id")
                        .HasName("pk_devices");

                    b.HasIndex("OwnerId")
                        .HasDatabaseName("ix_devices_owner_id");

                    b.ToTable("devices", (string)null);
                });

            modelBuilder.Entity("Backend.Core.DTOs.UserDto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Age")
                        .HasColumnType("integer")
                        .HasColumnName("age");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("text")
                        .HasColumnName("password_salt");

                    b.Property<string>("UserName")
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Backend.Core.DTOs.CoinDto", b =>
                {
                    b.HasOne("Backend.Core.DTOs.UserDto", "Owner")
                        .WithMany("Coins")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("fk_coins_users_owner_id");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Backend.Core.DTOs.DeviceDto", b =>
                {
                    b.HasOne("Backend.Core.DTOs.UserDto", "Owner")
                        .WithMany("Devices")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("fk_devices_users_owner_id");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Backend.Core.DTOs.UserDto", b =>
                {
                    b.Navigation("Coins");

                    b.Navigation("Devices");
                });
#pragma warning restore 612, 618
        }
    }
}
