using ControlVee.Port.InventoryManagement.DutShop.Test.Models;
using ControlVee.Port.InventoryManagement.DutShop.Test;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Newtonsoft.Json;
using System;

namespace ControlVee.Port.InventoryManagement.DutShop.Test.Controllers
{
    /// <summary>
    ///  TODO: Handles nulls in Models and Db.
    ///  TODO: Add getallinventory with buttons to delete.
    ///  TODO: Save S_Procs and clean up.
    ///  TODO: S_Procs for all.
    ///  TODO: S_Proc for dbo.Batches in progress to delete
    ///  after x-time to simulate completion.
    ///  TODO: 0 (zero) in S_Proc.
    ///  TODO: Organize references to CSS and .JS.
    ///  TODO: ONHandInventory table foreign key.
    ///  TODO: Handle user input create batch for null or bad values.
    /// </summary>
    public class HomeController : Controller
    {
        private List<BatchModel> batches;
        private List<InventoryOnHandModel> expiresNext;
        private List<InventoryOnHandModelByType> invTotalsByType;
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
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);
                
                batches = context.GetAllBatchesFromDb();
                expiresNext = context.GetExpiresNextByBatchFromDb();
                invTotalsByType = context.GetInventoryTotalsByType();
            };

            // TODO: Sort here or below methods?  BOTH?
            ViewBag.NewestBatches = batches.OrderBy(b => b.Started);
            ViewBag.ExpiresNext = expiresNext.OrderBy(b => b.Expiration);
            ViewBag.InvTotalsByType = invTotalsByType.OrderBy(b => b.NameOf);

            return View();
        }

      
        [HttpPost]
        public IActionResult CreateBatchRecord(string data)
        {
            // TODO: Handle unterminated string exc.
            // TODO: Click twice to update batches?
            var createBatchModel = JsonConvert.DeserializeObject<CreateBatchModel>(data);

            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);
                context.CreateBatchRecord(createBatchModel.nameOf, createBatchModel.total); 
            };

            return Json(JsonConvert.SerializeObject("{ message: \"OK200\" }"));
        }

        [HttpPost]
        public IActionResult MoveFromBatchToInventoryOnHand(int batchId, string nameOf, int totalMade)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);

                if (!context.MoveFromBatchToInventoryOnHandDb(batchId, nameOf, totalMade))
                {
                    throw new System.Exception("Move from batch to inventory failed.");
                }
            };

            GetAllBatchesFromS_Proc();
            return View("Index");
        }


        public IActionResult GetInventoryOnHandAll()
        {
            List<InventoryOnHandModel> inv = new List<InventoryOnHandModel>();
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);

                inv = context.GetOnHandInventoryAllFromDb();
                ViewBag.Inventory = inv;
            };
            return View("Index");
        }

        [HttpGet]
        public IActionResult GetAllBatchesFromS_Proc()
        {
            batches = new List<BatchModel>();
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);
             
                batches = context.GetAllBatchesFromDb();

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

            string json = JsonConvert.SerializeObject(expiresNext.OrderBy(b => b.Expiration));

            return Json(json);
        }

        [HttpGet]
        public IActionResult GetInventoryOnHandAllByType()
        {
            List<InventoryOnHandModelByType> inv = new List<InventoryOnHandModelByType>();
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);

                inv = context.GetInventoryTotalsByType();
                ViewBag.Inventory = inv;
            };
            string json = JsonConvert.SerializeObject(inv);

            return Json(json);
        }

        public IActionResult UpdateBatches()
        {
            return View("Index");
        }
    }
}
