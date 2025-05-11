using Jcf.CheckUrl.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Jcf.CheckUrl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariantController : ControllerBase
    {
        private readonly ILogger<VariantController> _logger;
        private readonly IVariantService _variantServie;

        public VariantController(ILogger<VariantController> logger, IVariantService variantServie)
        {
            _logger = logger;
            _variantServie = variantServie;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var all = _variantServie.GetList();
                if (all == null)
                {
                    return BadRequest("Não Encontrou nenhum!");
                }
                return Ok(all);
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("url")]
        public IActionResult GetVariant(string url)
        {
            try
            {
                var variant = _variantServie.UrlMatcher(url);
                if (variant == null)
                {
                    return BadRequest("Não Encontrou nenhum!");
                }
                return Ok(variant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
            
    }
}
