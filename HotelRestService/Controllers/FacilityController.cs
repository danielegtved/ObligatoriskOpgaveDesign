using System.Collections.Generic;
using System.Web.Http;
using HotelModel;
using HotelRestService.DBUtil;

namespace HotelRestService.Controllers
{
    public class FacilityController : ApiController
    {
        ManageFacility mngFacility = new ManageFacility();

        public IEnumerable<Facility> Get()
        {
            return mngFacility.GetAllFacilities();
        }

        public Facility Get(int id)
        {
            return mngFacility.GetFacilityFromId(id);
        }
        public void Post([FromBody]Facility value)
        {
            mngFacility.CreateFacility(value);
        }
        public void Put(int id, [FromBody]Facility value)
        {
            mngFacility.UpdateFacility(value, id);
        }
        public void Delete(int id)
        {
            mngFacility.DeleteFacility(id);
        }
    }
}
