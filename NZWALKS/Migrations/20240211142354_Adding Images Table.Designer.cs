﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NZWALKS.DB;

#nullable disable

namespace NZWALKS.Migrations
{
    [DbContext(typeof(NzdbContext))]
    [Migration("20240211142354_Adding Images Table")]
    partial class AddingImagesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NZWALKS.Models.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a819730b-fe96-49f4-bf91-9d73e560f96d"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("d572fe86-b1b5-42c2-8f27-8f650b44bd4a"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("ad5f514d-15a9-42b6-b032-6e000b812ab2"),
                            Name = "Easy"
                        });
                });

            modelBuilder.Entity("NZWALKS.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FileDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint");

                    b.Property<string>("filePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("NZWALKS.Models.Regions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageURI")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("57b59cc8-2fe0-4308-8249-c4b18027a080"),
                            Code = "NL",
                            Name = "Northland",
                            RegionImageURI = "https://www.doc.govt.nz/globalassets/images/places/northland/northland.jpg"
                        },
                        new
                        {
                            Id = new Guid("94c2967b-8dad-46c7-847b-2b233c8d8800"),
                            Code = "AK",
                            Name = "Auckland",
                            RegionImageURI = "https://www.doc.govt.nz/globalassets/images/places/auckland/auckland.jpg"
                        },
                        new
                        {
                            Id = new Guid("f9331faf-6ac3-4bf8-a0bc-9132bd9f4766"),
                            Code = "WK",
                            Name = "Waikato",
                            RegionImageURI = "https://www.doc.govt.nz/globalassets/images/places/waikato/waikato.jpg"
                        });
                });

            modelBuilder.Entity("NZWALKS.Models.Walks", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImageURI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("regionsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("regionsId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("NZWALKS.Models.Walks", b =>
                {
                    b.HasOne("NZWALKS.Models.Difficulty", "difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NZWALKS.Models.Regions", "regions")
                        .WithMany()
                        .HasForeignKey("regionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("difficulty");

                    b.Navigation("regions");
                });
#pragma warning restore 612, 618
        }
    }
}
