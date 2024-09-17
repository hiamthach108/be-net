namespace Application.Services;

using AutoMapper;
using Database;
using Core.Jwt;
using Application.Shared.Constant;

public abstract class BaseService
{
  protected readonly TodoDbContext _dbContext;
  protected readonly IMapper _mapper;
  protected readonly IHttpContextAccessor _httpCtx;

  public BaseService(TodoDbContext dbContext, IMapper mapper, IHttpContextAccessor httpCtx)
  {
    _dbContext = dbContext;
    _mapper = mapper;
    _httpCtx = httpCtx;
  }

  protected Payload? ExtractPayload()
  {
    var ctx = _httpCtx.HttpContext;
    if (ctx == null) return null;
    var payload = ctx.Items[JwtConst.PAYLOAD_KEY] as Payload;
    return payload;
  }
}