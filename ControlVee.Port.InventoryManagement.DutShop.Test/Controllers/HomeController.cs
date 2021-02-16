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
    ///  TODO: Disable input on index load with no data.
    /// </summary>
    public class HomeController : Controller
    {
        private List<BatchModel> batches;
        private List<InventoryOnHandModel> expiresNext;
        private List<InventoryOnHandModelByType> invTotalsByType;
        private List<InventoryOnHandModel> invOnHand;
        private readonly string cstring = @"Data Source=(localdb)\mssqllocaldb;Database=DutShop;Integrated Security=True";
        private DataAccess context;
        public MasterModel masterModel = new MasterModel();
        public MasterModel MasterModel

        {
            get
            {
                return masterModel;
            }
            set
            {
                masterModel = value;
            }
        }

        public HomeController()
        {

        }

        public IActionResult Index()
        {


            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);

                batches = context.GetAllBatchesFromDb();
                expiresNext = context.GetExpiresNextByBatchFromDb();
                invOnHand = context.GetOnHandInventoryAllFromDb();
                invTotalsByType = context.GetInventoryTotalsByTypeFromDb();


                masterModel.BatchModels = batches;
                masterModel.ExpiresNextModels = expiresNext;
                masterModel.InventoryOnHandModels = invOnHand;
                masterModel.InventoryOnHandByTypeModel = invTotalsByType;


            };

            return View(masterModel);
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
                

                if (!context.CreateBatchRecordFromDb(createBatchModel.nameOf, createBatchModel.total))
                {
                    // Return to ajax call.
                    throw new System.Exception("Move from batch to inventory failed.");
                }

              

            };

            return Json(JsonConvert.SerializeObject("{ message: \"200\" }"));
        }

        [HttpPost]
        public IActionResult MoveFromBatchToInventoryOnHand(string data)
        {
            var createBatchModel = JsonConvert.DeserializeObject<CreateBatchModel>(data);

            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);

                if (!context.MoveFromBatchToInventoryOnHandDb(createBatchModel.ID))
                {
                    // Return to ajax call.
                    throw new System.Exception("Move from batch to inventory failed.");
                }

                // Why is model null here?
                var batchModelwithId = masterModel.BatchModels.First(i => i.ID == createBatchModel.ID);
                masterModel.BatchModels.Remove(batchModelwithId);
               
            };


            return View("Index");
        }


        public IActionResult GetInventoryOnHandAll()
        {
            invOnHand = new List<InventoryOnHandModel>();
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);

                invOnHand = context.GetOnHandInventoryAllFromDb();

                masterModel.InventoryOnHandModels = invOnHand;
            };
            return View("Index");
        }

        [HttpGet]
        // TODO: Return partial view?
        public IActionResult GetAllBatches()
        {
            batches = new List<BatchModel>();
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);
             
                batches = context.GetAllBatchesFromDb();

                masterModel.BatchModels = batches;
            };

            return Json(JsonConvert.SerializeObject(batches));
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

                masterModel.ExpiresNextModels = expiresNext;
            };

             return Json(JsonConvert.SerializeObject(expiresNext));
        }

        [HttpGet]
        public IActionResult GetInventoryOnHandAllByType()
        {
            invTotalsByType = new List<InventoryOnHandModelByType>();
            using (var connection = new System.Data.SqlClient.SqlConnection())
            {
                connection.ConnectionString = cstring;

                context = new DataAccess(connection);

                invTotalsByType = context.GetInventoryTotalsByTypeFromDb();

                masterModel.InventoryOnHandByTypeModel = invTotalsByType;
               
            };

             return Json(JsonConvert.SerializeObject(invTotalsByType));
        }

        public IActionResult UpdateBatches()
        {
            return View("Index");
        }
    }
}
