namespace Jcf.CheckUrl.Api.Services
{
    public interface IVariantService
    {
        IEnumerable<string> GetList();
        IEnumerable<string> UrlMatcher(string url);
    }
}
