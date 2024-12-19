//using Hairr.Services;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.IO;
//using System.Threading.Tasks;

//namespace Hairr.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Roles = "User,Admin")] // Kullanıcı ve Admin rolündeki kullanıcılar erişebilir
//    public class AIController : ControllerBase
//    {
//        private readonly AIService _aiService;

//        public AIController(AIService aiService)
//        {
//            _aiService = aiService;
//        }

//        // POST: api/AI/AnalyzeHair
//        [HttpPost("AnalyzeHair")]
//        public async Task<IActionResult> AnalyzeHair(IFormFile image)
//        {
//            if (image == null || image.Length == 0)
//            {
//                return BadRequest("Lütfen geçerli bir resim yükleyin.");
//            }

//            using (var ms = new MemoryStream())
//            {
//                await image.CopyToAsync(ms);
//                var imageData = ms.ToArray();

//                var analysisResult = await _aiService.AnalyzeHairStyleAsync(imageData);

//                return Ok(new { Suggestion = analysisResult });
//            }
//        }
//    }
//}
