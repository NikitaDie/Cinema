using Cinema.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Storage.Contexts;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditorium> Auditoriums { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }
    
    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Pricelist> Pricelists { get; set; }

    public virtual DbSet<Seat> Seats { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auditoriums_pkey");

            entity.ToTable("auditoriums");

            entity.HasIndex(e => new { e.Id, e.BranchId }, "unique_id_branch").IsUnique();

            entity.HasIndex(e => new { e.Name, e.BranchId }, "unique_name_branch").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BranchId).HasColumnName("branch_id");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .HasColumnName("name");

            entity.HasOne(d => d.Branch).WithMany(p => p.Auditoriums)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_branch");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("branches_pkey");

            entity.ToTable("branches");

            entity.HasIndex(e => e.Name, "branches_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Region)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.ZipCode)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("zip_code");
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.HasIndex(e => e.Email, "clients_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("movies_pkey");

            entity.ToTable("movies");

            entity.HasIndex(e => e.ImagePath, "movies_image_path_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("title");

            entity.Property(e => e.OriginalTitle)
                .HasMaxLength(255)
                .IsRequired(false)
                .HasColumnName("original_title");

            entity.Property(e => e.AgeRating)
                .IsRequired()
                .HasColumnName("age_rating");

            entity.Property(e => e.ReleaseYear)
                .IsRequired() 
                .HasColumnName("release_year");

            entity.Property(e => e.Director)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("director");

            entity.Property(e => e.RentalPeriodStart)
                .HasColumnType("date")
                .IsRequired()
                .HasColumnName("rental_period_start");

            entity.Property(e => e.RentalPeriodEnd)
                .HasColumnType("date")
                .IsRequired() 
                .HasColumnName("rental_period_end");

            entity.Property(e => e.Language)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("language");

            entity.Property(e => e.Duration)
                .HasColumnType("interval")
                .IsRequired()
                .HasColumnName("duration");

            entity.Property(e => e.Producer)
                .HasMaxLength(255)
                .IsRequired(false)
                .HasColumnName("producer");

            entity.Property(e => e.ProductionStudio)
                .HasMaxLength(255)
                .IsRequired(false)
                .HasColumnName("production_studio");

            entity.Property(e => e.Screenplay)
                .HasMaxLength(255)
                .IsRequired(false)
                .HasColumnName("screenplay");

            entity.Property(e => e.InclusiveAdaptation)
                .HasMaxLength(255)
                .IsRequired(false)
                .HasColumnName("inclusive_adaptation");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired(false)
                .HasColumnName("description");

            entity.Property(e => e.TrailerLink)
                .HasMaxLength(500)
                .IsRequired()
                .HasColumnName("trailer_link");

            entity.Property(e => e.ImagePath)
                .HasMaxLength(500)
                .IsRequired()
                .HasColumnName("image_path");

            entity.HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .UsingEntity(
                    "MovieGenre",
                    l => l.HasOne(typeof(Genre))
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .HasPrincipalKey(nameof(Genre.Id)),
                    j => j.HasOne(typeof(Movie))
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .HasPrincipalKey(nameof(Movie.Id)),
                    j =>
                    {
                        j.HasKey("MovieId", "GenreId");
                        j.HasIndex("MovieId", "GenreId").IsUnique();
                    }
                );
            
            entity.HasMany(m => m.Starring)
                .WithMany(s => s.Movies)
                .UsingEntity(
                    "MovieActor",
                    l => l.HasOne(typeof(Actor))
                        .WithMany()
                        .HasForeignKey("ActorId")
                        .HasPrincipalKey(nameof(Actor.Id)),
                    j => j.HasOne(typeof(Movie))
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .HasPrincipalKey(nameof(Movie.Id)),
                    j =>
                    {
                        j.HasKey("MovieId", "ActorId");
                        j.HasIndex("MovieId", "ActorId").IsUnique();
                    }
                );
            
            entity.HasMany(m => m.Starring)
                .WithMany(g => g.Movies);

            entity.HasMany(e => e.Sessions)
                .WithOne(e => e.Movie)
                .HasForeignKey(e => e.MovieId);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genres_pkey");
            entity.ToTable("genres");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("name");
        });

        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("actors_pkey");
            entity.ToTable("actors");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsRequired()
                .HasColumnName("last_name");
        });
        
        modelBuilder.Entity<Pricelist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pricelist_pkey");

            entity.ToTable("pricelist");

            entity.HasIndex(e => new { e.SessionId, e.StatusId }, "unique_session_status").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Session).WithMany(p => p.Pricelists)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_session");

            entity.HasOne(d => d.Status).WithMany(p => p.Pricelists)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_status");
        });

        modelBuilder.Entity<Seat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("seats_pkey");

            entity.ToTable("seats");

            entity.HasIndex(e => new { e.Row, e.Column, e.AuditoriumId }, "unique_row_column").IsUnique();

            entity.HasIndex(e => new { e.XPosition, e.YPosition, e.AuditoriumId }, "unique_x_position_y_position").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AuditoriumId).HasColumnName("auditorium_id");
            entity.Property(e => e.Row).HasColumnName("row");
            entity.Property(e => e.Column).HasColumnName("column");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.XPosition).HasColumnName("x_position");
            entity.Property(e => e.YPosition).HasColumnName("y_position");

            entity.HasOne(d => d.Auditorium).WithMany(p => p.Seats)
                .HasForeignKey(d => d.AuditoriumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auditorium");

            entity.HasOne(d => d.Status).WithMany(p => p.Seats)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_status");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sessions_pkey");

            entity.ToTable("sessions");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AuditoriumId).HasColumnName("auditorium_id");
            entity.Property(e => e.EndTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("end_time");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.StartTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_time");

            entity.HasOne(d => d.Auditorium).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.AuditoriumId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_auditorium");

            entity.HasOne(d => d.Movie).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_movie");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("statuses_pkey");

            entity.ToTable("statuses");

            entity.HasIndex(e => e.Name, "statuses_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tickets_pkey");

            entity.ToTable("tickets");

            entity.HasIndex(e => new { e.SessionId, e.SeatId }, "unique_session_seat").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.SeatId).HasColumnName("seat_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Client).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_client");

            entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_seat");

            entity.HasOne(d => d.Session).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_session");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
