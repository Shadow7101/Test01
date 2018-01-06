using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App1.Data.MsSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using App1.Domain.Services;
using App1.Application.Users;
using App1.Application.Email;
using App1.Domain.Repositories;
using App1.Data.MsSql.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;

namespace App1.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<App1DbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("Database1")));

            services.AddMemoryCache();

            services.AddTransient<IAuthenticateService, AuthenticateService>();
            services.AddTransient<IRegisterService, RegisterService>();
            services.AddTransient<IRememberService, RememberService>();
            services.AddTransient<ISendMail, SendMail>();

            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    //os parametros
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "App1.Valid.Issuer",
                        ValidAudience = "App1.Valid.Audience",
                        IssuerSigningKey = App1.Application.JWT.JwtSecurityKey.Create("Secrete_Key-Is-Other?123456789")
                    };
                    //os eventos
                    option.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("OnAuthenticationFailed " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("OnTokenValidated " + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UsuarioApi", policy => policy.RequireClaim("UsuarioApiNumero"));
                options.AddPolicy("UsuarioRole", policy => policy.RequireClaim("UserRole"));
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });

            app.UseMvc();
        }
    }
}
