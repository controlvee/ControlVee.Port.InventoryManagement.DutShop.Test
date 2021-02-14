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
    ///  TODO: S_Proc for dbo.Batches in progress to delete
    ///  after x-time to simulate completion.
    /// </summary>
    public class HomeController : Controller
    {
        private List<BatchModel> batches;
        private List<InventoryOnHandModel> expiresNext;
        private readonly string cstring = @"Data Source=(localdb)\mssqllocaldb;Database=DutShop;Integrated Security=True";
        private DataAccess context;

        public HomeController()
        {
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);
            }
        }

        public IActionResult Index()
        {
            batches = new List<BatchModel>();
           
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);
                
                batches = context.GetJustUpdatedBatchesFromDb();
                expiresNext = context.GetExpiresNextByBatchFromDb();
            };

            ViewBag.NewestBatches = batches.OrderBy(b => b.Completion);
            ViewBag.ExpiresNext = expiresNext;

            return View();
        }
       
       

        // Change to past any time.
        public IActionResult GetBatchesPastFiveMinutes()
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

        [HttpGet]
        // TODO: Returns two obj from S_Proc.
        public IActionResult GetMostRecentBatchesFromS_Proc()
        {
            batches = new List<BatchModel>();
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);

                if (context.RunStoredProcSim())
                {
                    batches = context.GetJustUpdatedBatchesFromDb();
                }

            };

            string json = JsonConvert.SerializeObject(batches);

            return Json(json);
        }

        [HttpGet]
        public IActionResult GetExpiresNext()
        {
            expiresNext = new List<InventoryOnHandModel>();
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);

                expiresNext = context.GetExpiresNextByBatchFromDb();
               
            };

            string json = JsonConvert.SerializeObject(expiresNext);

            return Json(json);
        }


        public IActionResult UpdateBatches()
        {
            return View("Index");
        }
    }
}
