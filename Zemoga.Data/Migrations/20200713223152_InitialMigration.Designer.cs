﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zemoga.Data.Context;

namespace Zemoga.Data.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20200713223152_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Zemoga.Domain.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentText");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email");

                    b.Property<int?>("PostId");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Zemoga.Domain.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorEmail");

                    b.Property<string>("AuthorName");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("PostText");

                    b.Property<string>("PostTitle");

                    b.Property<int>("Status");

                    b.HasKey("PostId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Zemoga.Domain.Models.Comment", b =>
                {
                    b.HasOne("Zemoga.Domain.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");
                });
#pragma warning restore 612, 618
        }
    }
}