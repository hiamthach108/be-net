namespace Application.DTOs.Users;

using AutoMapper;
using Domain.Users;


public class UserDto
{
  public Guid Id { get; set; }
  public string FullName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string? Phone { get; set; }
  public string? Status { get; set; }
}

public class UserProfile : Profile
{
  public UserProfile()
  {
    CreateMap<User, UserDto>();
    CreateMap<UserDto, User>();

    // ignore null
    CreateMap<User, UserCreateDto>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    CreateMap<UserCreateDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

    CreateMap<User, UserUpdateDto>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    CreateMap<UserUpdateDto, User>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
  }
}