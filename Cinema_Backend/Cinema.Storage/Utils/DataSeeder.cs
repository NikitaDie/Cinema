using Bogus;
using Cinema.Core.Models;
using Cinema.Storage.Contexts;

namespace Cinema.Storage.Utils;

public class DataSeeder
{
    private readonly AppDbContext _context;

    public DataSeeder(AppDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (_context.Branches.Any()) return; // Skip if data already exists

        // 1. Seed Statuses
        var statuses = GenerateStatuses();
        await _context.Statuses.AddRangeAsync(statuses);

        // 2. Seed Branches and Auditoriums
        var branches = GenerateBranches(5);
        await _context.Branches.AddRangeAsync(branches);

        var auditoriums = GenerateAuditoriums(branches);
        await _context.Auditoriums.AddRangeAsync(auditoriums);

        // 3. Seed Seats
        var seats = GenerateSeats(auditoriums, statuses, 10, 10); // 10 rows, 10 columns
        await _context.Seats.AddRangeAsync(seats);

        // 4. Seed Genres and Actors
        var genres = GenerateGenres();
        await _context.Genres.AddRangeAsync(genres);

        var actors = GenerateActors(50);
        await _context.Actors.AddRangeAsync(actors);

        // 5. Seed Movies
        var movies = GenerateMovies(20, genres, actors);
        await _context.Movies.AddRangeAsync(movies);

        // 6. Seed Clients
        var clients = GenerateClients(50);
        await _context.Clients.AddRangeAsync(clients);

        // 7. Seed Sessions
        var sessions = GenerateSessions(auditoriums, movies);
        await _context.Sessions.AddRangeAsync(sessions);

        // 8. Seed Tickets
        var tickets = GenerateTickets(sessions, seats, clients);
        await _context.Tickets.AddRangeAsync(tickets);

        // 9. Seed PriceList
        var priceList = GeneratePriceList(sessions, statuses);
        await _context.Pricelists.AddRangeAsync(priceList);
        
        await _context.SaveChangesAsync();
    }

    private List<Status> GenerateStatuses()
    {
        return new List<Status>
        {
            new Status { Id = Guid.NewGuid(), Name = "GOOD", IsDeleted = false },
            new Status { Id = Guid.NewGuid(), Name = "SUPER LUX", IsDeleted = false }
        };
    }

    private List<Branch> GenerateBranches(int count)
    {
        var faker = new Faker<Branch>()
            .RuleFor(b => b.Id, _ => Guid.NewGuid())
            .RuleFor(b => b.Name, f => f.Company.CompanyName())
            .RuleFor(b => b.City, f => f.Address.City())
            .RuleFor(b => b.Address, f => f.Address.FullAddress())
            .RuleFor(b => b.Region, f => f.Address.State())
            .RuleFor(b => b.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(b => b.PhoneNumber, f => f.Random.Replace("+380 (0##) ###-####"))
            .RuleFor(b => b.ZipCode, f => f.Address.ZipCode())
            .RuleFor(b => b.IsDeleted, _ => false);

        return faker.Generate(count);
    }

    private List<Auditorium> GenerateAuditoriums(List<Branch> branches)
    {
        var faker = new Faker<Auditorium>()
            .RuleFor(a => a.Id, _ => Guid.NewGuid())
            .RuleFor(a => a.Name, f => f.Random.Word())
            .RuleFor(a => a.BranchId, f => f.PickRandom(branches).Id)
            .RuleFor(a => a.IsDeleted, _ => false);

        return faker.Generate(branches.Count * 3); // 3 auditoriums per branch
    }

    private List<Seat> GenerateSeats(List<Auditorium> auditoriums, List<Status> statuses, int rows, int columns)
    {
        var faker = new Faker();
        
        var seats = new List<Seat>();
        foreach (var auditorium in auditoriums)
        {
            for (short row = 1; row <= rows; row++)
            {
                for (short col = 1; col <= columns; col++)
                {
                    seats.Add(new Seat
                    {
                        Id = Guid.NewGuid(),
                        AuditoriumId = auditorium.Id,
                        Row = row,
                        Column = col,
                        XPosition = col,
                        YPosition = row,
                        Status = faker.PickRandom(statuses),
                        IsDeleted = false
                    });
                }
            }
        }
        return seats;
    }

    private List<Genre> GenerateGenres()
    {
        var genres = new[] { "Action", "Drama", "Comedy", "Horror", "Sci-Fi", "Romance" };
        return genres.Select(g => new Genre { Id = Guid.NewGuid(), Name = g }).ToList();
    }

    private List<Actor> GenerateActors(int count)
    {
        var faker = new Faker<Actor>()
            .RuleFor(a => a.Id, _ => Guid.NewGuid())
            .RuleFor(a => a.FirstName, f => f.Name.FirstName())
            .RuleFor(a => a.LastName, f => f.Name.LastName())
            .RuleFor(a => a.IsDeleted, _ => false);

        return faker.Generate(count);
    }

    private List<Movie> GenerateMovies(int count, List<Genre> genres, List<Actor> actors)
    {
        var imageFileNames = new List<string>
        {
            "02db5b123ecf200d3d6f35df1bd66e6f.jpeg",
            "6c69e77756cf8261cbda503e7e2dbae7.jpeg",
            "6d18c2bf0188731121fba6784b590d7c.jpeg",
            "6e0c03d6574c1895c2cc67d96241f730.jpeg",
            "6ee38113f22251699bce44770b575bd0.jpeg",
            "20c129219156ab536bb4c96cfbb93897.jpeg",
            "22fa8b2d2796679fcaa6a76528c345f0.jpeg",
            "25fb77b0167b54d1d2705d371618b728.jpeg",
            "84e2e3f2c8e33bd3ac8b7b82b6f5cc27.jpeg",
            "44756e0d8390325ab6eb9e664baca892.jpeg",
            "993463442cc688edc5fb3beb1e5ba346.jpeg",
            "a2e26c3b7381dba430f1bbb71ee35c83.jpeg",
            "aabe4ffd4f4a1651210665df6b412709.jpeg",
            "b9d587731a9926f3b365da015a75ade4.jpeg",
            "bd7c63ee437829acf1c520d07bfbb0f2.jpeg",
            "c474f390f286c61792c802b3403ae3d7.jpeg",
            "c74704a8890c60d723e2569bf2e771c2.jpeg",
            "c24729081c7a4f0821893d83523461e7.jpeg",
            "dbd8bbef919a7ac1aae47cbe8ad2b1ce.jpeg",
            "f3e638a237e295f019f1da2c713113a6.jpeg",
            "f29080442cdfd88cf698b71c337bc947.jpeg"
        };
        
        var faker = new Faker<Movie>()
            .RuleFor(m => m.Id, _ => Guid.NewGuid())
            .RuleFor(m => m.Title, f => f.Lorem.Sentence(3))
            .RuleFor(m => m.Director, f => f.Name.FullName())
            .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
            .RuleFor(m => m.ReleaseYear, f => f.Date.Past(20).Year)
            .RuleFor(m => m.Genres, f => f.PickRandom(genres, 2).ToList())
            .RuleFor(m => m.Starring, f => f.PickRandom(actors, 3).ToList())
            .RuleFor(m => m.Language, f => f.PickRandom("English", "Ukraine", "French", "German"))
            .RuleFor(m => m.ImagePath, f => f.PickRandom(imageFileNames))
            .RuleFor(m => m.RentalPeriodStart, f => DateOnly.FromDateTime(f.Date.Future()))
            .RuleFor(m => m.RentalPeriodEnd, (_, m) => m.RentalPeriodStart.AddMonths(1))
            .RuleFor(b => b.TrailerLink, f => f.Internet.Url())
            .RuleFor(m => m.IsDeleted, _ => false);

        return faker.Generate(count);
    }

    private List<Client> GenerateClients(int count)
    {
        var faker = new Faker<Client>()
            .RuleFor(c => c.Id, _ => Guid.NewGuid())
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.IsDeleted, _ => false);

        return faker.Generate(count);
    }

    private List<Session> GenerateSessions(List<Auditorium> auditoriums, List<Movie> movies)
    {
        var faker = new Faker<Session>()
            .RuleFor(s => s.Id, _ => Guid.NewGuid())
            .RuleFor(s => s.AuditoriumId, f => f.PickRandom(auditoriums).Id)
            .RuleFor(s => s.MovieId, f => f.PickRandom(movies).Id)
            .RuleFor(s => s.StartTime, f => f.Date.Future().ToUniversalTime())
            .RuleFor(s => s.EndTime, (_, s) => s.StartTime.AddHours(2).ToUniversalTime())
            .RuleFor(s => s.IsDeleted, _ => false);

        return faker.Generate(auditoriums.Count * 2); // 2 sessions per auditorium
    }

    private List<Ticket> GenerateTickets(List<Session> sessions, List<Seat> seats, List<Client> clients)
    {
        var faker = new Faker<Ticket>()
            .RuleFor(t => t.Id, _ => Guid.NewGuid())
            .RuleFor(t => t.ClientId, f => f.PickRandom(clients).Id)
            .RuleFor(t => t.CreatedAt, _ => DateTime.UtcNow)
            .RuleFor(t => t.UpdatedAt, _ => DateTime.UtcNow)
            .RuleFor(t => t.IsDeleted, _ => false);

        // Generate all possible unique combinations of session and seat
        var sessionSeatCombinations = sessions
            .SelectMany(session => seats, (session, seat) => new { SessionId = session.Id, SeatId = seat.Id })
            .ToList();

        // Shuffle the session-seat combinations to randomize them
        var random = new Random();
        sessionSeatCombinations = sessionSeatCombinations.OrderBy(x => random.Next()).ToList();

        // Generate the tickets
        var tickets = new List<Ticket>();

        for (var i = 0; i < 500; i++) // Generate 500 tickets
        {
            var sessionSeat = sessionSeatCombinations[i % sessionSeatCombinations.Count];

            var ticket = faker.Generate();
            ticket.SessionId = sessionSeat.SessionId; // Assign unique session
            ticket.SeatId = sessionSeat.SeatId; // Assign unique seat

            tickets.Add(ticket);
        }

        return tickets;
    }
    
    private IEnumerable<Pricelist> GeneratePriceList(IEnumerable<Session> sessions, IReadOnlyCollection<Status> statuses)
    {
        var faker = new Faker();

        return sessions
            .SelectMany(session => statuses, (session, status) => new Pricelist
            {
                Id = Guid.NewGuid(),
                SessionId = session.Id,
                StatusId = status.Id,
                Price = faker.Random.Decimal(10, 50),
                IsDeleted = false
            })
            .ToList();
    }
}