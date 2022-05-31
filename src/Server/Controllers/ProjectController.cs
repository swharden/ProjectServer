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
        project.ScanAndLoadExperiments();
        return project;
    }

    [HttpPut]
    public IActionResult Put(Shared.DTOs.ProjectInfo project)
    {
        string folderPath = Path.GetFullPath(project.FolderPath.Replace("\\", "/"));
        if (!Directory.Exists(folderPath))
            return BadRequest($"folder not found: {folderPath}");

        string jsonFilePath = Path.Combine(folderPath, "project.json");
        string json = project.ToJson();
        System.IO.File.WriteAllText(jsonFilePath, json);
        return NoContent();
    }
}
