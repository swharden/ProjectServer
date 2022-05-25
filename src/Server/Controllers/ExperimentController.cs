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
    public Shared.ExperimentFolder Get(string? path, bool? scan = true)
    {
        Shared.ExperimentFolder experiment = Shared.ExperimentFolder.FromFolderPath(path ?? "X:/", scan ?? true);
        if (string.IsNullOrWhiteSpace(experiment.FolderPath))
            throw new InvalidOperationException();
        return experiment;
    }
}
