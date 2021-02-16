using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlVee.Port.InventoryManagement.DutShop.Test.Models
{
    public class MasterModel
    {
        public List<BatchModel> BatchModels { get; set; }
        public List<InventoryOnHandModel>? ExpiresNextModels { get; set; }
        public List<InventoryOnHandModel>? InventoryOnHandModels { get; set; }
        public List<InventoryOnHandModelByType>? InventoryOnHandByTypeModel { get; set; }
    }
}
