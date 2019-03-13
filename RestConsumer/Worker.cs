using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables.Core;
using HotelModel;
using Newtonsoft.Json;

namespace RestConsumer
{
    class Worker
    {
        private const string URI = "http://localhost:2265/api/Facility";

        public void Start()
        {
            List<Facility> facilities = GetAllFacilities();

            foreach (Facility facility in facilities)
            {
                Console.WriteLine(facility.Hotel_No);
            }

            var table = new ConsoleTable("Hotel_No", "Bar", "Swimming_Pool", "Table_Tennis", "Pool_Table", "Restaurant");

            foreach (Facility facility in facilities)
            {
                table.AddRow(facility.Hotel_No, facility.Bar, facility.SwimmingPool, facility.TableTennis, facility.PoolTable, facility.Restaurant);
            }
            table.Write(Format.Default);

            DeleteFacility(5);
            CreateFacility(new Facility(5, 't','t','t','t','t'));
            GetAllFacilities();


        }

        private List<Facility> GetAllFacilities()
        {
            List<Facility> facilities = new List<Facility>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                String jsonStr = resTask.Result;

                facilities = JsonConvert.DeserializeObject<List<Facility>>(jsonStr);
            }
            return facilities;
        }

        private Facility GetOneFacility(int id)
        {
            Facility facility = new Facility();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI + "/" + id);
                String jsonStr = resTask.Result;

                facility = JsonConvert.DeserializeObject<Facility>(jsonStr);
            }
            return facility;
        }
        private bool DeleteFacility(int id)
        {
            bool ok = true;

            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(URI + "/" + id);

                HttpResponseMessage resp = deleteAsync.Result;
                if (!resp.IsSuccessStatusCode)
                {
                    ok = false;
                }
            }
            return ok;
        }

        private bool CreateFacility(Facility facility)
        {
            bool ok = true;

            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(facility);
                StringContent content = new StringContent(jsonStr, Encoding.ASCII, "application/json");

                Task<HttpResponseMessage> postAsync = client.PostAsync(URI, content);

                HttpResponseMessage resp = postAsync.Result;
                if (!resp.IsSuccessStatusCode)
                {
                    ok = false;
                }
            }
            return ok;
        }

        private bool UpdateFacility(int id, Facility facility)
        {
            bool ok = true;

            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(facility);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> putAsync = client.PutAsync(URI + "/" + id, content);

                HttpResponseMessage resp = putAsync.Result;
                if (!resp.IsSuccessStatusCode)
                {
                    ok = false;
                }
            }
            return ok;
        }
    }
}
