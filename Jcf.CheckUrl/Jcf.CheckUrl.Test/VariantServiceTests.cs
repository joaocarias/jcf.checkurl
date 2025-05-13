using Jcf.CheckUrl.Api.Services;

namespace Jcf.CheckUrl.Test
{
    public class VariantServiceTests
    {
        VariantService _service;

        public VariantServiceTests()
        {
            _service = new VariantService();
        }

        [Fact]
        public void GetAllList_Test_More_Than_Zero()
        {
            var result = _service.GetList().ToList();
            Assert.True(result.Count > 0);
        }

        [Theory]
        [InlineData("ge.globo.com")]
        [InlineData("mundo.globo.com")]
        [InlineData("ha.remote.homeagent.com.br")]
        [InlineData("netvasco.com.pt")]
        [InlineData("netvasco.com.br")]
        public void UrlMatcher_Test_More_Than_Zero(string url)
        {
            var result = _service.UrlMatcher(url).ToList();
            Assert.True(result.Count > 0);
        }

        [Theory]
        [InlineData("globo.com/ge")]
        [InlineData("globo.com")]
        [InlineData("remote.homeagent.com.br")]
        [InlineData("netvasco.com")]
        [InlineData("br.netvasco.com")]
        public void UrlMatcher_Test_No_Content(string url)
        {
            var result = _service.UrlMatcher(url).ToList();
            Assert.True(result.Count <= 0);
        }

        [Theory]
        [InlineData("ha.remote.homeagent.com.br")]
        [InlineData("ha1.remote.homeagent.com.br")]
        [InlineData("ha50.remote.homeagent.com.br")]
        [InlineData("server.channel.one.com.br")]
        [InlineData("br.localhost.test:5000")]
        [InlineData("com.localhost.test")]
        [InlineData("ola.channel.one.com.br/test=3837")]
        public void UrlMatcher_Test_Full_Variant(string url)
        {
            var result = _service.UrlMatcher(url).ToList();
            Assert.True(result.Count > 0);
        }

        [Theory]
        [InlineData("remote.homeagent.com.br")]
        [InlineData("ha1.remote.homeagent.com")]
        [InlineData("remote.homeagent.com")]
        [InlineData("server.channel.one")]
        [InlineData("localhost")]
        [InlineData(".localhost.test")]
        [InlineData("github.com/joaocarias")]
        [InlineData("ola.channel.one.com")]
        [InlineData("sonha.com.br")]
        public void UrlMatcher_Test_Full_Variant_No_Content(string url)
        {
            var result = _service.UrlMatcher(url).ToList();
            Assert.True(result.Count <= 0);
        }

    }
}
