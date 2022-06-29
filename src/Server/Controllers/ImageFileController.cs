
using Microsoft.AspNetCore.Mvc;

namespace ProjectServer.Server.Controllers;

[ApiController]
[Route("api/ImageFile")]
public class ImageFileController : ControllerBase
{
    private readonly ILogger<AbfFolderController> _logger;

    public ImageFileController(ILogger<AbfFolderController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public FileResult Get(string? path)
    {
        if (!System.IO.File.Exists(path))
        {
            throw new FileNotFoundException();
        }

        string contentType;
        if (path.EndsWith(".jpg"))
            contentType = "image/jpeg";
        else if (path.EndsWith(".png"))
            contentType = "image/png";
        else
            throw new InvalidOperationException("file type not supported");

        FileStream fs = new(path, FileMode.Open);
        return new FileStreamResult(fs, contentType);
    }
}
