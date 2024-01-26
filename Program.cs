using System.Text.Json.Serialization;
using firstapp.Data;
using firstapp.interfaces;
using firstapp.Repository;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// i have addSingleton: oneobject allong the app
//  and addTransient each time is requested it is made even if two times in on request 
// scoped create only one for each request 
builder.Services.AddScoped<iPokemonRepository ,PokemonRepository>() ;
builder.Services.AddScoped<iCategoryRepository ,CategoryRepository>() ;
builder.Services.AddScoped<iCountryRepository ,CountryRepository>() ;
builder.Services.AddScoped<iOwnerRepository ,OwnerRepository>() ;
builder.Services.AddScoped<iReviewRepository ,ReviewRepository>() ;
builder.Services.AddScoped<iReviewerRepository ,ReviewerRepository>() ;

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<Seed>();
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
)
;


var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
