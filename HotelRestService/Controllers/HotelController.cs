using System.Collections.Generic;
using System.Web.Http;
using HotelModel;
using HotelRestService.DBUtil;

namespace HotelRestService.Controllers
{
    public class HotelController : ApiController
    {
        ManageHotel manageHotel = new ManageHotel();
        
        public IEnumerable<Hotel> Get()
        {
            return manageHotel.GetAllHotel();
        }
        
        public Hotel Get(int id)
        {
            return manageHotel.GetHotelFromId(id);
        }
        
        public void Post([FromBody]Hotel value)
        {
            manageHotel.CreateHotel(value);
        }
        
        public void Put(int id, [FromBody]Hotel value)
        {
            manageHotel.UpdateHotel(value, id);
        }
        
        public void Delete(int id)
        {
            manageHotel.DeleteHotel(id);
        }
    }
}
