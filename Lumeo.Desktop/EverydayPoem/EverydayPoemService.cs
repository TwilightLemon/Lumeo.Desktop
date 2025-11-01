using MyToolBar.Common;
using MyToolBar.Plugin;

namespace Lumeo.Desktop.EverydayPoem;

public class EverydayPoemService : ServiceBase
{
    internal static EverydayPoemService? ServiceInstance = null;
    internal const string ConfigKey = "Lumeo.Desktop.EverydayPoemFrame.Config";
    private readonly SettingsMgr<EverydayPoemConfig> config = new(ConfigKey, EverydayPoemPlugin.PluginName);
    private bool _isRunning = false;
    private MainWindow? mainWindow = null;
    public bool IsRunning
    {
        get => _isRunning;
        private set
        {
            if (_isRunning != value)
            {
                _isRunning = value;
                IsRunningChanged?.Invoke(this, value);
            }
        }
    }
    public event EventHandler<bool>? IsRunningChanged;
    public event EventHandler? OnForceStop;
    public void Dispose()
    {
        if(mainWindow?.IsLoaded == true)
        {
            mainWindow.Close();
            mainWindow = null;
        }
    }

    public async Task Start()
    {
        ServiceInstance = this;
        await config.Load();
        IsRunning = true;
        mainWindow = new MainWindow(config.Data);
        var frame = new EverydayPoemFrame(config.Data);
        mainWindow.Content = frame;
        mainWindow.Closed += (s, e) =>
        {
            IsRunning = false;
            OnForceStop?.Invoke(this, EventArgs.Empty);
            mainWindow = null;
            _= config.Save();
        };
        mainWindow.Show();
    }

    public async Task Stop()
    {
        await config.Save();
        Dispose();
    }
}
