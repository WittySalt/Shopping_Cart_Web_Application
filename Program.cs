using Shopping_Cart_Web_Application_V1._0.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	var conn_str = builder.Configuration.GetConnectionString("conn_str");
	options.UseLazyLoadingProxies().UseSqlServer(conn_str);
});

// Add services to use sessions
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Middleware to track if user is logged in
//app.UseMiddleware<TrackLoginMiddleware>();

//Enabling session
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

InitDB(app.Services);

app.Run();

void InitDB(IServiceProvider serviceProvider)
{
	using var scope = serviceProvider.CreateScope();
	ApplicationDbContext db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
	db.Database.EnsureCreated();
	//db.Database.EnsureDeleted();
}