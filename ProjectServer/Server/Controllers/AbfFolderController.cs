using Microsoft.AspNetCore.Mvc;

namespace ProjectServer.Server.Controllers;

[ApiController]
[Route("api/AbfFolder")]
public class AbfFolderController : ControllerBase
{
    private readonly ILogger<AbfFolderController> _logger;

    public AbfFolderController(ILogger<AbfFolderController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public Shared.AbfFolder Get(string? path)
    {
        return Shared.AbfFolder.Scan(path ?? "X:/");
    }
}
