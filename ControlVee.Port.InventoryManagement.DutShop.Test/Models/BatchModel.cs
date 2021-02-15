using System;
using System.Collections.Generic;
using System.Text;

namespace ControlVee.Port.InventoryManagement.DutShop.Test.Models
{
    [Serializable()]
    public class BatchModel 
    {
        public int ID { get; set; }
        public string NameOf { get; set; }
        public int? Total { get; set; }
        public DateTime? Started { get; set; }

        public BatchModel()
        {
        }
    }
}
