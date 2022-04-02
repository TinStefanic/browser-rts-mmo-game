﻿// <auto-generated />
using System;
using BrowserGame.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BrowserGame.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220331104929_AddedCityCoordinates")]
    partial class AddedCityCoordinates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BrowserGame.Models.BuildingSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("BuildingSlots");
                });

            modelBuilder.Entity("BrowserGame.Models.BuildQueue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BuildingType")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CompletionTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TargetId")
                        .HasColumnType("int");

                    b.Property<int?>("TargetLevel")
                        .HasColumnType("int");

                    b.Property<string>("TargetName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId")
                        .IsUnique();

                    b.ToTable("BuildQueues");
                });

            modelBuilder.Entity("BrowserGame.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BuildingSlotId")
                        .HasColumnType("int");

                    b.Property<int?>("ClayId")
                        .HasColumnType("int");

                    b.Property<int?>("CropId")
                        .HasColumnType("int");

                    b.Property<int?>("IronId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int?>("WoodId")
                        .HasColumnType("int");

                    b.Property<int>("XCoord")
                        .HasColumnType("int");

                    b.Property<int>("YCoord")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuildingSlotId");

                    b.HasIndex("ClayId");

                    b.HasIndex("CropId");

                    b.HasIndex("IronId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.HasIndex("WoodId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("BrowserGame.Models.CityBuilding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BuildingSlotId")
                        .HasColumnType("int");

                    b.Property<int>("CityBuildingType")
                        .HasColumnType("int");

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("CropUpkeep")
                        .HasColumnType("int");

                    b.Property<bool>("IsUpgradeInProgress")
                        .HasColumnType("bit");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BuildingSlotId");

                    b.HasIndex("CityId");

                    b.ToTable("CityBuildings");
                });

            modelBuilder.Entity("BrowserGame.Models.Clay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Available")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("LastUpdate")
                        .HasColumnType("bigint");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clays");
                });

            modelBuilder.Entity("BrowserGame.Models.Crop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Available")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("LastUpdate")
                        .HasColumnType("bigint");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UpkeepBuildings")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Crops");
                });

            modelBuilder.Entity("BrowserGame.Models.Iron", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Available")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("LastUpdate")
                        .HasColumnType("bigint");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Irons");
                });

            modelBuilder.Entity("BrowserGame.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("MessageBody")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<int>("RecipientId")
                        .HasColumnType("int");

                    b.Property<string>("RecipientName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<string>("SenderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Unread")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("BrowserGame.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ActiveCityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("UserId")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("BrowserGame.Models.ResourceField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<int?>("ClayId")
                        .HasColumnType("int");

                    b.Property<int?>("CropId")
                        .HasColumnType("int");

                    b.Property<int>("CropUpkeep")
                        .HasColumnType("int");

                    b.Property<int?>("IronId")
                        .HasColumnType("int");

                    b.Property<bool>("IsUpgradeInProgress")
                        .HasColumnType("bit");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("WoodId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("ClayId");

                    b.HasIndex("CropId");

                    b.HasIndex("IronId");

                    b.HasIndex("WoodId");

                    b.ToTable("ResourceFields");
                });

            modelBuilder.Entity("BrowserGame.Models.Upgrade", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("AdditionalCropUpkeep")
                        .HasColumnType("int");

                    b.Property<string>("BuildingName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClayCost")
                        .HasColumnType("int");

                    b.Property<int>("CropCost")
                        .HasColumnType("int");

                    b.Property<int>("IronCost")
                        .HasColumnType("int");

                    b.Property<bool>("IsFinnalUpgrade")
                        .HasColumnType("bit");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("UpgradeDuration")
                        .HasColumnType("int");

                    b.Property<decimal>("ValueChangeDecimal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("WoodCost")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Upgrades");
                });

            modelBuilder.Entity("BrowserGame.Models.Wood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Available")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("LastUpdate")
                        .HasColumnType("bigint");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Woods");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BrowserGame.Models.BuildQueue", b =>
                {
                    b.HasOne("BrowserGame.Models.City", "City")
                        .WithOne("BuildQueue")
                        .HasForeignKey("BrowserGame.Models.BuildQueue", "CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("BrowserGame.Models.City", b =>
                {
                    b.HasOne("BrowserGame.Models.BuildingSlot", "BuildingSlot")
                        .WithMany()
                        .HasForeignKey("BuildingSlotId");

                    b.HasOne("BrowserGame.Models.Clay", "Clay")
                        .WithMany()
                        .HasForeignKey("ClayId");

                    b.HasOne("BrowserGame.Models.Crop", "Crop")
                        .WithMany()
                        .HasForeignKey("CropId");

                    b.HasOne("BrowserGame.Models.Iron", "Iron")
                        .WithMany()
                        .HasForeignKey("IronId");

                    b.HasOne("BrowserGame.Models.Player", "Player")
                        .WithOne("Capital")
                        .HasForeignKey("BrowserGame.Models.City", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BrowserGame.Models.Wood", "Wood")
                        .WithMany()
                        .HasForeignKey("WoodId");

                    b.Navigation("BuildingSlot");

                    b.Navigation("Clay");

                    b.Navigation("Crop");

                    b.Navigation("Iron");

                    b.Navigation("Player");

                    b.Navigation("Wood");
                });

            modelBuilder.Entity("BrowserGame.Models.CityBuilding", b =>
                {
                    b.HasOne("BrowserGame.Models.BuildingSlot", null)
                        .WithMany("CityBuildings")
                        .HasForeignKey("BuildingSlotId");

                    b.HasOne("BrowserGame.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("BrowserGame.Models.ResourceField", b =>
                {
                    b.HasOne("BrowserGame.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("BrowserGame.Models.Clay", null)
                        .WithMany("Fields")
                        .HasForeignKey("ClayId");

                    b.HasOne("BrowserGame.Models.Crop", null)
                        .WithMany("Fields")
                        .HasForeignKey("CropId");

                    b.HasOne("BrowserGame.Models.Iron", null)
                        .WithMany("Fields")
                        .HasForeignKey("IronId");

                    b.HasOne("BrowserGame.Models.Wood", null)
                        .WithMany("Fields")
                        .HasForeignKey("WoodId");

                    b.Navigation("City");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BrowserGame.Models.BuildingSlot", b =>
                {
                    b.Navigation("CityBuildings");
                });

            modelBuilder.Entity("BrowserGame.Models.City", b =>
                {
                    b.Navigation("BuildQueue");
                });

            modelBuilder.Entity("BrowserGame.Models.Clay", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("BrowserGame.Models.Crop", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("BrowserGame.Models.Iron", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("BrowserGame.Models.Player", b =>
                {
                    b.Navigation("Capital");
                });

            modelBuilder.Entity("BrowserGame.Models.Wood", b =>
                {
                    b.Navigation("Fields");
                });
#pragma warning restore 612, 618
        }
    }
}
