using Application.Payments.Interfaces;
using Infrastructure.Payments.Options;
using Infrastructure.Payments.Providers;
using Infrastructure.Payments.Providers.VnPay;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class PaymentDependencyInjection
    {
        public static IServiceCollection AddPaymentServices(this IServiceCollection services, IConfiguration config)
        {
            var section = config.GetSection("Vnpay");
            services.Configure<VnPayOptions>(section);
            services.AddHttpContextAccessor();

            services.AddScoped<IPaymentGateway, VnPayGateway>();
            services.AddScoped<IPaymentOrchestrator, PaymentOrchestrator>();

            return services;
        }
    }
}
