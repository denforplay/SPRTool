using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SPR.Client.Abstractions.Core;
using SPR.Client.Abstractions.Http;
using SPR.Client.Abstractions.Services;
using SPR.Client.Communication.Http;
using SPR.Client.Services.Navigation;
using SPR.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SPR.Client
{
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }
        private readonly INavigationService _navigationService;

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddHttpClient<IGroupHttpService, GroupHttpService>(config =>
                    {
                        config.BaseAddress = new Uri("http://localhost:5052");
                    });
                    services.AddHttpClient<IStudentHttpService, StudentHttpService>(config =>
                    {
                        config.BaseAddress = new Uri("http://localhost:5053");
                    });
                    services.AddSingleton<INavigationService>(sp => new NavigationService(new ViewModelsFactoryService(
                        new Dictionary<Type, Func<ViewModelBase>>()
                        {
                            { typeof(MainViewModel), () => new MainViewModel(sp.GetRequiredService<INavigationService>()) },
                            { typeof(HomeViewModel), () => new HomeViewModel() },
                            { typeof(CourseManagementViewModel), () => new CourseManagementViewModel() },
                            { typeof(StudentManagementViewModel), () => new StudentManagementViewModel(sp.GetRequiredService<IStudentHttpService>(), sp.GetRequiredService<IGroupHttpService>()) },
                        })));
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}