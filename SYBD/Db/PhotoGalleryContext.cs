using Microsoft.EntityFrameworkCore;
using SYBD.Db.Models;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SYBD.Db
{
    public partial class PhotoGalleryContext : DbContext
    {
        public PhotoGalleryContext()
        {
        }

        public PhotoGalleryContext(DbContextOptions<PhotoGalleryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Income> Income { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Photographer> Photographer { get; set; }
        public virtual DbSet<Repository> Repository { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=192.168.1.14;Port=5432;Database=PhotoGallery;Username=postgres;Password=65282174389");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.HasComment("Таблица содержащая информацию о жанрах");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("Первичный ключ");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'Неизвестный жанр'::character varying")
                    .HasComment("Название жанра");
            });

            modelBuilder.Entity<Income>(entity =>
            {
                entity.ToTable("income");

                entity.HasComment("Таблица хранящая в себе информацию о  поступлении фоторафий");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("Первичный ключ");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasComment("Количество фотографий");

                entity.Property(e => e.Incomedate)
                    .HasColumnName("incomedate")
                    .HasColumnType("date")
                    .HasComment("Дата поступления");

                entity.Property(e => e.Photoid)
                    .HasColumnName("photoid")
                    .HasComment("Внешний ключ на запись в таблице photo");

                entity.Property(e => e.Stockid)
                    .HasColumnName("stockid")
                    .HasComment("Внешний ключ на запись в таблице stock");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Income)
                    .HasForeignKey(d => d.Photoid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("photo_fk");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.Income)
                    .HasForeignKey(d => d.Stockid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("stock_fk");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("photo");

                entity.HasComment("Таблица хранящая в себе информацию о фотографии");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("Первичный ключ");

                entity.Property(e => e.Genreid)
                    .HasColumnName("genreid")
                    .HasComment("Внешний ключ на запись в таблице genre");

                entity.Property(e => e.Photodate)
                    .HasColumnName("photodate")
                    .HasColumnType("date")
                    .HasDefaultValueSql("now()")
                    .HasComment("дата создания фотографии");

                entity.Property(e => e.Photographerid)
                    .HasColumnName("photographerid")
                    .HasComment("Внешний ключ на запись в таблице photographer");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(19,2)")
                    .HasComment("стоимость фотографии");

                entity.Property(e => e.Quality)
                    .IsRequired()
                    .HasColumnName("quality")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'Информация о качестве отсутствует'::character varying")
                    .HasComment("качество фотографии");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasComment("рейтинг фотографии");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Photo)
                    .HasForeignKey(d => d.Genreid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("genre_fk");

                entity.HasOne(d => d.Photographer)
                    .WithMany(p => p.Photo)
                    .HasForeignKey(d => d.Photographerid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("phototographer_fk");
            });

            modelBuilder.Entity<Photographer>(entity =>
            {
                entity.ToTable("photographer");

                entity.HasComment("Таблица хранящая в себе информацию о фотографе");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("Первичный ключ");

                entity.Property(e => e.Age)
                    .HasColumnName("age")
                    .HasComment("Возраст фотографа");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'Неизвестное имя'::character varying")
                    .HasComment("Имя фотографа");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("'Неизвестный статус'::character varying")
                    .HasComment("Статус фотографа");
            });

            modelBuilder.Entity<Repository>(entity =>
            {
                entity.ToTable("repository");

                entity.HasComment("Вспомогательная сущность для реализации связи многий ко многим таблиц stock и photo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("Первичный ключ");

                entity.Property(e => e.Count)
                    .HasColumnName("count")
                    .HasComment("Количество фотографий");

                entity.Property(e => e.Photoid)
                    .HasColumnName("photoid")
                    .HasComment(@"Внешний ключ на запись в таблице photo
");

                entity.Property(e => e.Stockid)
                    .HasColumnName("stockid")
                    .HasComment("Внешний ключ на запись в таблице stock");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Repository)
                    .HasForeignKey(d => d.Photoid)
                    .HasConstraintName("photo_fk");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.Repository)
                    .HasForeignKey(d => d.Stockid)
                    .HasConstraintName("stock_fk");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("sale");

                entity.HasComment("Таблица хранящая в себе информацию о продаже фоторафий");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("Первичный ключ");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.Photoid)
                    .HasColumnName("photoid")
                    .HasComment("Внешний ключ на запись в таблице photo");

                entity.Property(e => e.Solddate)
                    .HasColumnName("solddate")
                    .HasColumnType("date")
                    .HasComment("Дата продажи");

                entity.Property(e => e.Stockid)
                    .HasColumnName("stockid")
                    .HasComment("Внешний ключ на запись в таблице stock");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Sale)
                    .HasForeignKey(d => d.Photoid)
                    .HasConstraintName("photo_fk");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.Sale)
                    .HasForeignKey(d => d.Stockid)
                    .HasConstraintName("stock_fk");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("stock");

                entity.HasComment("Таблица содержащая номера складов");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasComment("Первичный ключ");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("Address")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("'Неизвестный адрес'::character varying")
                    .HasComment("Адрес склада");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
