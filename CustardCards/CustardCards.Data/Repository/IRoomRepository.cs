using CustardCards.Data.Entities;

namespace CustardCards.Data.Repository
{
    public interface IRoomRepository
    {
        Task<Room> GetRoom(Guid roomId);
        Task<int> AddRoom(Room room);
        Task<int> AddUserToRoom(User users, Room room);
        Task<int> UpdateModerator(int userId, Room room);
    }
}
