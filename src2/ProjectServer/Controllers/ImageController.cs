using Microsoft.AspNetCore.Mvc;

namespace ProjectServer.Controllers
{
    [Route("image")]
    [ApiController]
    public class ImageController : Controller
    {
        public IActionResult LoadImage(string path)
        {
            if (!System.IO.File.Exists(path))
                return NotFound();

            string contentType;

            if (path.ToUpper().EndsWith(".JPG"))
            {
                contentType = "image/jpeg";
            }
            else if (path.ToUpper().EndsWith(".PNG"))
            {
                contentType = "image/png";
            }
            else
            {
                contentType = "text/plain";
            }

            return PhysicalFile(path, contentType);
        }
    }
}
