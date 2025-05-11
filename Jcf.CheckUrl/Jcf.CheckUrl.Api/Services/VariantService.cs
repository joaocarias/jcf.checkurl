
using Jcf.CheckUrl.Api.Data;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;
using System;

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
               

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<string>();
            }
        }
    }
}
