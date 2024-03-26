﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyApp;

#nullable disable

namespace MyApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230609113501_folderchange")]
    partial class folderchange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("ModuleStudents", b =>
                {
                    b.Property<int>("modulesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("studentsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("modulesId", "studentsId");

                    b.HasIndex("studentsId");

                    b.ToTable("ModuleStudents");
                });

            modelBuilder.Entity("MyApp.Models.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Credit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ModuleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Dbmodules");
                });

            modelBuilder.Entity("MyApp.Models.Results", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Grade")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ModuleId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StudentsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ModuleId");

                    b.HasIndex("StudentsId");

                    b.ToTable("Dbresults");
                });

            modelBuilder.Entity("MyApp.Models.Students", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RegNo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TelNo")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Dbstudnets");
                });

            modelBuilder.Entity("MyApp.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Dbusers");
                });

            modelBuilder.Entity("ModuleStudents", b =>
                {
                    b.HasOne("MyApp.Models.Module", null)
                        .WithMany()
                        .HasForeignKey("modulesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyApp.Models.Students", null)
                        .WithMany()
                        .HasForeignKey("studentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyApp.Models.Results", b =>
                {
                    b.HasOne("MyApp.Models.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyApp.Models.Students", null)
                        .WithMany("results")
                        .HasForeignKey("StudentsId");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("MyApp.Models.Students", b =>
                {
                    b.Navigation("results");
                });
#pragma warning restore 612, 618
        }
    }
}
