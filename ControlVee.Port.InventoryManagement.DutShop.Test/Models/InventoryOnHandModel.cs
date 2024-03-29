﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ControlVee.Port.InventoryManagement.DutShop.Test.Models
{
    [Serializable()]
    public class InventoryOnHandModel
    {
        public int ID { get; set; }
        public string NameOf { get; set; }
        public int Total { get; set; }
        public DateTime? Completion { get; set; }
        public DateTime? Expiration { get; set; }
        public int BatchId { get; set; }

        public InventoryOnHandModel()
        {

        }
    }
}
