using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class MovieShopDbContext: DbContext
    {
        // get the connection string into constructor
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options) 
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // specify fluent API rules for your entities
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(mg => new { mg.MovieId, mg.UserId });
            builder.Property(m => m.Rating).HasColumnType("decimal(3, 2)").HasDefaultValue(9.9m);


        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(mg => new { mg.UserId, mg.RoleId });
            builder.HasOne(m => m.User).WithMany(m => m.Roles).HasForeignKey(m => m.RoleId);
            builder.HasOne(g => g.Role).WithMany(g => g.Users).HasForeignKey(g => g.UserId);
        }


        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            builder.HasOne(m => m.Movie).WithMany(m => m.Genres).HasForeignKey(m => m.MovieId);
            builder.HasOne(g => g.Genre).WithMany(g => g.Movies).HasForeignKey(g => g.GenreId);

        }


        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mg => new { mg.MovieId, mg.CastId });
            builder.HasOne(m => m.Movie).WithMany(m => m.Casts).HasForeignKey(m => m.MovieId);
            builder.HasOne(g => g.Cast).WithMany(g => g.Movies).HasForeignKey(g => g.CastId);

        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(mg => new { mg.MovieId, mg.CrewId });
            builder.HasOne(m => m.Movie).WithMany(m => m.Crews).HasForeignKey(m => m.MovieId);
            builder.HasOne(g => g.Crew).WithMany(g => g.Movies).HasForeignKey(g => g.CrewId);

        }


        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);

        }

            private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

            // we wanna tell EF to ignore Rating property and not create the column
            builder.Ignore(m => m.Rating);
        }



        // make sure our entity classes are represented as DbSets
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
