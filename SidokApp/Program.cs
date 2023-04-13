using SidokApp.Helpers;
using SidokApp.Repository;
using SidokApp.Repository.Interfaces;
using SidokApp.Services.Interfaces;
using SidokApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace SidokApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IPoliRepository, PoliRepository>();
            builder.Services.AddScoped<IPoliService, PoliService>();
            builder.Services.AddScoped<IDokterRepository, DokterRepository>();
            builder.Services.AddScoped<IDokterService, DokterService>();
            builder.Services.AddScoped<ISpesialisasiRepository, SpesialisasiRepository>();
            builder.Services.AddScoped<ISpesialisasiService, SpesialisasiService>();
            builder.Services.AddScoped<ISpesialisasiDokterRepository, SpesialisasiDokterRepository>();
            builder.Services.AddScoped<ISpesialisasiDokterService, SpesialisasiDokterService>();
            builder.Services.AddScoped<IJadwalJagaRepository, JadwalJagaRepository>();
            builder.Services.AddScoped<IJadwalJagaService, JadwalJagaService>();
            builder.Services.AddTransient<DapperContext>();

            // Get the scopes from the configuration (appsettings.json)
            IEnumerable<string>? initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ');


            builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration, "AzureAd")
                .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
                    .AddDownstreamWebApi("DownstreamApi", builder.Configuration.GetSection("DownstreamApi"))
                    .AddInMemoryTokenCaches();
            // </ms_docref_add_msal>

            // <ms_docref_add_default_controller_for_sign-in-out>
            builder.Services.AddRazorPages().AddMvcOptions(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                              .RequireAuthenticatedUser()
                              .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddMicrosoftIdentityUI();


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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Dokter}/{action=Index}/{id?}");

            app.Run();
        }
    }
}