using ControlVee.Port.InventoryManagement.DutShop.Test.Models;
using System.Collections.Generic;
using System;

namespace ControlVee.Port.InventoryManagement.DutShop.Test
{
    public class DataAccess
    {
        private System.Data.IDbConnection connection;
        private readonly string dbName = "DutShop";
        private readonly string batchesTable = "dbo.BatchesInProgress";
        private readonly string doughtNutsTable = "dbo.Doughnuts";
        private readonly string onHandInventoryTable = "dbo.OnHandInventory";
        private readonly string salesTable = "dbo.Sales";
        private readonly string storedProc_SimulateBatches = "SimulateBatches";
        private readonly string storedProc_GetInventory = "GetOnHandInventory";

        public DataAccess()
        {
        }

        public DataAccess(System.Data.IDbConnection connection)
        { 
            this.connection = connection;
        }

        public bool RunStoredProcSim()
        {
            bool updated = false;

            if (!AssuredConnected())
            {
                using (System.Data.IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = storedProc_SimulateBatches;
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    using (System.Data.IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.RecordsAffected > 0)
                            updated = true;
                    }
                }
            }

            return updated;
        }

        #region DbActions
        public List<InventoryOnHandModel> GetOnHandInventoryFromDb()
        {
            List<InventoryOnHandModel> inv = new List<InventoryOnHandModel>();

            // TODO.
            AssuredConnected();
            using (System.Data.IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = storedProc_GetInventory;
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (System.Data.IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inv.Add(MapTotalOnHandInvetoryToDb(reader));
                    }
                }
            }

            return inv;
        }

        public List<BatchModel> GetBatchesFromDb()
        {
            List<BatchModel> b = new List<BatchModel>();

            // TODO.
            AssuredConnected();
            using (System.Data.IDbCommand command = connection.CreateCommand())
            {
                string text = $"select * from {dbName}.{batchesTable}";
                command.CommandText = text;
                command.CommandType = System.Data.CommandType.Text;

                using (System.Data.IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        b.Add(MapBatchesToDb(reader));
                    }
                }
            }

            return b;
        }

        public BatchModel GetBatchesByIdFromDb(int id)
        {
            // TODO.
            AssuredConnected();
            using (System.Data.IDbCommand command = connection.CreateCommand())
            {
                string text = $"select * from {dbName}.{batchesTable} where ID = {id}";
                command.CommandText = text;
                command.CommandType = System.Data.CommandType.Text;

                // Study the implementation.
                System.Data.IDataReader reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    return null;
                }

                BatchModel b = MapBatchesToDb(reader);
                if (reader.Read())
                {
                    throw new Exception($"Found more than one matching record with name: {id}.");
                }

                return b;
            }
        } 
        #endregion

        #region Db Mappings
        public BatchModel MapBatchesToDb(System.Data.IDataReader reader)
        {
            // TODO.
            BatchModel b = new BatchModel();

            b.ID = (int)reader["ID"];
            b.NameOf = (string)reader["nameOf"];
            b.Total = (int)reader["total"];
            b.Completion = (DateTime)reader["completion"];

            return b;
        }

        public InventoryOnHandModel MapTotalOnHandInvetoryToDb(System.Data.IDataReader reader)
        {
            // TODO.
            InventoryOnHandModel inv = new InventoryOnHandModel();

            inv.NameOf = (string)reader["nameOf"];
            inv.Total = (int)reader["total"];

            return inv;
        } 
        #endregion

        private bool AssuredConnected()
        {
            switch (connection.State)
            {
                case (System.Data.ConnectionState.Closed):
                    connection.Open();
                    return false;

                case (System.Data.ConnectionState.Broken):
                    connection.Close();
                    connection.Open();
                    return false;

                default: return true;
            }
        }
    }
}
