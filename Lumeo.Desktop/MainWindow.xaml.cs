using MyToolBar.Common;
using MyToolBar.Common.WinAPI;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace Lumeo.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal readonly FrameConfig Config;
        public MainWindow(FrameConfig config)
        {
            InitializeComponent();
            Config = config;
            Init();
            SourceInitialized += MainWindow_SourceInitialized;
            GlobalService.OnThemeColorChanged += GlobalService_OnThemeColorChanged;
            Closing += MainWindow_Closing;
        }

        private void MainWindow_SourceInitialized(object? sender, EventArgs e)
        {
            WindowLongAPI.SetToolWindow(this);
           // DesktopWindowHelper.EmbedWindowToDesktop(this);
        }

        private void GlobalService_OnThemeColorChanged()
        {
            if (App.Current.Resources["SystemThemeColor"] is SolidColorBrush { Color: var c })
                Material.CompositonColor = Color.FromArgb(153, c.R, c.G, c.B);
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            Config.WindowRect = new Rect(Left, Top, Width, Height);
        }

        private void Init()
        {
            GlobalService_OnThemeColorChanged();
            if (Config.WindowRect is { Width: > 0, Height: > 0 } rect)
            {
                Left = rect.Left;
                Top = rect.Top;
                Width = rect.Width;
                Height = rect.Height;
            }
        }
    }
}