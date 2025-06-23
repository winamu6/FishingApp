using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using FishingApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using FishingApp.Core.Repoitories.UserRepositories.IUserRepositories;
using FishingApp.Core.Repoitories.UserRepositories;
using FishingApp.Core.Services.AuthServices.IAuthServices;
using FishingApp.Core.Services.AuthServices;
using System.Runtime.InteropServices.JavaScript;
using FishingApp.Core.Repositories.Interfaces;
using FishingApp.Core.Repositories.Implementations;
using FishingApp.Core.Services.FishServices.Interfaces;
using FishingApp.Core.Services.FishServices;
using FishingApp.WPF.Windows;

namespace FishingApp.WPF
{
    public partial class App : Application
    {
        private IHost? _host;

        public static IServiceProvider Services => ((App)Current)._host!.Services;

        protected override void OnStartup(StartupEventArgs e)
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<FishingDbContext>(options =>
                        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));


                    services.AddScoped<IUserRepository, UserRepository>();
                    services.AddScoped<IFishRepository, FishRepository>();


                    services.AddScoped<IAuthService, AuthService>();
                    services.AddScoped<IFishService, FishService>();

                    services.AddSingleton<MainWindow>();
                    services.AddTransient<FishDexWindow>();
                })
            .Build();

            _host.Start();

            var mainWindow = Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (_host is not null)
                await _host.StopAsync();

            base.OnExit(e);
        }
    }
}
