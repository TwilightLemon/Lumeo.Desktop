using System.Windows.Controls;

namespace Lumeo.Desktop.EverydayPoem
{
    /// <summary>
    /// Interaction logic for EverydayPoemFrame.xaml
    /// </summary>
    public partial class EverydayPoemFrame : UserControl
    {
        internal EverydayPoemConfig Config;
        public EverydayPoemFrame(EverydayPoemConfig config)
        {
            InitializeComponent();
            Config = config;
            Init();
        }

        private async void Init()
        {
            TitleTb.Text = Config.Title;
            AuthorTb.Text = Config.Author;
            ContentTb.Text = Config.Content;
            if (Config.UpdatedTime != DateOnly.FromDateTime(DateTime.Now))
            {
               // Update data
                await UpdatePoemDataAsync();
                Config.UpdatedTime = DateOnly.FromDateTime(DateTime.Now);
            }
        }

        private async Task UpdatePoemDataAsync()
        {
            try
            {
                var poemData = await PoemFetcher.FetchDailyPoemAsync();
       
                if (poemData != null)
                {
                    TitleTb.Text=Config.Title = poemData.Title;
                    AuthorTb.Text= Config.Author = poemData.Author;
                    ContentTb.Text = Config.Content = poemData.Content;
                }
            }
            catch { }
        }
    }
}
