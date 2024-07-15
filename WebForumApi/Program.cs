
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Xml.XPath;
using WebForumApi.Database;

namespace WebForumApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                opt =>
                {
                    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer"
                    });

                    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });

                    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                }
            );

            builder.Services.AddDbContext<UsersDbContext>(options => options.UseSqlite("DataSource = identityDb; Cache=Shared"));
            builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<UsersDbContext>();
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<ForumDbContext>(options => options.UseSqlite("DataSource = forumDb2; Cache=Shared"));
            builder.Services.AddScoped<ForumRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                //app.ApplyMigrations();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapGroup("/identity").MapIdentityApi<IdentityUser>();

            using (var scope = app.Services.CreateScope()) 
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] {"Moderator", "User"};
                foreach (var role in roles)
                {
                    if(!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string adminUsername = "admin@webforum.com";
                string adminPassword ="P@ssw0rd";

                if(await userManager.FindByNameAsync(adminUsername) == null)
                {
                    var adminUser = new IdentityUser(adminUsername)
                    {
                        Email = adminUsername
                    };
                    await userManager.CreateAsync(adminUser, adminPassword);
                    await userManager.AddToRoleAsync(adminUser, "Moderator");
                }
            }

            app.Run();
        }
    }
}
