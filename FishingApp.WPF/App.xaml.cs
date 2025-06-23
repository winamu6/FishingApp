using FishingApp.Core.Repoitories.FishingRepositories;
using FishingApp.Core.Repoitories.FishingRepositories.IFishingRepositories;
using FishingApp.Core.Repoitories.UserRepositories;
using FishingApp.Core.Repoitories.UserRepositories.IUserRepositories;
using FishingApp.Core.Repositories.Implementations;
using FishingApp.Core.Repositories.Interfaces;
using FishingApp.Core.Services.AuthServices;
using FishingApp.Core.Services.AuthServices.IAuthServices;
using FishingApp.Core.Services.FishingServices;
using FishingApp.Core.Services.FishingServices.IFishingServices;
using FishingApp.Core.Services.FishServices;
using FishingApp.Core.Services.FishServices.Interfaces;
using FishingApp.Core.Services.UserServices.Implementations;
using FishingApp.Core.Services.UserServices.Interfaces;
using FishingApp.Data.Context;
using FishingApp.WPF.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Windows;

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
                    services.AddScoped<IFishingRepository, FishingRepository>();

                    services.AddScoped<IAuthService, AuthService>();
                    services.AddScoped<IFishService, FishService>();
                    services.AddScoped<IUserService, UserService>();
                    services.AddScoped<IFishingService, FishingService>();

                    services.AddTransient<MainWindow>();
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
