using AutoMapper;
using CustardCards.Application.Helpers;
using CustardCards.Application.Services;
using CustardCards.Data.Entities;
using CustardCards.Data.Repository;
using CustardCards.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustardCards.Test
{
    public class RoomServiceTests
    {
        [Fact]
        public async Task AddRoom_WhenRoomFound_ReturnsRoomViewModel()
        {
            var roomRepository = new Mock<IRoomRepository>(MockBehavior.Strict);
            Mock<IMapper> mappingService = new Mock<IMapper>();
            roomRepository.Setup(p=>p.AddRoom(It.IsAny<Room>())).ReturnsAsync(1);
            mappingService
                .Setup(p => p.Map<Room>(It.IsAny<CreatedRoomViewModel>()))
                .Returns((CreatedRoomViewModel input) => new Room
                {
                    Name = input.Name 
                });
            mappingService
                .Setup(p => p.Map<RoomViewModel>(It.IsAny<Room>()))
                .Returns((Room input) => new RoomViewModel
                {
                    Name = input.Name 
                });
            var manager = new RoomService(mappingService.Object, roomRepository.Object);

            var result = await manager.AddRoom(new CreatedRoomViewModel());
            Assert.IsType<RoomViewModel>(result);
        }

        [Fact]
        public async Task AddRoom_WhenSaveChangesDontWork_ThrowsBussinessException()
        {
            var roomRepository = new Mock<IRoomRepository>(MockBehavior.Strict);
            Mock<IMapper> mappingService = new Mock<IMapper>();
            roomRepository.Setup(p => p.AddRoom(It.IsAny<Room>())).ReturnsAsync(0);
            mappingService
                .Setup(p => p.Map<Room>(It.IsAny<CreatedRoomViewModel>()))
                .Returns((CreatedRoomViewModel input) => new Room
                {
                    Name = input.Name
                });
            var manager = new RoomService(mappingService.Object, roomRepository.Object);

            var result = await Assert.ThrowsAsync<BusinessException>(() => manager.AddRoom(new CreatedRoomViewModel()));
            Assert.True(result is BusinessException);
        }

        [Fact]
        public async Task AddUserToRoom_WhenRoomNotFound_ShouldThrowNotFoundException()
        {
            var roomId = Guid.NewGuid();
            var users = new UserViewModel();

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetRoom(roomId)).ReturnsAsync((Room)null);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, User>());
            var mapper = mapperConfiguration.CreateMapper();

            var roomService = new RoomService(mapper, roomRepository.Object);

            await Assert.ThrowsAsync<BusinessException>(() => roomService.AddUserToRoom(users, roomId));
        }

        [Fact]
        public async Task AddUserToRoom_WhenUserAddFails_ShouldThrowInternalServerErrorException()
        {
            var roomId = Guid.NewGuid();
            var users = new UserViewModel();
            var room = new Room(); 

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetRoom(roomId)).ReturnsAsync(room);
            roomRepository.Setup(r => r.AddUserToRoom(It.IsAny<User>(), room)).ReturnsAsync(0);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, User>());
            var mapper = mapperConfiguration.CreateMapper();

            var roomService = new RoomService(mapper, roomRepository.Object);

            await Assert.ThrowsAsync<BusinessException>(() => roomService.AddUserToRoom(users, roomId));
        }

        [Fact]
        public async Task GetRoom_WhenRoomNotFound_ShouldThrowNotFoundException()
        {
            var roomId = Guid.NewGuid();

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetRoom(roomId)).ReturnsAsync((Room)null);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomViewModel>());
            var mapper = mapperConfiguration.CreateMapper();

            var roomService = new RoomService(mapper, roomRepository.Object);

            await Assert.ThrowsAsync<BusinessException>(() => roomService.GetRoom(roomId));
        }

        [Fact]
        public async Task GetRoom_WhenRoomFound_ShouldReturnMappedViewModel()
        {
            var roomId = Guid.NewGuid();
            var room = new Room();

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetRoom(roomId)).ReturnsAsync(room);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomViewModel>());
            var mapper = mapperConfiguration.CreateMapper();

            var roomService = new RoomService(mapper, roomRepository.Object);

            var result = await roomService.GetRoom(roomId);

            Assert.NotNull(result);
            Assert.IsType<RoomViewModel>(result);
        }

        [Fact]
        public async Task UpdateModerator_WhenRoomNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var userId = 1; 
            var roomId = Guid.NewGuid();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomViewModel>());
            var mapper = mapperConfiguration.CreateMapper();

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetRoom(roomId)).ReturnsAsync((Room)null);

            var roomService = new RoomService(mapper, roomRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => roomService.UpdateModerator(userId, roomId));
        }

        [Fact]
        public async Task UpdateModerator_WhenModeratorUpdateFails_ShouldThrowInternalServerErrorException()
        {
            // Arrange
            var userId = 1; 
            var roomId = Guid.NewGuid();
            var room = new Room();
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<Room, RoomViewModel>());
            var mapper = mapperConfiguration.CreateMapper();

            var roomRepository = new Mock<IRoomRepository>();
            roomRepository.Setup(r => r.GetRoom(roomId)).ReturnsAsync(room);
            roomRepository.Setup(r => r.UpdateModerator(userId, room)).ReturnsAsync(0);

            var roomService = new RoomService(mapper, roomRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => roomService.UpdateModerator(userId, roomId));
        }
    }
}
