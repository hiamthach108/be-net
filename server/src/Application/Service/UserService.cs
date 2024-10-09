using Application.DTOs.Pagination;
using Application.DTOs.User;
using Application.DTOs.Users;
using Application.Services;
using Application.Shared.Enum;
using Application.Shared.Type;
using AutoMapper;
using Azure;
using Database;
using Domain.Users;
using Application.Shared.Constant;
using Microsoft.AspNetCore.Mvc;
using Repository.Users;

namespace Application.Service;

public interface IUserService
{
  Task<IActionResult> HandleGetAllAsync(UserQuery query);
  Task<IActionResult> HandleGetByIdAsync(Guid id);
  Task<IActionResult> HandleCreateAsync(UserCreateDto dto);
  Task<IActionResult> HandleUpdateAsync(Guid id, UserUpdateDto dto);
  Task<IActionResult> HandleDeleteAsync(Guid id);
  Task<IActionResult> HandleGetProfileAsync();
}

public class UserService : BaseService, IUserService
{
  private readonly IUserRepository _repository;

  public UserService(TodoDbContext dbContext, IMapper mapper, IHttpContextAccessor httpCtx) : base(dbContext, mapper, httpCtx)
  {
    _repository = new UserRepository(dbContext);
  }

  public async Task<IActionResult> HandleCreateAsync(UserCreateDto dto)
  {
    var user = _mapper.Map<User>(dto);

    user.Status = UserStatusEnum.Active.ToString();
    user.RoleId = new Guid(UserConst.DEFAULT_ROLE_ADMIN_ID);

    await _repository.AddAsync(user);

    return SuccessResp.Created("User created successfully");
  }

  public async Task<IActionResult> HandleDeleteAsync(Guid id)
  {
    var user = await _repository.GetByIdAsync(id);

    if (user == null)
    {
      return ErrorResp.NotFound("User not found");
    }

    await _repository.DeleteAsync(user);

    return SuccessResp.Ok("User deleted successfully");
  }

  public async Task<IActionResult> HandleGetAllAsync(UserQuery query)
  {
    var resp = await _repository.GetUserListAsync(query);

    var users = _mapper.Map<IEnumerable<UserDto>>(resp.Users);

    var result = new
    {
      Data = users,
      Total = resp.Total,
      Page = query.Page,
      PageSize = query.PageSize
    };

    return SuccessResp.Ok(result);
  }

  public async Task<IActionResult> HandleGetByIdAsync(Guid id)
  {
    var user = await _repository.GetByIdAsync(id);

    if (user == null)
    {
      return ErrorResp.NotFound("User not found");
    }

    var result = _mapper.Map<UserDto>(user);

    return SuccessResp.Ok(result);
  }

  public async Task<IActionResult> HandleUpdateAsync(Guid id, UserUpdateDto dto)
  {
    var user = await _repository.GetByIdAsync(id);

    if (user == null)
    {
      return ErrorResp.NotFound("User not found");
    }

    _mapper.Map(dto, user);

    await _repository.UpdateAsync(id, user);

    return SuccessResp.Ok("User updated successfully");
  }

  public async Task<IActionResult> HandleGetProfileAsync()
  {
    var payload = ExtractPayload();
    if (payload == null)
    {
      return ErrorResp.Unauthorized("Invalid token");
    }

    var user = await _repository.GetByIdAsync(payload.UserId);

    if (user == null)
    {
      return ErrorResp.NotFound("User not found");
    }

    var result = _mapper.Map<UserDto>(user);

    return SuccessResp.Ok(result);
  }
}