using CustardCards.Application.Services;
using CustardCards.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustardCards.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("{roomId}")]
        [Produces(typeof(RoomViewModel))]
        public async Task<IActionResult> Get(Guid roomId)
        {
            var room = await _roomService.GetRoom(roomId);
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> PostRoom(CreatedRoomViewModel room)
        {           
            return Created("Get", await _roomService.AddRoom(room));
        }
    }
}
