using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using Microsoft.AspNetCore.Identity;
using VillaManagementWeb.Models;
using AdminInterfaces = VillaManagementWeb.Admin.Services.Interfaces;
using AdminImpl = VillaManagementWeb.Admin.Services.Implementations;
using AdminRepoInterfaces = VillaManagementWeb.Admin.Repositories.Interfaces;
using AdminRepoImpl = VillaManagementWeb.Admin.Repositories.Implementations;
using VillaManagementWeb.Services.Implementations;
using VillaManagementWeb.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<VillaDbContext>(options => options.UseSqlServer(connectionString));

// Đăng ký cho Room
builder.Services.AddScoped<AdminRepoInterfaces.IRoomsRepository, AdminRepoImpl.RoomsRepository>();
builder.Services.AddScoped<AdminInterfaces.IRoomsService, AdminImpl.RoomsService>();
// Đăng ký cho RoomBooking
builder.Services.AddScoped<AdminRepoInterfaces.IRoomBookingsRepository, AdminRepoImpl.RoomBookingsRepository>();
builder.Services.AddScoped<AdminInterfaces.IRoomBookingsService, AdminImpl.RoomBookingsService>();
// Đăng ký cho Events
builder.Services.AddScoped<AdminRepoInterfaces.IEventsRepository, AdminRepoImpl.EventsRepository>();
builder.Services.AddScoped<AdminInterfaces.IEventsService, AdminImpl.EventsService>();
// Đăng ký cho Tickets
builder.Services.AddScoped<AdminRepoInterfaces.ITicketsRepository, AdminRepoImpl.TicketsRepository>();
builder.Services.AddScoped<AdminInterfaces.ITicketsService, AdminImpl.TicketsService>();
// Đăng ký cho Tours 
builder.Services.AddScoped<AdminRepoInterfaces.IToursRepository, AdminRepoImpl.ToursRepository>();
builder.Services.AddScoped<AdminInterfaces.IToursService, AdminImpl.ToursService>();
// Đăng ký cho TourBookings 
builder.Services.AddScoped<AdminRepoInterfaces.ITourBookingsRepository, AdminRepoImpl.TourBookingsRepository>();
builder.Services.AddScoped<AdminInterfaces.ITourBookingsService, AdminImpl.TourBookingsService>();
// Đăng ký cho RoomImages
builder.Services.AddScoped<AdminRepoInterfaces.IRoomImagesRepository, AdminRepoImpl.RoomImagesRepository>();
builder.Services.AddScoped<AdminInterfaces.IRoomImagesService, AdminImpl.RoomImagesService>();
// Đăng ký cho News
builder.Services.AddScoped<AdminRepoInterfaces.INewsRepository, AdminRepoImpl.NewsRepository>();
builder.Services.AddScoped<AdminInterfaces.INewsService, AdminImpl.NewsService>();

// 2. Cấu hình Identity (Sử dụng class User của bạn)
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
})
.AddEntityFrameworkStores<VillaDbContext>()
.AddDefaultTokenProviders();

// Register Repositories
// builder.Services.AddScoped<IRoomRepository, RoomRepository>();
// builder.Services.AddScoped<IBookingRepository, BookingRepository>();
// builder.Services.AddScoped<IEventRepository, EventRepository>();
// builder.Services.AddScoped<INewsRepository, NewsRepository>();
// builder.Services.AddScoped<ITourRepository, TourRepository>();
// builder.Services.AddScoped<ITourBookingRepository, TourBookingRepository>();
// builder.Services.AddScoped<ITicketRepository, TicketRepository>();
// builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
// MVC + Runtime Compilation
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Register Services
// builder.Services.AddScoped<IRoomService, RoomService>();
// builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITourBookingService, TourBookingService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<LayoutService>();
// Identity
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<VillaDbContext>().AddDefaultTokenProviders();

// Cookie paths (vì Account nằm trong Admin Area)
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Account/Login";
    options.LogoutPath = "/Admin/Account/Logout";
    options.AccessDeniedPath = "/Admin/Account/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

// 4. Các dịch vụ khác
builder.Services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = 104857600);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages(); // Cần thiết cho Identity

var app = builder.Build();

// 5. Seed Roles tự động
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// 6. Middleware Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// 7. Route Mapping
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();