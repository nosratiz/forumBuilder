using System;
using AutoMapper;
using FourmBuilder.Api.Core.Application.Auth.Command.RegisterCommand;
using FourmBuilder.Api.Core.Application.Roles.Dto;
using FourmBuilder.Api.Core.Application.Users.Command.CreateUser;
using FourmBuilder.Api.Core.Application.Users.Command.UpdateProfile;
using FourmBuilder.Api.Core.Application.Users.Command.UpdateUser;
using FourmBuilder.Api.Core.Application.Users.Dto;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper;
using FourmBuilder.Common.Types;

namespace FourmBuilder.Api.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User

            CreateMap<User, UserDto>()
                .ForMember(x => x.RoleName, opt => opt.MapFrom(des => des.Role.Name));

         
            CreateMap<CreateUserCommand, User>()
                .ForMember(src => src.IsMobileConfirm, opt => opt.MapFrom(des => string.IsNullOrWhiteSpace(des.Mobile)))
                .ForMember(src => src.IsEmailConfirm, opt => opt.MapFrom(des => true))
                .ForMember(src => src.Password, opt => opt.MapFrom(des => PasswordManagement.HashPass(des.Password)))
                .ForMember(src => src.ActiveCode, opt => opt.MapFrom(des => new Random().Next(10000, 99999).ToString()))
                .ForMember(src => src.ModifiedDate, opt => opt.MapFrom(des => DateTimeKind.Local))
                .ForMember(src => src.RegisterDate, opt => opt.MapFrom(des => DateTimeKind.Local));

            CreateMap<UpdateUserCommand, User>()
                .ForMember(src => src.IsMobileConfirm, opt => opt.MapFrom(des => string.IsNullOrWhiteSpace(des.Mobile)))
                .ForMember(src => src.ModifiedDate, opt => opt.MapFrom(des => DateTime.Now));

            CreateMap<PagedResult<User>, PagedResult<UserDto>>();


            CreateMap<RegisterCommand, User>().ForMember(x => x.Password,
                    opt => opt.MapFrom(des => PasswordManagement.HashPass(des.Password)))
               .ForMember(x => x.ExpiredCode, opt => opt.MapFrom(des => DateTime.Now.AddMinutes(215)))
                .ForMember(src => src.ActiveCode, opt => opt.MapFrom(des => new Random().Next(10000, 99999).ToString()))
                .ForMember(x => x.RegisterDate, opt => opt.MapFrom(des => DateTime.Now.AddMinutes(210)))
                .ForMember(x => x.ModifiedDate, opt => opt.MapFrom(des => DateTime.Now.AddMinutes(210)));


            CreateMap<UpdateProfileCommand, User>();

            #endregion

            #region Role

            CreateMap<Role, UserRoleDto>();


            CreateMap<Role, RoleDto>();

            #endregion
        }
    }
}