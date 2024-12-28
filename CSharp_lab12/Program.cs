using Microsoft.EntityFrameworkCore;
using TourClass;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TourContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TourDatabase")));
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<TourContext>();
DbInitializer.Initialize(context);

app.Run();
public static class DbInitializer
{
    public static void Initialize(TourContext context)
    {
        context.Database.EnsureCreated();
        
        if (!context.Clients.Any() && !context.Tours.Any() && !context.Bookings.Any())
        {
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Clients', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Tours', RESEED, 0)");
            context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Bookings', RESEED, 0)");
            var clients = new[]
        {
            new Client {FirstName = "Анна", LastName = "Смирнова", Description = "Любитель культурных путешествий и экскурсий по музеям Европы." },
            new Client {FirstName = "Иван", LastName = "Петров", Description = "Поклонник активного отдыха и горнолыжных курортов." },
            new Client {FirstName = "Мария", LastName = "Иванова", Description = "Предпочитает пляжные курорты и семейные туры на море." },
            new Client {FirstName = "Сергей", LastName = "Кузнецов", Description = "Увлекается экотуризмом и походами в дикой природе." },
            new Client {FirstName = "Ольга", LastName = "Васильева", Description = "Интересуется гастрономическими турами и дегустацией местной кухни." }
        };
            context.Clients.AddRange(clients);
            var tours = new[]
            {
            new Tour { Destination = "Париж, Франция", Price = 1200.0, Description = "Романтический тур с посещением Эйфелевой башни и Лувра." },
            new Tour { Destination = "Мальдивы", Price = 2500.0, Description = "Идеальный пляжный отдых на белоснежных песках Индийского океана." },
            new Tour { Destination = "Альпы, Швейцария", Price = 1800.0, Description = "Горнолыжный тур с потрясающими видами и уютными шале." },
            new Tour { Destination = "Киото, Япония", Price = 1500.0, Description = "Культурный тур с посещением храмов, садов и традиционных чайных церемоний." },
            new Tour { Destination = "Барселона, Испания", Price = 1300.0, Description = "Экскурсионный тур с изучением архитектуры Гауди и средиземноморской кухни." }
        };
            context.Tours.AddRange(tours);
            context.SaveChanges();
            var bookings = new[]
            {
            new Booking { ClientId = 1, TourId = 1, NumberOfPeople = 2, Description = "Романтическое путешествие на двоих в Париж." },
            new Booking { ClientId = 2, TourId = 3, NumberOfPeople = 4, Description = "Семейный горнолыжный отпуск в Альпах." },
            new Booking { ClientId = 3, TourId = 2, NumberOfPeople = 1, Description = "Уединенное путешествие для отдыха на Мальдивах." },
            new Booking { ClientId = 4, TourId = 4, NumberOfPeople = 3, Description = "Тур для группы друзей с культурными достопримечательностями Киото." },
            new Booking { ClientId = 5, TourId = 5, NumberOfPeople = 5, Description = "Большая группа туристов изучает Барселону." }
        };
            context.Bookings.AddRange(bookings);
            context.SaveChanges();
        }
        
    }
}