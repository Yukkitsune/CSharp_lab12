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
            new Client {FirstName = "����", LastName = "��������", Description = "�������� ���������� ����������� � ��������� �� ������ ������." },
            new Client {FirstName = "����", LastName = "������", Description = "��������� ��������� ������ � ����������� ��������." },
            new Client {FirstName = "�����", LastName = "�������", Description = "������������ ������� ������� � �������� ���� �� ����." },
            new Client {FirstName = "������", LastName = "��������", Description = "���������� ����������� � �������� � ����� �������." },
            new Client {FirstName = "�����", LastName = "���������", Description = "������������ ����������������� ������ � ����������� ������� �����." }
        };
            context.Clients.AddRange(clients);
            var tours = new[]
            {
            new Tour { Destination = "�����, �������", Price = 1200.0, Description = "������������� ��� � ���������� ��������� ����� � �����." },
            new Tour { Destination = "��������", Price = 2500.0, Description = "��������� ������� ����� �� ����������� ������ ���������� ������." },
            new Tour { Destination = "�����, ���������", Price = 1800.0, Description = "����������� ��� � ������������ ������ � ������� ����." },
            new Tour { Destination = "�����, ������", Price = 1500.0, Description = "���������� ��� � ���������� ������, ����� � ������������ ������ ���������." },
            new Tour { Destination = "���������, �������", Price = 1300.0, Description = "������������� ��� � ��������� ����������� ����� � ����������������� �����." }
        };
            context.Tours.AddRange(tours);
            context.SaveChanges();
            var bookings = new[]
            {
            new Booking { ClientId = 1, TourId = 1, NumberOfPeople = 2, Description = "������������� ����������� �� ����� � �����." },
            new Booking { ClientId = 2, TourId = 3, NumberOfPeople = 4, Description = "�������� ����������� ������ � ������." },
            new Booking { ClientId = 3, TourId = 2, NumberOfPeople = 1, Description = "���������� ����������� ��� ������ �� ���������." },
            new Booking { ClientId = 4, TourId = 4, NumberOfPeople = 3, Description = "��� ��� ������ ������ � ����������� ����������������������� �����." },
            new Booking { ClientId = 5, TourId = 5, NumberOfPeople = 5, Description = "������� ������ �������� ������� ���������." }
        };
            context.Bookings.AddRange(bookings);
            context.SaveChanges();
        }
        
    }
}