namespace TodoApi.Controllers;

using Application.DTOs.User;
using Application.DTOs.Users;
using Application.Middlewares;
using Application.Service;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/v1/users")]
public class UserController : ControllerBase
{

  private readonly ILogger<UserController> _logger;
  private readonly IUserService _service;

  public UserController(ILogger<UserController> logger, IUserService service)
  {
    _logger = logger;
    _service = service;
  }

  [HttpGet("")]
  public async Task<IActionResult> Get([FromQuery] UserQuery query)
  {
    _logger.LogInformation("Get all users request received");

    return await _service.HandleGetAllAsync(query);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(Guid id)
  {
    _logger.LogInformation("Get user by id request received");

    return await _service.HandleGetByIdAsync(id);
  }

  [HttpPost("")]
  public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
  {
    _logger.LogInformation("Create user request received");

    return await _service.HandleCreateAsync(dto);
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> Update(Guid id, [FromBody] UserUpdateDto dto)
  {
    _logger.LogInformation("Update user request received");

    return await _service.HandleUpdateAsync(id, dto);
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> Delete(Guid id)
  {
    _logger.LogInformation("Delete user request received");

    return await _service.HandleDeleteAsync(id);
  }

  [Protected]
  [HttpGet("profile")]
  public async Task<IActionResult> GetProfile()
  {
    _logger.LogInformation("Get user profile request received");

    return await _service.HandleGetProfileAsync();
  }
}
