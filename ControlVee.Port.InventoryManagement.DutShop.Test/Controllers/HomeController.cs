using ControlVee.Port.InventoryManagement.DutShop.Test.Models;
using ControlVee.Port.InventoryManagement.DutShop.Test;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Newtonsoft.Json;

namespace ControlVee.Port.InventoryManagement.DutShop.Test.Controllers
{
    /// <summary>
    ///  TODO: Handles nulls in Models and Db.
    ///  TODO: Save SProcs and clean up.
    /// </summary>
    public class HomeController : Controller
    {
        private List<BatchModel> batches;
        private List<InventoryOnHandModel> inv;
        private readonly string cstring = @"Data Source=(localdb)\mssqllocaldb;Database=DutShop;Integrated Security=True";
        private DataAccess context;
        
        public HomeController()
        {
           
        }

        public IActionResult Index()
        {
            batches = new List<BatchModel>();
            inv = new List<InventoryOnHandModel>();

            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);
                
                batches = context.GetJustUpdatedBatchesFromDb();
               
                batches.OrderBy(b => b.Completion);
                inv = context.GetOnHandInventoryFromDb();
            };

            ViewBag.Batches = batches;
            ViewBag.Inventory = inv;

            return View();
        }
       
        

        public IActionResult GetBatches()
        {
            batches = new List<BatchModel>();
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);

                batches = context.GetJustUpdatedBatchesFromDb();
            };

            return View("Index"); 
        }

        public IActionResult GetInventoryOnHand()
        {
            List<InventoryOnHandModel> inv = new List<InventoryOnHandModel>();
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);

                inv = context.GetOnHandInventoryFromDb();
                ViewBag.Inventory = inv;
            };
            return View("Index");
        }

        public IActionResult UpdateBatches()
        {
            return View("Index");
        }

        [HttpGet]
        public IActionResult StartRandomBatches()
        {
            batches = new List<BatchModel>();
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);
                // Randomize here.
                batches = context.RunStoredProcSim();
            };

            string json = JsonConvert.SerializeObject(batches);

            return Json(json);
        }
    }
}
