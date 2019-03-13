using System.Collections.Generic;
using System.Web.Http;
using HotelModel;
using HotelRestService.DBUtil;

namespace HotelRestService.Controllers
{
    public class GuestController : ApiController
    {
        ManageGuest manageGuest = new ManageGuest();

        public IEnumerable<Guest> Get()
        {
            return manageGuest.GetAllGuest();
        }
        
        public Guest Get(int id)
        {
            return manageGuest.GetGuestFromId(id);
        }
        
        public void Post([FromBody]Guest value)
        {
            manageGuest.CreateGuest(value);
        }
        
        public void Put(int id, [FromBody]Guest value)
        {
            manageGuest.UpdateGuest(value,id);
        }
        
        public void Delete(int id)
        {
            manageGuest.DeleteGuest(id);
        }
    }
}
