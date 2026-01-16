using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using Microsoft.AspNetCore.Identity;
using VillaManagementWeb.Models;
// Alias cho Admin
using AdminInterfaces = VillaManagementWeb.Admin.Services.Interfaces;
using AdminImpl = VillaManagementWeb.Admin.Services.Implementations;
using AdminRepoInterfaces = VillaManagementWeb.Admin.Repositories.Interfaces;
using AdminRepoImpl = VillaManagementWeb.Admin.Repositories.Implementations;
// Alias cho Client (Nếu có trùng tên, hãy dùng alias để phân biệt)
using VillaManagementWeb.Services.Implementations;
using VillaManagementWeb.Services.Interfaces;
using VillaManagementWeb.Repositories.Interfaces; // Thêm cái này
using VillaManagementWeb.Repositories.Implementations; // Thêm cái này

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<VillaDbContext>(options => options.UseSqlServer(connectionString));

// 2. Cấu hình Identity (CHỈ GỌI 1 LẦN DUY NHẤT)
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

// 3. ĐĂNG KÝ DI CHO ADMIN AREA
builder.Services.AddScoped<AdminRepoInterfaces.IRoomsRepository, AdminRepoImpl.RoomsRepository>();
builder.Services.AddScoped<AdminInterfaces.IRoomsService, AdminImpl.RoomsService>();
builder.Services.AddScoped<AdminRepoInterfaces.IRoomBookingsRepository, AdminRepoImpl.RoomBookingsRepository>();
builder.Services.AddScoped<AdminInterfaces.IRoomBookingsService, AdminImpl.RoomBookingsService>();
builder.Services.AddScoped<AdminRepoInterfaces.IEventsRepository, AdminRepoImpl.EventsRepository>();
builder.Services.AddScoped<AdminInterfaces.IEventsService, AdminImpl.EventsService>();
builder.Services.AddScoped<AdminRepoInterfaces.ITicketsRepository, AdminRepoImpl.TicketsRepository>();
builder.Services.AddScoped<AdminInterfaces.ITicketsService, AdminImpl.TicketsService>();
builder.Services.AddScoped<AdminRepoInterfaces.IToursRepository, AdminRepoImpl.ToursRepository>();
builder.Services.AddScoped<AdminInterfaces.IToursService, AdminImpl.ToursService>();
builder.Services.AddScoped<AdminRepoInterfaces.ITourBookingsRepository, AdminRepoImpl.TourBookingsRepository>();
builder.Services.AddScoped<AdminInterfaces.ITourBookingsService, AdminImpl.TourBookingsService>();
builder.Services.AddScoped<AdminRepoInterfaces.INewsRepository, AdminRepoImpl.NewsRepository>();
builder.Services.AddScoped<AdminInterfaces.INewsService, AdminImpl.NewsService>();

// 4. ĐĂNG KÝ DI CHO CLIENT (MỞ KHÓA CÁC REPOSITORY BỊ THIẾU)
// Lưu ý: Đảm bảo các Class này tồn tại trong namespace VillaManagementWeb.Repositories
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<ITourRepository, TourRepository>();
builder.Services.AddScoped<ITourBookingRepository, TourBookingRepository>();
builder.Services.AddScoped<IEventsRepository, EventsRepository>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

// Đăng ký Client Services
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITourBookingService, TourBookingService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<LayoutService>();

// 5. Cấu hình Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Account/Login";
    options.LogoutPath = "/Admin/Account/Logout";
    options.AccessDeniedPath = "/Admin/Account/AccessDenied";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

builder.Services.Configure<FormOptions>(options => options.MultipartBodyLengthLimit = 104857600);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();

var app = builder.Build();

// 6. Seed Roles
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

// 7. Middleware
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

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();