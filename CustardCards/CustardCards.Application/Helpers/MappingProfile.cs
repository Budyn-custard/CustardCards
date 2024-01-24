using AutoMapper;
using CustardCards.Data.Entities;
using CustardCards.Models.ViewModels;

namespace CustardCards.Application.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Room, RoomViewModel>().ReverseMap();
            CreateMap<Room, CreatedRoomViewModel>().ReverseMap();
            CreateMap<RoomViewModel, CreatedRoomViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
