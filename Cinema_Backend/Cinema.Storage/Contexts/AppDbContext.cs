using Cinema.Core.Models;
using Cinema.Core.Models.Helpers;
using Cinema.Storage.Utils;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .AddInterceptors(new SoftDeleteInterceptor());

    public virtual DbSet<Auditorium> Auditoriums { get; set; } = null!;

    public virtual DbSet<Branch> Branches { get; set; } = null!;

    public virtual DbSet<Client> Clients { get; set; } = null!;

    public virtual DbSet<Movie> Movies { get; set; } = null!;

    public virtual DbSet<Genre> Genres { get; set; } = null!;
    
    public virtual DbSet<Actor> Actors { get; set; } = null!;

    public virtual DbSet<Pricelist> Pricelists { get; set; } = null!;

    public virtual DbSet<Seat> Seats { get; set; } = null!;

    public virtual DbSet<Session> Sessions { get; set; } = null!;

    public virtual DbSet<Status> Statuses { get; set; } = null!;

    public virtual DbSet<Ticket> Tickets { get; set; } = null!;

    private static void ConfigureSoftDelete(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Check if the entity inherits from SoftDelete
            if (typeof(SoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType).Property(nameof(SoftDelete.IsDeleted)).HasDefaultValue(false);
                modelBuilder.Entity(entityType.ClrType).Property(nameof(SoftDelete.DeletedAt)).IsRequired(false);
            }
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureSoftDelete(modelBuilder);
        
        modelBuilder.Entity<Auditorium>().HasQueryFilter(e => e.IsDeleted == false);
        modelBuilder.Entity<Branch>().HasQueryFilter(e => e.IsDeleted == false);
        modelBuilder.Entity<Client>().HasQueryFilter(e => e.IsDeleted == false);
        modelBuilder.Entity<Movie>().HasQueryFilter(e => e.IsDeleted == false);
        modelBuilder.Entity<Genre>().HasQueryFilter(e => e.IsDeleted == false);
        modelBuilder.Entity<Actor>().HasQueryFilter(e => e.IsDeleted == false);
        modelBuilder.Entity<Pricelist>().HasQueryFilter(e => e.IsDeleted == false);
        modelBuilder.Entity<Seat>().HasQueryFilter(e => e.IsDeleted == false);
        modelBuilder.Entity<Session>().HasQueryFilter(e => e.IsDeleted == false);
        modelBuilder.Entity<Status>().HasQueryFilter(e => e.IsDeleted == false);
        modelBuilder.Entity<Ticket>().HasQueryFilter(e => e.IsDeleted == false);
        
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
                .HasConstraintName("fk_branch")
                .OnDelete(DeleteBehavior.Cascade);
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
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.HasIndex(e => e.Email, "clients_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("movies_pkey");

            entity.ToTable("movies");

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
                .HasForeignKey(e => e.MovieId)
                .OnDelete(DeleteBehavior.Cascade);;
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
                .HasConstraintName("fk_session")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Status).WithMany(p => p.Pricelists)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_status")
                .OnDelete(DeleteBehavior.Cascade);
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
                .HasConstraintName("fk_auditorium")
                .OnDelete(DeleteBehavior.Cascade);;

            entity.HasOne(d => d.Status).WithMany(p => p.Seats)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_status")
                .OnDelete(DeleteBehavior.Cascade);;
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
                .HasColumnType("timestamp with time zone")
                .HasColumnName("end_time");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.StartTime)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("start_time");

            entity.HasOne(d => d.Auditorium).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.AuditoriumId)
                .HasConstraintName("fk_auditorium")
                .OnDelete(DeleteBehavior.Cascade);;

            entity.HasOne(d => d.Movie).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("fk_movie")
                .OnDelete(DeleteBehavior.Cascade);;
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
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.SeatId).HasColumnName("seat_id");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Client).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("fk_client")
                .OnDelete(DeleteBehavior.Cascade);;

            entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SeatId)
                .HasConstraintName("fk_seat")
                .OnDelete(DeleteBehavior.Cascade);;

            entity.HasOne(d => d.Session).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("fk_session")
                .OnDelete(DeleteBehavior.Cascade);;
        });

        OnModelCreatingPartial(modelBuilder);
    }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
