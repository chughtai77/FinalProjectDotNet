using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Rules;
using Restaurant.DataAccess;
using Restaurant.DataAccess.Repository;
using Restaurant.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Restaurant.Utility;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace RestaurantWeb
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			//---connection string with db context class in container
			builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
				builder.Configuration.GetConnectionString("DefaultConnection")
				));

			builder.Services.AddIdentity<IdentityUser , IdentityRole>().AddDefaultTokenProviders()
			 .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddSingleton<IEmailSender , EmailSender>();

            //builder.Services.AddDefaultIdentity<IdentityUser>()
            // .AddEntityFrameworkStores<ApplicationDbContext>();

            //We will use repository here 
            //--------------New Dependency Injection----------- 
            //And We USe Scope ("lifetime" service/patteren) 
            //builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            //Change to Unit Of Work For Dont Repeat 
            //builder.Services.AddScoped<IUn, UnitOfWork>();

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
			builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

			builder.Services.ConfigureApplicationCookie(option =>
			{
				option.LoginPath = $"/Identity/Account/Login";
                option.LogoutPath = $"/Identity/Account/Logout";
                option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });
			
			
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

               app.UseAuthentication();;

			app.UseAuthorization();

			app.MapRazorPages();
			
			app.MapControllerRoute(
				name: "default",
				pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}