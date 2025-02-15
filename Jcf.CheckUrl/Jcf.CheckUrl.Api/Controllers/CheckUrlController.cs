using Jcf.CheckUrl.Api.Data;
using Jcf.CheckUrl.Api.DTOs;
using Jcf.CheckUrl.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jcf.CheckUrl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckUrlController : ControllerBase
    {
        private readonly ILogger<CheckUrlController> _logger;
        private readonly ICheckUrlService _checkUrlService;

        public CheckUrlController(ILogger<CheckUrlController> logger, ICheckUrlService checkUrlService)
        {
            _logger = logger;
            _checkUrlService = checkUrlService;
        }

        [HttpPost]
        public IActionResult Post([FromForm] PostUrl postUrl)
        {
            try
            {
                var check = _checkUrlService.Run(postUrl.Url);
                if (check == null)
                {
                    return BadRequest("Não foi possível verificar!");
                }

                return Ok(check);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(UrlsRepositories.GetAll());
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
