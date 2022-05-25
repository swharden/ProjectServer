using Microsoft.AspNetCore.Mvc;

namespace ProjectServer.Server.Controllers;

[ApiController]
[Route("api/Project")]
public class ProjectController : ControllerBase
{
    private readonly ILogger<AbfFolderController> Logger;

    public ProjectController(ILogger<AbfFolderController> logger)
    {
        Logger = logger;
    }

    [HttpGet]
    public Shared.ProjectFolder Get(string? path)
    {
        Shared.ProjectFolder project = Shared.ProjectFolder.Load(path ?? "X:/");

        if (project.Exists)
        {
            Logger.LogInformation($"Project '{project.Title}': {path}");
        }
        else
        {
            Logger.LogInformation($"Project does not exist: {path}");
        }

        return project;
    }
}
