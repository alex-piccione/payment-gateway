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

            string mockPaymentId = Configuration["Bank:Mock:PaymentId"];
            services.AddSingleton<IBankClient>(new BankClientMock(mockPaymentId));           
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
                var bankClientMock = app.ApplicationServices.GetService<IBankClient>() as BankClientMock;
                if (bankClientMock != null)
                    bankClientMock.KnownPaymentId = Configuration["Bank:Mock:PaymentId"];
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
