using Microsoft.AspNetCore.Mvc;

namespace ProjectServer.Server.Controllers;

[ApiController]
[Route("api/Experiment")]
public class ExperimentController : ControllerBase
{
    private readonly ILogger<AbfFolderController> _logger;

    public ExperimentController(ILogger<AbfFolderController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public Shared.ExperimentFolder Get(string? path)
    {
        return Shared.ExperimentFolder.Scan(path ?? "X:/");
    }
}
