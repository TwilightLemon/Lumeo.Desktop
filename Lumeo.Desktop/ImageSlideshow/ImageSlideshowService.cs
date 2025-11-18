using MyToolBar.Common;
using MyToolBar.Plugin;
using System.ComponentModel;

namespace Lumeo.Desktop.ImageSlideshow;

public class ImageSlideshowService : ServiceBase
{
    internal static ImageSlideshowService? ServiceInstance = null;
    internal const string ConfigKey = "Lumeo.Desktop.ImageSlideshowFrame.Config";
    private readonly SettingsMgr<ImageSlideshowConfigEntity> config = new(ConfigKey, ImgSlideshowPlugin.PluginName);
    private bool _isRunning = false;
    private readonly List<MainWindow> mainWindows = [];
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

    public void CreateNewFrame()
    {
        var fc = new ImageSlideshowConfig();
        config.Data.FrameConfigs.Add(fc);
        var w = new MainWindow(fc);
        var frame = new ImageSlideshowFrame(fc, w);
        w.Content = frame;
        w.Show();
        mainWindows.Add(w);
    }

    public void RemoveFrame(MainWindow w)
    {
        if (mainWindows.Contains(w) && w.Config is ImageSlideshowConfig slideConfig)
        {
            config.Data.FrameConfigs.Remove(slideConfig);
            mainWindows.Remove(w);
            w.Close();
            // if no frame left, stop the service
            if (mainWindows.Count == 0)
            {
                OnForceStop?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void Dispose()
    {
        foreach (var w in mainWindows)
        {
            w.Close();
        }
        mainWindows.Clear();
    }

    public async Task Start()
    {
        ServiceInstance = this;
        await config.LoadAsync();
        IsRunning = true;
        if (config.Data.FrameConfigs.Count == 0)
        {
            config.Data.FrameConfigs.Add(new());
        }
        foreach (var fc in config.Data.FrameConfigs)
        {
            var w = new MainWindow(fc);
            var frame = new ImageSlideshowFrame(fc, w);
            w.Content = frame;
            w.Show();
            mainWindows.Add(w);
        }
    }

    public async Task Stop()
    {
        Dispose();
        await config.SaveAsync();
    }
}
