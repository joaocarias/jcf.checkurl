using Jcf.CheckUrl.Api.Services;

namespace Jcf.CheckUrl.Test
{
    public class CheckUrlServiceTests
    {
        private readonly CheckUrlService _service;
        private const string _invalidUrl = "URL Não Conhecida!";

        public CheckUrlServiceTests()
        {
            _service = new CheckUrlService();
        }

        [Theory]
        [InlineData("x.com/joao", "x.com")]
        [InlineData("olx.com", _invalidUrl)]        
        [InlineData("filha.joao.com.br", "filha.joao.com.br")]
        [InlineData("globo.com/alunos/938202", "globo.com")]
        [InlineData("youtube.com/joao/fhsksks", "youtube.com/joao")]
        [InlineData("youtube.com", _invalidUrl)]
        [InlineData("youtube.com/anamaria", _invalidUrl)]
        [InlineData("anamaria.facebook.com", "facebook.com")]
        [InlineData("msn.com", _invalidUrl)]
        [InlineData("netflix.com", _invalidUrl)]
        [InlineData("msnx.com", _invalidUrl)]

        public void Run_ShouldReturnCorrectGroup(string url, string expectedGroup)
        {
            var result = _service.Run(url);
            Assert.Equal(expectedGroup, result);
        }

        [Theory]
        [InlineData("invalid-url")]
        [InlineData("olx.com")]
        [InlineData("youtube.com")]
        [InlineData("meusite.nzt.br")]
        [InlineData("teste.meusite.com")]
        [InlineData("meusite.com")]
        [InlineData("sub.meusite.com/teste")]
        [InlineData("sub.netvasco.com/teste")]
        [InlineData("msn.com")]
        [InlineData("netflix.com")]
        public void Run_ShouldReturnDefaultMessage_WhenInvalidUrl(string url)
        {
            var result = _service.Run(url);
            Assert.Equal(_invalidUrl, result);
        }

        [Theory]
        [InlineData("x.com/joao")]    
        [InlineData("filha.joao.com.br")]
        [InlineData("globo.com/alunos/938202")]
        [InlineData("youtube.com/joao/fhsksks")]
        [InlineData("anamaria.facebook.com")]
        public void Run_ShouldReturnCorrect_NotInvaliUrl(string url)
        {
            var result = _service.Run(url);
            Assert.NotEqual(_invalidUrl, result);
        }
    }
}