using System.Windows;

namespace Lumeo.Desktop;

public class FrameConfig
{
    public Rect WindowRect { get; set; }
}

#region ImageSlideshow Configs
public class ImageSlideshowConfig : FrameConfig
{
    public List<string> ImgPaths { get; set; } = [];
    public int Index { get; set; } = 0;
    public bool Locked { get; set; } = false;
    public int Interval { get; set; } = 5; // minutes
    public bool RandomOrder { get; set; } = false;
}
public class ImageSlideshowConfigEntity
{
    public List<ImageSlideshowConfig> FrameConfigs { get; set; } = [];
}
#endregion

#region EverydayPoem
public class EverydayPoemConfig : FrameConfig
{
    public string FontFamily { get; set; } = "FangSong, KaiTi, Segoe UI";
    public string Content { get; set; } = "";
    public string Author { get; set; } = "";
    public string Title { get; set; } = "";
    public DateOnly UpdatedTime { get; set; }
}
#endregion