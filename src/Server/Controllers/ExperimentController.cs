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
        return Shared.ExperimentFolder.FromFolderPath(
            folderPath: path ?? "X:/",
            scanAbfs: scan ?? true);
    }

    [HttpPut]
    public IActionResult Put(Shared.DTOs.ExperimentInfo experiment)
    {
        string folderPath = Path.GetFullPath(experiment.FolderPath.Replace("\\", "/"));
        if (!Directory.Exists(folderPath))
            return BadRequest($"folder not found: {folderPath}");

        string jsonFilePath = Path.Combine(folderPath, "experiment.json");
        string json = experiment.ToJson();
        System.IO.File.WriteAllText(jsonFilePath, json);
        return NoContent();
    }
}
