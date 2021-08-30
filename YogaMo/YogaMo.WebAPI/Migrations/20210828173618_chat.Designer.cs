﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YogaMo.WebAPI.Database;

namespace YogaMo.WebAPI.Migrations
{
    [DbContext(typeof(_150222Context))]
    [Migration("20210828173618_chat")]
    partial class chat
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("YogaMo.WebAPI.Database.Administrator", b =>
                {
                    b.Property<int>("AdministratorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdministratorId");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.AuthorizationToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AdministratorId")
                        .HasColumnType("int");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InstructorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AdministratorId");

                    b.HasIndex("ClientId");

                    b.HasIndex("InstructorId");

                    b.ToTable("AuthorizationToken");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.ChatInstructorsClients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastMessage")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("InstructorId");

                    b.ToTable("ChatInstructorsClients");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ClientId");

                    b.HasIndex("CityId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("UQ__Client__A9D105342A2F607D");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasName("UQ__Client__536C85E498366873");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("CountryId");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Instructor", b =>
                {
                    b.Property<int>("InstructorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("InstructorId");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("UQ__Instruct__A9D10534730A8E14");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasName("UQ__Instruct__536C85E4BCC82888");

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("OrderId");

                    b.HasIndex("ClientId");

                    b.ToTable("Order_");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("QuantityStock")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Rating1")
                        .HasColumnName("Rating")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProductId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Yoga", b =>
                {
                    b.Property<int>("YogaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("YogaId");

                    b.ToTable("Yoga");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.YogaClass", b =>
                {
                    b.Property<int>("YogaClassId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Day")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("InstructorId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("TimeFrom")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TimeTo")
                        .HasColumnType("time");

                    b.Property<int?>("YogaHallId")
                        .HasColumnType("int");

                    b.Property<int?>("YogaId")
                        .HasColumnType("int");

                    b.HasKey("YogaClassId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("YogaHallId");

                    b.HasIndex("YogaId");

                    b.ToTable("YogaClass");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.YogaHall", b =>
                {
                    b.Property<int>("YogaHallId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("YogaHallId");

                    b.ToTable("YogaHall");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.YogaPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PhotoFileName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("YogaPhoto");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.YogaVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ThumbnailFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YoutubeID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("YogaVideo");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.AuthorizationToken", b =>
                {
                    b.HasOne("YogaMo.WebAPI.Database.Administrator", "Administrator")
                        .WithMany()
                        .HasForeignKey("AdministratorId");

                    b.HasOne("YogaMo.WebAPI.Database.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("YogaMo.WebAPI.Database.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.ChatInstructorsClients", b =>
                {
                    b.HasOne("YogaMo.WebAPI.Database.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YogaMo.WebAPI.Database.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.City", b =>
                {
                    b.HasOne("YogaMo.WebAPI.Database.Country", "Country")
                        .WithMany("City")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK_CountryCity");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Client", b =>
                {
                    b.HasOne("YogaMo.WebAPI.Database.City", "City")
                        .WithMany("Client")
                        .HasForeignKey("CityId")
                        .HasConstraintName("FK_ClientCity");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Order", b =>
                {
                    b.HasOne("YogaMo.WebAPI.Database.Client", "Client")
                        .WithMany("Order")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_OrderClient");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.OrderItem", b =>
                {
                    b.HasOne("YogaMo.WebAPI.Database.Order", "Order")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_OrderItemOrder");

                    b.HasOne("YogaMo.WebAPI.Database.Product", "Product")
                        .WithMany("OrderItem")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_OrderItemProduct");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Product", b =>
                {
                    b.HasOne("YogaMo.WebAPI.Database.Category", "Category")
                        .WithMany("Product")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_ProductCategory");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.Rating", b =>
                {
                    b.HasOne("YogaMo.WebAPI.Database.Client", "Client")
                        .WithMany("Rating")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_RatingClient");

                    b.HasOne("YogaMo.WebAPI.Database.Product", "Product")
                        .WithMany("Rating")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_RatingProduct");
                });

            modelBuilder.Entity("YogaMo.WebAPI.Database.YogaClass", b =>
                {
                    b.HasOne("YogaMo.WebAPI.Database.Instructor", "Instructor")
                        .WithMany("YogaClass")
                        .HasForeignKey("InstructorId")
                        .HasConstraintName("FK_YogaClassInstructor");

                    b.HasOne("YogaMo.WebAPI.Database.YogaHall", "YogaHall")
                        .WithMany()
                        .HasForeignKey("YogaHallId");

                    b.HasOne("YogaMo.WebAPI.Database.Yoga", "Yoga")
                        .WithMany("YogaClass")
                        .HasForeignKey("YogaId")
                        .HasConstraintName("FK_YogaClassYoga");
                });
#pragma warning restore 612, 618
        }
    }
}
