using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieDemo.Data;
using MovieDemo.Services;

namespace MovieDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //sfsfsf
            //sfsfsf

            // Add services to the container.
            builder.Services.AddDbContext<MovieDemoContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDemoContext") ?? throw new InvalidOperationException("Connection string 'MovieDemoContext' not found.")));
           
            builder.Services.AddControllersWithViews();

            //builder.Services.AddSingleton  samma instans för hela applikations livslängd
            //builder.Services.AddScoped  samma instans för hela requestet
            //builder.Services.AddTransient  samma instans varje gång någon vill ha servicen


            builder.Services.AddScoped<IGenreSelectListService, GenreSelectListService>();




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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movies}/{action=Index}/{id?}");

            app.Run();
        }
    }
}