using AutoMapper;
using CoOpHub.Core.Dtos;
using CoOpHub.Core.Models;

namespace CoOpHub.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			// Create mappings
			CreateMap<Game, GameDto>();
			CreateMap<ApplicationUser, UserDto>();
			CreateMap<Coop, CoopDto>();
			CreateMap<Notification, NotificationDto>();
		}
	}
}