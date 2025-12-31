using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using VillaManagementWeb.Data;
using VillaManagementWeb.Repositories.Interfaces;
using VillaManagementWeb.Repositories.Implementations;
using VillaManagementWeb.Services.Interfaces;
using VillaManagementWeb.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Kiểm tra nếu quên chưa cấu hình ConnectionString trong appsettings.json
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

builder.Services.AddDbContext<VillaDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.Configure<FormOptions>(options => {
    options.MultipartBodyLengthLimit = 104857600; 
});

// Register Repositories
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();

// Register Services
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<INewsService, NewsService>();

// 2. Đăng ký các dịch vụ MVC
builder.Services.AddControllersWithViews();

// Thêm cái này nếu bạn muốn các thay đổi ở View tự cập nhật mà không cần restart lại server (cần cài thêm NuGet Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation)
 builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// 3. Pipeline xử lý Request
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Trong môi trường Dev, nên hiện lỗi chi tiết để nhóm dễ fix bug
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Cho phép truy cập ảnh, css, js trong wwwroot

app.UseRouting();

app.UseAuthorization(); // Xác thực người dùng 
// Route cho Areas (Admin)
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);
// Route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();