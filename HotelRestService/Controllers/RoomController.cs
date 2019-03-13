using System.Collections.Generic;
using System.Web.Http;
using HotelModel;
using HotelRestService.DBUtil;

namespace HotelRestService.Controllers
{
    public class RoomController : ApiController
    {
        private readonly ManageRoom _mngRoom = new ManageRoom();
        
        public IEnumerable<Room> Get()
        {
            return _mngRoom.GetAllRooms();
        }
        
        public Room Get(int roomNr, int hotelNr)
        {
            return _mngRoom.GetRoomFromID(roomNr, hotelNr);
        }
        
        public void Post([FromBody]Room value)
        {
            _mngRoom.CreateRoom(value);
        }
        
        public void Put([FromBody]Room value, int roomNr, int hotelNr)
        {
            _mngRoom.UpdateRoom(value, roomNr, hotelNr);
        }
        
        public void Delete(int roomNr, int hotelNr)
        {
            _mngRoom.DeleteRoom(roomNr, hotelNr);
        }
    }
}
