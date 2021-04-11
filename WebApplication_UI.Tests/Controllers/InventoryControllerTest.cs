using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication_UI;
using WebApplication_UI.Controllers;
using System.Threading.Tasks;
using WebApplication_UI.Models;
using Newtonsoft.Json;

namespace WebApplication_UI.Tests.Controllers
{
    [TestClass]
    public class InventoryControllerTest
    {
        [TestMethod]
        public async Task Test_CaptureInventory()
        {
           
            InventoryController controller = new InventoryController();
            Product pro = new Product();
            var result = await controller.CaptureInventory();
            ViewResult viewResult = (ViewResult)result;
            Assert.AreEqual("CaptureInventory", viewResult.ViewName);
            Assert.IsNotNull(viewResult.Model);
        }

        [TestMethod]
        public async Task Test_InsertUpdateItem()
        {
            
            InventoryController controller = new InventoryController();
            Product pro = new Product();
            pro.ItemId = 4;
            pro.Name = "Dress";
            pro.Description = "Lux Brand";
            pro.Price = 38.50M;
            pro.Action = "Save";
            var data = "";
            var result = await controller.InsertUpdateItem(pro);
            data= result.Data.ToString();
            Pdetail response = Newtonsoft.Json.JsonConvert.DeserializeObject<Pdetail>(data);
            Assert.AreEqual("success", response.Output.ToLower());
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task Test_GetRecordsbyId()
        {
            InventoryController controller = new InventoryController();
            Product pro = new Product();
            pro.ItemId = 1;
            var data = "";
            var result = await controller.GetRecordsbyId(pro);
            data = result.Data.ToString();
            pro = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(data);
            Assert.IsNotNull(pro);
        }

        [TestMethod]
        public async Task Test_DeleteItem()
        {
           
            InventoryController controller = new InventoryController();
            Product pro = new Product();
            pro.ItemId = 2;
            var data = "";
            var result = await controller.DeleteItem(pro);
            data = result.Data.ToString();
            Pdetail response = Newtonsoft.Json.JsonConvert.DeserializeObject<Pdetail>(data);
            Assert.AreEqual("deleted", response.Output.Trim().ToLower());
            Assert.IsNotNull(response);
        }

        
    }
}
