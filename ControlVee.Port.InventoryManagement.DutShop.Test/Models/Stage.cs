using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlVee.Port.InventoryManagement.DutShop.Test.Models
{
    public class Stage
    {
        #region staged code





//        <div class="container">





//    <div class="card-header">
//        newest batches: <span id = "isMaxedOut" class="red"></span>
//    </div>


//    <ul class="list-group-horizontal list-unstyled flex-wrap pre-scrollable">

//        @{

//            int k = 0;
//            foreach (BatchModel inv in Model.BatchModels)
//            {

//                <li class="list-group-item border">
//                    <div id = "flexSwitchCompleted" class="form-check form-switch float-right">
//                        <input class="form-check-input" type="checkbox" onclick="$('#moveBatchAlert_@k').show()">
//                        <div class="form-text float-left" id="idOf_@k">@inv.ID</div>
//                        <div id = "moveBatchAlert_@k" class="alert alert-primary collapse" role="alert">
//                            <div id = "alert_idOf_@k" >
//                                < div class="pre">
//                                    do you want to move to inventory?
//                                </div>
//                                <div id = "batchIdInsideRecord" >
//                                    @inv.ID
//                                </ div >



//                                < button type="button" class="btn btn-secondary">no</button>
//                                <button id = "saveButton_@k" type="button" class="btn btn-primary" onclick="moveFromBatchToInventory()">yes</button>
//                            </div>

//                        </div>
//                    </div>

//                    @*Change ID names to have more meaning.*@

//                    <div id = "nameOf_@k" > @inv.NameOf </ div >


//                    < div >
//                        < span class="badge bg-primary rounded-pill" id="batchTotal_@k">@inv.Total</span>
//                    </div>

//                    <div id = "started_@k" > @inv.Started </ div >

//                </ li >
//                k++;

//    }
//}
//    </ ul >


//    < select id = "nameOfDoughnutBatch" class= "form-select" aria - label = "Default select example" >
    
//            < option value = "Classic Glazed" > Classic Glazed </ option >
         
//                 < option value = "Strawberry PopTart" > Strawberry PopTart </ option >
              
//                      < option value = "Chocolatte" > Chocolatte </ option >
               
//                       < option value = "Capn Crunched" > Capn Crunched </ option >
                    
//                            < option value = "Emma noms" > Emma noms </ option >
                         
//                                 < option value = "Shred Tha Gnar" > Shred Tha Gnar</option>
//                                      <option value = "Sugar High" > Sugar High</option>
//                                  </select>
//    <select id = "quantityOfBatch" class= "form-select" aria - label = "Default select example" >
   
//           < option value = "6" > 6 </ option >
    
//            < option value = "12" > 12 </ option >
     
//             < option value = "24" > 24 </ option >
      
//              < option value = "48" > 48 </ option >
       
//           </ select >
       



//           < script type = "text/javascript" >
//                                function getAllBatches() {
//                            // Get all batches.
//                            $.ajax({
//        url: '/Home/GetAllBatches',
//                                type: 'GET',
//                                dataType: 'json',
//                                success: function(data) {
//            if (data.length > 0)
//            {
//                console.log("getting all batchess");
//                console.log(data);
//                var parsedData = JSON.parse(data);
//                var t = JSON.stringify(parsedData);
//                console.log(t);

//                for (let i = 0; i < (parsedData.length); i++)
//                {
//                    console.log(parsedData[i]['ID']);
//                    console.log(parsedData[i]['NameOf']);
//                    console.log(parsedData[i]['Started']);
//                    console.log(parsedData[i]['Total']);
//                    document.getElementById("idOf_" + i).innerHTML = parsedData[i]['ID'];
//                    document.getElementById("nameOf_" + i).innerHTML = parsedData[i]['NameOf'];
//                    document.getElementById("started_" + i).innerHTML = parsedData[i]['Started'];
//                    document.getElementById("batchTotal_" + i).innerHTML = parsedData[i]['Total'];
//                }
//                console.log(parsedData.length);
//                console.log("got batches");
//            }
//        },
//                                error: function(e) {
//            console.log(e);
//        }

//    });
//}
//function createBatch()
//{
//    console.log("inside created batch");
//    var nameOf = document.getElementById("nameOfDoughnutBatch").value;
//    var total = document.getElementById("quantityOfBatch").value;



//                        @*stackoverflow.com / questions / 25050240 / ajax - post - parameter - is -always - null -in -mvc - app *@
//                        $.ajax({
//        type: 'POST',
//                            url: '/Home/CreateBatchRecord',
//                            dataType: 'json',
//                            data: "data=" + JSON.stringify({ id: 0, nameOf: nameOf, total: total }),
//                            success: function() {
//    console.log("created batch");
//    getAllBatches();
//},
//                            error: function() { console.log("FAIL ON CREATE BATCH"); }

//                        });



//                    };

//    </ script >
//    < button id = "createBatch" class= "btn float-right" onclick = "createBatch()" > create batch </ button >
        





//        </ div >






		 //@*<script type = "text/javascript" >

   //         function moveFromBatchToInventory()
   //     {



   //         var batchId = document.getElementById('batchIdInsideRecord').innerHTML;

   //             $.ajax({
   //             type: 'POST',
   //                 url: '/Home/MoveFromBatchToInventoryOnHand',
   //                 dataType: 'json',
   //                 data: "data=" + JSON.stringify({ id: batchId }),
   //                 success: function() {
   //                 console.log("moved batch to inventory");
   //                 updateInfo();
   //             },
   //                 error: function() { console.log("FAIL ON MOVE BATCH"); }

   //         });

   //     };





   //     function updateInfo()
   //     {

   //         getAllBatches();

   //                     //    // Update expires next.
   //                     //    $.ajax({
   //                     //        url: '/Home/GetExpiresNext',
   //                     //        type: 'GET',
   //                     //        dataType: 'json',
   //                     //        success: function (data) {
   //                     //            if (data.length > 0) {
   //                     //                console.log("getting expires next");
   //                     //                var parsedData = JSON.parse(data);

   //     //                for (let i = 0; i < data.length; i++) {
   //     //                    document.getElementById("idOfBatch_Exp_" + i).innerHTML = parsedData[i].BatchId;
   //     //                    document.getElementById("nameOf_Exp_" + i).innerHTML = parsedData[i].NameOf;
   //     //                    document.getElementById("totalOf_Exp_" + i).innerHTML = parsedData[i].Total;
   //     //                    document.getElementById("expOf_Exp_" + i).innerHTML = parsedData[i].Expiration;
   //     //                }

   //     //                console.log("got expires next");
   //     //            }
   //     //            else {
   //     //                console.log("FAILED");
   //     //            }
   //     //        }
   //     //    });

   //     //    // Get totals by type.
   //     //    $.ajax({
   //     //        url: '/Home/GetInventoryOnHandAllByType',
   //     //        type: 'GET',
   //     //        dataType: 'json',
   //     //        success: function (data) {
   //     //            if (data.length > 0) {
   //     //                console.log("gettig on hand by type");
   //     //                var parsedData = JSON.parse(data);

   //     //                for (let i = 0; i < data.length; i++) {
   //     //                    document.getElementById("nameOfType_" + i).innerHTML = parsedData[i].NameOf;
   //     //                    document.getElementById("batchTotalType_" + i).innerHTML = parsedData[i].Total;
   //     //                }

   //     //                console.log("got totals by type");
   //     //            }
   //     //            else {
   //     //                console.log("FAILED");
   //     //            }
   //     //        }
   //     //    });
   //     //};
   //     </ script > *@ 
	#endregion
    }
}
