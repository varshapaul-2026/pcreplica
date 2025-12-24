using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcReplica.API.DTOs;
using PcReplica.Core.Entities;
using PcReplica.Core.Enums;
using PcReplica.Infrastructure.Data;

namespace PcReplica.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(ApplicationDbContext context, ILogger<ProjectsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        try
        {
            var projects = await _context.Projects
                .Include(p => p.Company)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Location = p.Location,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status.ToString(),
                    Budget = p.Budget,
                    CompanyId = p.CompanyId,
                    CompanyName = p.Company.Name
                })
                .ToListAsync();

            return Ok(projects);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving projects");
            return StatusCode(500, new { message = "An error occurred while retrieving projects" });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(int id)
    {
        try
        {
            var project = await _context.Projects
                .Include(p => p.Company)
                .Where(p => p.Id == id)
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Location = p.Location,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Status = p.Status.ToString(),
                    Budget = p.Budget,
                    CompanyId = p.CompanyId,
                    CompanyName = p.Company.Name
                })
                .FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound(new { message = "Project not found" });
            }

            return Ok(project);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving project {ProjectId}", id);
            return StatusCode(500, new { message = "An error occurred while retrieving the project" });
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin,ProjectManager")]
    public async Task<ActionResult<ProjectDto>> CreateProject([FromBody] CreateProjectDto dto)
    {
        try
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = (ProjectStatus)dto.Status,
                Budget = dto.Budget,
                CompanyId = dto.CompanyId
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            var company = await _context.Companies.FindAsync(dto.CompanyId);

            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Location = project.Location,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status.ToString(),
                Budget = project.Budget,
                CompanyId = project.CompanyId,
                CompanyName = company?.Name
            };

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, projectDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating project");
            return StatusCode(500, new { message = "An error occurred while creating the project" });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,ProjectManager")]
    public async Task<ActionResult<ProjectDto>> UpdateProject(int id, [FromBody] UpdateProjectDto dto)
    {
        try
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound(new { message = "Project not found" });
            }

            project.Name = dto.Name;
            project.Description = dto.Description;
            project.Location = dto.Location;
            project.StartDate = dto.StartDate;
            project.EndDate = dto.EndDate;
            project.Status = (ProjectStatus)dto.Status;
            project.Budget = dto.Budget;
            project.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var company = await _context.Companies.FindAsync(project.CompanyId);

            var projectDto = new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                Location = project.Location,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status.ToString(),
                Budget = project.Budget,
                CompanyId = project.CompanyId,
                CompanyName = company?.Name
            };

            return Ok(projectDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating project {ProjectId}", id);
            return StatusCode(500, new { message = "An error occurred while updating the project" });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        try
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null)
            {
                return NotFound(new { message = "Project not found" });
            }

            project.IsDeleted = true;
            project.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting project {ProjectId}", id);
            return StatusCode(500, new { message = "An error occurred while deleting the project" });
        }
    }
}
