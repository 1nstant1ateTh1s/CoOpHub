using AutoMapper;
using CoOpHub.Dtos;
using CoOpHub.Models;

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