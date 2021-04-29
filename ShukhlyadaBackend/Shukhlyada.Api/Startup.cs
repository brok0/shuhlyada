using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Shukhlyada.Api.AuthenticationHandlers;
using Shukhlyada.Api.Middlewares;
using Shukhlyada.BusinessLogic.Abstractions;
using Shukhlyada.BusinessLogic.Services;
using Shukhlyada.Domain.Models;
using Shukhlyada.Infrastructure;
using Shukhlyada.Infrastructure.Abstractions;
using Shukhlyada.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Shukhlyada.Api
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

            services.AddControllers(options => options.SuppressAsyncSuffixInActionNames = false);

            services.Configure<ElasticEmailCredentials>(Configuration.GetSection("ElasticEmailCredentials"));

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ShukhlyadaDB"),
                                                                                o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IMailService, MailService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFeedService, FeedService>();
            services.AddScoped<IChannelService, ChannelService>();

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shukhlyada.Api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = AuthenticationSchemes.Basic.ToString(),
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "basic"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.AddCors(o => o.AddPolicy("ReactPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shukhlyada.Api v1"));
                app.UseCors("ReactPolicy");
            }

           // app.UseHttpsRedirection(); //redirects http on https 

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
