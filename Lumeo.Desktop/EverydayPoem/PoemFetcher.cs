using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lumeo.Desktop.EverydayPoem
{
    /// <summary>
    /// 用于从 meirishici.com 抓取每日一诗数据
    /// </summary>
    internal class PoemFetcher
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string PoemUrl = "https://meirishici.com/";

        static PoemFetcher()
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
        }

        public static async Task<PoemData> FetchDailyPoemAsync()
        {
            var html = await _httpClient.GetStringAsync(PoemUrl);
            return ParsePoemFromHtml(html) ?? new();
        }

        private static PoemData? ParsePoemFromHtml(string html)
        {
            try
            {
                var poemData = new PoemData();

                // 提取标题 - 在 <h1> 标签内的链接文本
                var titleMatch = Regex.Match(html, @"<h1[^>]*>.*?<a[^>]*>(.*?)</a>.*?</h1>", RegexOptions.Singleline);
                if (titleMatch.Success)
                {
                    poemData.Title = DecodeHtml(titleMatch.Groups[1].Value.Trim());
                }

                // 提取作者 - 查找dynasty后面的作者链接
                var authorMatch = Regex.Match(html, @"dynasty_id=\d+"">.*?</a>.*?<span>·</span>.*?<a[^>]*>(.*?)</a>", RegexOptions.Singleline);
                if (authorMatch.Success)
                {
                    poemData.Author = DecodeHtml(authorMatch.Groups[1].Value.Trim());
                }

                // 提取内容 - 在 line-clamp-8 的 div 中
                var contentMatch = Regex.Match(html, @"<div[^>]*class=""[^""]*line-clamp-8[^""]*""[^>]*>(.*?)</div>", RegexOptions.Singleline);
                if (contentMatch.Success)
                {
                    poemData.Content = DecodeHtml(contentMatch.Groups[1].Value.Trim());
                }

                // 验证是否成功提取了数据
                if (string.IsNullOrEmpty(poemData.Title) &&
                    string.IsNullOrEmpty(poemData.Author) &&
                    string.IsNullOrEmpty(poemData.Content))
                {
                    return null;
                }

                return poemData;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 解码HTML实体
        /// </summary>
        private static string DecodeHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            return System.Net.WebUtility.HtmlDecode(html)
                 .Replace("\r", "")
                 .Replace("\n", "")
            .Trim();
        }
    }

    internal class PoemData
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
