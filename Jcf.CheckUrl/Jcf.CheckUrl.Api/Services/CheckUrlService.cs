using Jcf.CheckUrl.Api.Data;

namespace Jcf.CheckUrl.Api.Services
{
    public class CheckUrlService : ICheckUrlService
    {
        private const string _default = "URL Não Conhecida!";
       
        public CheckUrlService()
        {
            
        }

        public string Run(string url)
        {
            try
            {
                var listUrlsBase = UrlsRepositories.GetAll();
                var _check = Check(url, listUrlsBase);
                return _check;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return string.Empty;
        }

        private string Check(string url, IEnumerable<string> listUrls)
        {

            Uri uri;
            try
            {
                uri = new UriBuilder(url.Contains("http") ? url : "http://" + url).Uri;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return _default;
            }

            string host = uri.Host;
            string path = uri.AbsolutePath.Trim('/');

            foreach (var item in listUrls.OrderByDescending(u => u.Count(c => c == '.')))
            {
                if (host.Equals(item, StringComparison.OrdinalIgnoreCase) ||
                    host.EndsWith("." + item, StringComparison.OrdinalIgnoreCase) ||
                    ($"{host}/{path}").StartsWith(item, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }

            return _default;
        }
    }
}
