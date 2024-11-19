﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GenAlgo.Migrations
{
    [DbContext(typeof(AlgoContext))]
    [Migration("20241118181643_AddNameToPopulation")]
    partial class AddNameToPopulation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("GenAlgo.Arrangement", b =>
                {
                    b.Property<int>("ArrangementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PopulationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ArrangementId");

                    b.HasIndex("PopulationId");

                    b.ToTable("Arrangements");
                });

            modelBuilder.Entity("GenAlgo.Square", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ArrangementId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Size")
                        .HasColumnType("INTEGER");

                    b.Property<int>("X")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Y")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ArrangementId");

                    b.ToTable("Square");
                });

            modelBuilder.Entity("Population", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Populations");
                });

            modelBuilder.Entity("GenAlgo.Arrangement", b =>
                {
                    b.HasOne("Population", "Population")
                        .WithMany("Arrs")
                        .HasForeignKey("PopulationId");

                    b.Navigation("Population");
                });

            modelBuilder.Entity("GenAlgo.Square", b =>
                {
                    b.HasOne("GenAlgo.Arrangement", null)
                        .WithMany("Lst")
                        .HasForeignKey("ArrangementId");
                });

            modelBuilder.Entity("GenAlgo.Arrangement", b =>
                {
                    b.Navigation("Lst");
                });

            modelBuilder.Entity("Population", b =>
                {
                    b.Navigation("Arrs");
                });
#pragma warning restore 612, 618
        }
    }
}
