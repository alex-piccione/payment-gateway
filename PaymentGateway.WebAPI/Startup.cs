using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentGateway.Core;
using PaymentGateway.Core.Bank;
using PaymentGateway.Core.Mocking;
using PaymentGateway.DataLayer;

namespace PaymentGateway.WebApi
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
            services.AddControllers();

            services.AddSingleton<IPaymentsProcessor, PaymentsProcessor>();

            // mocking
            services.AddSingleton<IPaymentsRepository>(new PaymentsRepositoryMock());
            services.AddSingleton<IBankClient>(new BankClientMock());           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsEnvironment("Mock"))
            {
                //var bankClientMock = app.ApplicationServices.GetService<IBankClient>() as BankClientMock;
                var paymentsRepositoyMock = app.ApplicationServices.GetService<IPaymentsRepository>() as PaymentsRepositoryMock;
                if (paymentsRepositoyMock != null)
                    paymentsRepositoyMock.Capacity = int.Parse( Configuration["Mocking:RepositoryMaxSize"]);
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
