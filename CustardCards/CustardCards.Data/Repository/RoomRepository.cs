using CustardCards.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustardCards.Data.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly CustardCardsDbContext _context;
        public RoomRepository(CustardCardsDbContext context)
        {

            _context = context;

        }
        public Task<int> AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            return _context.SaveChangesAsync();
        }

        public Task<int> AddUserToRoom(User users, Room room)
        {
            room.Users.Add(users);
            return _context.SaveChangesAsync();
        }

        public Task<Room> GetRoom(Guid roomId)
        {
            return _context.Rooms.Include(p=>p.Users).FirstOrDefaultAsync(p=>p.Id == roomId);
        }

        public Task<int> UpdateModerator(int userId, Room room)
        {
            room.Users.Where(p => p.IsModerator).FirstOrDefault().IsModerator = false;
            room.Users.Where(p => p.Id == userId).FirstOrDefault().IsModerator = true;

            return _context.SaveChangesAsync();
        }
    }
}
