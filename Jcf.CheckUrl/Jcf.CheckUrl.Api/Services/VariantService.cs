
using Jcf.CheckUrl.Api.Data;
using Microsoft.OpenApi.Extensions;

namespace Jcf.CheckUrl.Api.Services
{
    public class VariantService : IVariantService
    {
        public VariantService() { }

        public IEnumerable<string> GetList()
        {
            try
            {
                return UrlsRepositories.GetAll().Where(x => x.Contains("*")).ToList();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<string>();
            }                
        }

        public IEnumerable<string> UrlMatcher(string url)
        {
            try
            {
                List<string> result = new List<string>();

                var list = GetList();
                foreach (var item in list)
                {
                    var variant = string.Empty;
                    variant = GetVariantType(item) switch
                    {
                        AppAnalyticUrlVariantType.start => item.Substring(1, item.Length - 1),
                        AppAnalyticUrlVariantType.end => item.Substring(0, item.Length - 2),
                        AppAnalyticUrlVariantType.full => item.Substring(1, item.Length - 2),                       
                        _ => item,
                    };

                    Console.WriteLine($"Url: {url} | Variant: {variant}");

                    if (!string.IsNullOrEmpty(variant) && url.Contains(variant, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($" --------->>>>>  Url: {url} | Variant: {variant}");

                        var r = ExpandWildcardPattern(url, variant);
                        Console.WriteLine($" --------->>>>>  Url: {url} | Variant: {variant} | Result: {r}");

                        if (!string.IsNullOrEmpty(r))                           
                            result.Add(r);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<string>();
            }
        }

        private AppAnalyticUrlVariantType GetVariantType(string value)
        {
            try
            {
                if (IsVariant(value))
                {
                    if (value.StartsWith("*") && value.EndsWith("*"))
                        return AppAnalyticUrlVariantType.full;

                    if (value.StartsWith("*"))
                        return AppAnalyticUrlVariantType.start;

                    if (value.EndsWith("*"))
                        return AppAnalyticUrlVariantType.end;

                    return AppAnalyticUrlVariantType.unknown;
                }

                return AppAnalyticUrlVariantType.noVariant;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(VariantService)} | {nameof(GetVariantType)} | Value: {value} | Error: {ex.Message}");
            }

            return AppAnalyticUrlVariantType.unknown;
        }

        private bool IsVariant(string value)
        {
            try
            {
                return value.Contains("*");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(VariantService)} | {nameof(IsVariant)} | Value: {value} | Error: {ex.Message}");
                return false;
            }
        }
        
        private string? ExpandWildcardPattern(string url, string pattern)
        {
            int index = url.IndexOf(pattern, StringComparison.OrdinalIgnoreCase);
            if (index == -1)
                return null; // padrão não encontrado na URL

            string prefix = url.Substring(0, index); // tudo antes
            string suffix = url.Substring(index + pattern.Length); // tudo depois

            string full = prefix + pattern + suffix;
            Console.WriteLine($"Prefix: {prefix} | Suffix: {suffix} | Pattern: {pattern} | Full: {full}");

            return full;
        }
    }

    public enum AppAnalyticUrlVariantType
    {
        unknown,
        start,
        end,
        full,
        noVariant
    }
}
