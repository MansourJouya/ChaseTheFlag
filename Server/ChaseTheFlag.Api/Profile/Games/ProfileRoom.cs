using ChaseTheFlag.Domain.Entities.Games; // Importing necessary namespace

namespace ChaseTheFlag.Api.Profile.Games
{
    public class ProfileRoom : AutoMapper.Profile
    {
        public ProfileRoom()
        {
            CreateMap<Room, RegisteredRoom>() // Creating a mapping from Room to RegisteredRoom
                .ForMember(x => x.Id, opt => opt.MapFrom(src => 0)) // Mapping the Id property with a constant value of 0
                .ForMember(x => x.RegistrationDateTime, opt => opt.MapFrom(src => DateTime.Now)) // Mapping the RegistrationDateTime property with the current DateTime
                .ReverseMap(); // Reversing the mapping to allow mapping from RegisteredRoom to Room
        }
    }
}
