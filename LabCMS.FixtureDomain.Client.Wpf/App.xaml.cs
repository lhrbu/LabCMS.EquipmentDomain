using LabCMS.FixtureDomain.Client.Wpf.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raccoon.DevKits.Wpf.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LabCMS.FixtureDomain.Client.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IDIApplication
    {
        public IServiceProvider ServiceProvider { get; set; } = null!;
        public IConfiguration Configuration { get; set; } = null!;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<FixturesPage>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.OnStartupProxy<FixturesPage>();
        }
    }
}
