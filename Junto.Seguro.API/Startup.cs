using AutoMapper;
using Junto.Seguros.Data.Context;
using Junto.Seguros.Data.Users;
using Junto.Seguros.Domain.Auths.Contracts;
using Junto.Seguros.Domain.Commons;
using Junto.Seguros.Domain.Users.Contracts;
using Junto.Seguros.Infra.Encrypters;
using Junto.Seguros.Infra.Jwt;
using Junto.Seguros.Infra.Notifications;
using Junto.Seguros.Services.Auths;
using Junto.Seguros.Services.Commons;
using Junto.Seguros.Services.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Junto.Seguro.API
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
            //todo adicionar os scopes
            //todo adicionar os  testes
            //todo adicionar o validation
            //todo versionar github
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAuthService, AuthAppService>();
            services.AddScoped<IUserService, UserAppService>();
            services.AddScoped<IEncrypterService, EncrypterService>();
            services.AddJwt(Configuration);
            services.AddAutoMapper(typeof(MapperBase));
            services.AddScoped<IDomainNotificationProvider, DomainNotificationProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddControllers();

            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",new OpenApiInfo());

            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Junto Seguros");
            });

        }
    }
}
