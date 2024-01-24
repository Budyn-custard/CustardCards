using AutoMapper;
using CustardCards.Application.Helpers;
using CustardCards.Data.Entities;
using CustardCards.Data.Repository;
using CustardCards.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustardCards.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IRoomRepository _roomRepository;

        public RoomService(IMapper mapper, IRoomRepository roomRepository)
        {
            _mapper = mapper;
            _roomRepository = roomRepository;
        }

        public async Task<RoomViewModel> AddRoom(CreatedRoomViewModel room)
        {
            var roomEntity = _mapper.Map<Room>(room);
            var isSaved =  await _roomRepository.AddRoom(roomEntity);
            if (isSaved == 0)
                throw new BusinessException((int)HttpStatusCode.InternalServerError, "Something went wrong.");

            return _mapper.Map<RoomViewModel>(roomEntity);
        }

        public async Task AddUserToRoom(UserViewModel users, Guid roomId)
        {
            var room = await _roomRepository.GetRoom(roomId);
            if(room == null)
                throw new BusinessException((int)HttpStatusCode.NotFound, roomId);
            
            var addUserResponse = await _roomRepository.AddUserToRoom(_mapper.Map<User>(users), room);
            if(addUserResponse == 0)
                throw new BusinessException((int)HttpStatusCode.InternalServerError, "Something went wrong.");
        }

        public async Task<RoomViewModel> GetRoom(Guid roomId)
        {
            var room = await _roomRepository.GetRoom(roomId).ConfigureAwait(false);
            if(room == null)
                throw new BusinessException((int)HttpStatusCode.NotFound, roomId);

            return _mapper.Map<RoomViewModel>(room);
        }

        public async Task UpdateModerator(int userId, Guid roomId)
        {
            var room = await _roomRepository.GetRoom(roomId);
            if (room == null)
                throw new BusinessException((int)HttpStatusCode.NotFound, roomId);

            var updateModeratorResponse = await _roomRepository.UpdateModerator(userId, room);
            if(updateModeratorResponse == 0)
                throw new BusinessException((int)HttpStatusCode.InternalServerError, "Something went wrong.");
        }
    }
}
