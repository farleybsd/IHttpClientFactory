using Microsoft.Extensions.Options;
using System;
using ViaCep.com.br.Dominio;
using ViaCep.com.br.ViaCep.Handlers;
using ViaCep.com.br.ViaCep.Service;
using ViaCep.com.br.ViaCep.Settings;
using ViaCep.com.br.ViaCep.Validates;

namespace ViaCep.com.br.ViaCep.Infra
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddViaCep(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<LoggingHandler>();
            services.AddSingleton<IValidateOptions<ViaCepSettings>, ViaCepSettingsValidate>();
            services.AddOptionsWithValidateOnStart<ViaCepSettings>()
                .Bind(configuration)
                .ValidateDataAnnotations() // Se houver atributos de validação na classe ViaCepSettings
                .ValidateOnStart(); // Garante que a validação ocorra na inicialização;

            services.AddTransient<ViaCepGateway>();

            services.AddHttpClient<IviaCepGateway, ViaCepGateway>((serviceProvider, httpClient) =>
            {
                var viaCepSettings = serviceProvider.GetRequiredService<IOptions<ViaCepSettings>>();
                httpClient.BaseAddress = new(viaCepSettings.Value.BaseAddress);
                Console.WriteLine($"BaseAddress configurada corretamente: {httpClient.BaseAddress}");
                // httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {viaCepSettings.Value.ApiKey}");
            }).AddHttpMessageHandler<LoggingHandler>();

            return services;
        }
    }
}
