using Lumeo.Desktop.EverydayPoem;
using Lumeo.Desktop.ImageSlideshow;
using MyToolBar.Plugin;

namespace Lumeo.Desktop;

public class Package : IPackage
{
    internal const string GlobalName = "Lumeo.Desktop";
    public string PackageName => GlobalName;

    public string DisplayName => GlobalName;

    public string Description { get; } = "A simple frame showing content in the desktop.";

    public Version Version { get; } = new(1, 0, 0, 0);

    public List<IPlugin> Plugins { get; set; }= [new ImgSlideshowPlugin(),new EverydayPoemPlugin()];
}

public class ImgSlideshowPlugin : IPlugin
{
    internal const string PluginName = "Lumeo.Desktop.ImageSlideshowFrame";

    public string Name => PluginName;

    public string DisplayName => "Image Slideshow Frame";

    public string Description => "A simple image frame";

    public List<string>? SettingsSignKeys { get; set; } = null;

    public PluginType Type => PluginType.UserService;
    public ServiceBase GetServiceHost() => new ImageSlideshowService();
}

public class EverydayPoemPlugin : IPlugin
{
    internal const string PluginName = "Lumeo.Desktop.EverydayPoemFrame";

    public string Name => PluginName;

    public string DisplayName => "Everyday Poem Frame";

    public string Description => "Everyday poem";

    public List<string>? SettingsSignKeys { get; set; } = null;

    public PluginType Type => PluginType.UserService;
    public ServiceBase GetServiceHost() => new EverydayPoemService();
}