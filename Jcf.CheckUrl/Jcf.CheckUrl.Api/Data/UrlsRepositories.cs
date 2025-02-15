namespace Jcf.CheckUrl.Api.Data
{
    public static class UrlsRepositories
    {
        private static List<string> _urls = new List<string>() {
            "netvasco.com.br",
            "joao.com.br",
            "anamaria.com.br",
            "filha.joao.com.br",
            "globo.com",
            "x.com",
            "x.com/joao",
            "x.com/anamaria",
            "facebook.com",
            "canal.youtube.com",
            "youtube.com/joao",
            "youtube.com/anamaria/203932"
        };

        public static List<string> GetAll()
        {
            return _urls;
        }
    }
}
