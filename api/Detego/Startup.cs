using Detego.Communication;
using Detego.DAL;
using Detego.Handlers;
using Detego_RfidReaderAdapter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Detego
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
            services.AddSingleton<IDataStorage, DataStorage>();
            services.AddSingleton<IStorageRfidReaderHandler, StorageRfidReaderHandler>();
            services.AddSingleton<ISignalrTagReadHandler, SignalrTagReadHandler>();
            services.AddSingleton<IRfidReaderAdapter, RfidReaderAdapter>();
            services.AddSingleton<HandlerRegistrator>();
            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            HandlerRegistrator handlerRegistrator)
        {
            handlerRegistrator.Register();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(builder =>
            builder.WithOrigins("http://localhost:8080")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
            app.UseHttpsRedirection();
            app.UseSignalR(routes =>
            {
                routes.MapHub<TagHub>("/tag");
            });
            app.UseMvc();
        }
    }
}
