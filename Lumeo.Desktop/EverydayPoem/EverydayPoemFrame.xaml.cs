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
            if (Config.UpdatedTime != DateOnly.FromDateTime(DateTime.Now))
            {
               // Update data
                await UpdatePoemDataAsync();
            }
            TitleTb.Text = Config.Title;
            AuthorTb.Text = Config.Author;
            ContentTb.Text = Config.Content;
            DateTb.Text = Config.UpdatedTime.ToShortDateString();
        }

        private async Task UpdatePoemDataAsync()
        {
            try
            {
                var poemData = await PoemFetcher.FetchDailyPoemAsync();
       
                if (poemData != null)
                {
                    Config.Title = poemData.Title;
                    Config.Author = poemData.Author;
                    Config.Content = poemData.Content;
                    Config.UpdatedTime = DateOnly.FromDateTime(DateTime.Now);
                }
            }
            catch { }
        }
    }
}
