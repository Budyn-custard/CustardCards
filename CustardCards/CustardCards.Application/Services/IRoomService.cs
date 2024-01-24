using CustardCards.Data.Entities;
using CustardCards.Models.ViewModels;

namespace CustardCards.Application.Services
{
    public interface IRoomService
    {
        Task<RoomViewModel> GetRoom(Guid roomId);
        Task<RoomViewModel> AddRoom(CreatedRoomViewModel room);
        Task AddUserToRoom(UserViewModel users, Guid roomId);
        Task UpdateModerator(int userId, Guid roomId);
    }
}
