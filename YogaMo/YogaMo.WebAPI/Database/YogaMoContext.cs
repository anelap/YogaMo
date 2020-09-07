using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace YogaMo.WebAPI.Database
{
    public partial class YogaMoContext : DbContext
    {
        public YogaMoContext()
        {
        }

        public YogaMoContext(DbContextOptions<YogaMoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Instructor> Instructor { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Yoga> Yoga { get; set; }
        public virtual DbSet<YogaClass> YogaClass { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=YogaMo;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Client__A9D105347D71BC7F")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Client__536C85E4AF949E7A")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_ClientCity");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Instruct__A9D10534419B78CA")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Instruct__536C85E4C9239B78")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Instructor)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_InstructorCity");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order_");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.TotalPrice).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_OrderClient");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.OrderStatusId)
                    .HasConstraintName("FK_OrderOrderStatusId");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(5, 2)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderItemOrder");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItem)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_OrderItemProduct");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_ProductCategory");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.Rating1).HasColumnName("Rating");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_RatingClient");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Rating)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_RatingProduct");
            });

            modelBuilder.Entity<Yoga>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.Yoga)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_YogaInstructor");
            });

            modelBuilder.Entity<YogaClass>(entity =>
            {
                entity.Property(e => e.Day)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Time)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Yoga)
                    .WithMany(p => p.YogaClass)
                    .HasForeignKey(d => d.YogaId)
                    .HasConstraintName("FK_YogaClassYoga");
            });
        }
    }
}
