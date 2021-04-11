using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication_UI.Models;

namespace WebApplication_UI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        
        public async Task<ActionResult> CaptureInventory()
        {
            string Baseurl = "http://localhost:57984/";
            Product pro = new Product();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Inventory/GetAllRecords");
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                if (Res.IsSuccessStatusCode)
                { 
                    var data = JsonConvert.DeserializeObject<Product>(EmpResponse);
                    pro.ProInfo = data.ProInfo;
                }
            }
            return View("CaptureInventory",pro);
        }  
        public async Task<JsonResult> InsertUpdateItem(Product pro)
        {
            Pdetail data = new Pdetail();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57984");
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Inventory/InsertUpdateItemDetails");

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("ItemId", pro.ItemId.ToString()));
            keyValues.Add(new KeyValuePair<string, string>("Name", pro.Name));
            keyValues.Add(new KeyValuePair<string, string>("Description", pro.Description));
            keyValues.Add(new KeyValuePair<string, string>("Price", pro.Price.ToString()));
            keyValues.Add(new KeyValuePair<string, string>("Action", pro.Action));
            request.Content = new FormUrlEncodedContent(keyValues);
            HttpResponseMessage Res = await client.SendAsync(request);
            var EmpResponse = Res.Content.ReadAsStringAsync().Result;
            if (Res.IsSuccessStatusCode)
            {
                 data = JsonConvert.DeserializeObject<Pdetail>(EmpResponse);               
            }
            JsonResult result = new JsonResult();
            result = this.Json(JsonConvert.SerializeObject(data), JsonRequestBehavior.AllowGet);
            return result;
        }

        public async Task<JsonResult> GetRecordsbyId(Product pro)
        {
            string Baseurl = "http://localhost:57984/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Inventory/GetRecordsbyId?ItemId=" + pro.ItemId);
                var EmpResponse = Res.Content.ReadAsStringAsync().Result; 

                if (Res.IsSuccessStatusCode)
                {
                    var data= JsonConvert.DeserializeObject<Pdetail>(EmpResponse);
                    pro = data.Pro;
                }
            }

            JsonResult result = new JsonResult();
            result = this.Json(JsonConvert.SerializeObject(pro), JsonRequestBehavior.AllowGet);
            return result;
        }

        public async Task<JsonResult> DeleteItem(Product pro)
        {
            Pdetail data = new Pdetail();
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:57984");
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/Inventory/DeleteItemDetails");

            var keyValues = new List<KeyValuePair<string, string>>();
            keyValues.Add(new KeyValuePair<string, string>("ItemId", pro.ItemId.ToString()));
            request.Content = new FormUrlEncodedContent(keyValues);
            HttpResponseMessage Res = await client.SendAsync(request);
            var EmpResponse = Res.Content.ReadAsStringAsync().Result;
            if (Res.IsSuccessStatusCode)
            {
                data = JsonConvert.DeserializeObject<Pdetail>(EmpResponse);
            }
            JsonResult result = new JsonResult();
            result = this.Json(JsonConvert.SerializeObject(data), JsonRequestBehavior.AllowGet);
            return result;
        }

    }
}