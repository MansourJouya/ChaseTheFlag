


using ChaseTheFlag.Domain.Entities.Users; // Importing necessary namespace

namespace ChaseTheFlag.Api.Profile.Users
{
    public class ProfileUser : AutoMapper.Profile
    {
        private readonly Random random = new(); // Initializing a Random object

        public ProfileUser()
        {
            CreateMap<RegisteredUserData, RegisteredUser>() // Creating a mapping from RegisteredUserData to RegisteredUser
                .ForMember(x => x.Id, opt => opt.MapFrom(src => 0)) // Mapping the Id property with a constant value of 0
                .ForMember(x => x.RegistrationDateTime, opt => opt.MapFrom(src => DateTime.Now)) // Mapping the RegistrationDateTime property with the current DateTime
                .ForMember(x => x.PlayerTag, opt => opt.MapFrom(src => ((char)random.Next(65, 91)).ToString()))  // Generating a random player tag and mapping it to the PlayerTag property
                .ReverseMap(); // Reversing the mapping to allow mapping from RegisteredUser to RegisteredUserData
        }
    }
}
