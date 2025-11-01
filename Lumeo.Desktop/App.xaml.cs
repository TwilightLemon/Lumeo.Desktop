using Lumeo.Desktop.EverydayPoem;
using System.Windows;

namespace Lumeo.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            EverydayPoemService.ServiceInstance = new();
            EverydayPoemService.ServiceInstance?.Start();
            new Window() { Visibility = Visibility.Hidden }.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            EverydayPoemService.ServiceInstance?.Stop();
        }
    }

}
