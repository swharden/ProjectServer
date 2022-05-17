using Microsoft.AspNetCore.Mvc;

namespace ProjectServer.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AbfInfoController : ControllerBase
{
    private readonly ILogger<AbfFolderController> _logger;

    public AbfInfoController(ILogger<AbfFolderController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public Shared.AbfInfo Get(string path)
    {
        return Shared.AbfInfo.FromAbf(path);
    }
}
