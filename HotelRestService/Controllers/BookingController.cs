using System.Collections.Generic;
using System.Web.Http;
using HotelModel;
using HotelRestService.DBUtil;

namespace HotelRestService.Controllers
{
    public class BookingController : ApiController
    {
        ManageBooking mngBooking = new ManageBooking();
        
        public IEnumerable<Booking> Get()
        {
            return mngBooking.GetAllBookings();
        }
        
        public Booking Get(int id)
        {
            return mngBooking.GetBookingFromID(id);
        }
        
        public void Post([FromBody]Booking value)
        {
            mngBooking.CreateBooking(value);
        }

        public void Put([FromBody]Booking value, int id)
        {
            mngBooking.UpdateBooking(value, id);
        }
        
        public void Delete(int id)
        {
            mngBooking.DeleteBooking(id);
        }
    }
}
