using AutoMapper;
using CustardCards.API.Controllers;
using CustardCards.Application.Helpers;
using CustardCards.Application.Services;
using CustardCards.Data.Entities;
using CustardCards.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace CustardCards.Test
{
    public class CustardControllerTests
    {
        [Fact]
        public async Task GetReturns_Ok()
        {
            //arrange
            Mock<IRoomService> roomService = new Mock<IRoomService>(MockBehavior.Strict);
            roomService.Setup(p => p.GetRoom(It.IsAny<Guid>())).ReturnsAsync(new RoomViewModel());
            var controller = new RoomsController(roomService.Object);
            //act;
            var result = await controller.Get(Guid.NewGuid());
            //assert

            Assert.True(result is OkObjectResult);
        }

        [Fact]
        public async Task GetReturns_NotFound()
        {
            //arrange
            Mock<IRoomService> roomService = new Mock<IRoomService>(MockBehavior.Strict);
            roomService.Setup(p => p.GetRoom(It.IsAny<Guid>())).ThrowsAsync(new BusinessException((int)HttpStatusCode.NotFound, ""));
            var controller = new RoomsController(roomService.Object);
            //act;
            var result = await Assert.ThrowsAsync< BusinessException>(() => controller.Get(Guid.NewGuid()));
            //assert

            Assert.True(result is BusinessException);
        }

        [Fact]
        public async Task PostReturns_Created()
        {
            //arrange
            Mock<IRoomService> roomService = new Mock<IRoomService>(MockBehavior.Strict);
            roomService.Setup(p => p.AddRoom(It.IsAny<CreatedRoomViewModel>())).ReturnsAsync(new RoomViewModel());
            var controller = new RoomsController(roomService.Object);
            //act;
            var result = await controller.PostRoom(new CreatedRoomViewModel());
            //assert

            Assert.True(result is CreatedResult);
        }
    }
}