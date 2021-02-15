using ControlVee.Port.InventoryManagement.DutShop.Test.Models;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Data;

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
        private readonly string storedProc_CreateBatchRecord = "CreateBatchRecord";
        private readonly string storedProc_GetInventoryTotalsByType = "GetOnHandInventoryTotalsByType";
        private readonly string storedProc_GetExpiresNext = "GetInventoryTotals_Expire_MIN";
        private readonly string storedProc_GetAllBatches = "GetAllBatches";
        private readonly string storedProc_MoveFromBatchToOnHandInventory = "MoveFromBatchToOnHandInventory";
        private List<BatchModel> batches;
        private List<InventoryOnHandModel> inv;
        private BatchModel batch;

        public DataAccess()
        {
            batches = new List<BatchModel>();
            inv = new List<InventoryOnHandModel>();
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
        public List<InventoryOnHandModel> GetOnHandInventoryAllFromDb()
        {
            List<InventoryOnHandModel> inv = new List<InventoryOnHandModel>();

            // TODO.
            AssuredConnected();
            using (System.Data.IDbCommand command = connection.CreateCommand())
            {
                string text = $"select * from {dbName}.{onHandInventoryTable} ";
                command.CommandText = text;
                command.CommandType = System.Data.CommandType.Text;
                

                using (System.Data.IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inv.Add(MapTotalOnHandInvetoryAllToDb(reader));
                    }
                }
            }

            return inv;
        }

        public bool MoveFromBatchToInventoryOnHandDb(int batchId, string nameOf, int totalMade)
        {
            bool updated = false;

            AssuredConnected();
            using (System.Data.IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = storedProc_MoveFromBatchToOnHandInventory;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // Add input parameter.
                SqlParameter parameter = new SqlParameter();
                parameter.ParameterName = "@batchId";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = batchId;
                // Add input parameter.
                SqlParameter parameterB = new SqlParameter();
                parameter.ParameterName = "@nameOf";
                parameter.SqlDbType = SqlDbType.NVarChar;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = nameOf;
                // Add input parameter.
                SqlParameter parameterC = new SqlParameter();
                parameter.ParameterName = "@totalMade";
                parameter.SqlDbType = SqlDbType.Int;
                parameter.Direction = ParameterDirection.Input;
                parameter.Value = totalMade;

                using (System.Data.IDataReader reader = command.ExecuteReader())
                {
                    if (reader.RecordsAffected > 0)
                        updated = true;
                }
            }

            return updated;
        }

        internal bool CreateBatchRecord(int batchId, string nameOf, int total)
        {
            bool updated = false;

            AssuredConnected();
            using (System.Data.IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = storedProc_CreateBatchRecord;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // Add input parameter.
                SqlParameter parameterBatchId = new SqlParameter();
                parameterBatchId.ParameterName = "@batchId";
                parameterBatchId.SqlDbType = SqlDbType.Int;
                parameterBatchId.Direction = ParameterDirection.Input;
                parameterBatchId.Value = batchId;
                // Add input parameter.
                SqlParameter parameterNameOf = new SqlParameter();
                parameterNameOf.ParameterName = "@nameOf";
                parameterNameOf.SqlDbType = SqlDbType.NVarChar;
                parameterNameOf.Direction = ParameterDirection.Input;
                parameterNameOf.Value = nameOf;
                // Add input parameter.
                SqlParameter parameterTotalMade = new SqlParameter();
                parameterTotalMade.ParameterName = "@totalMade";
                parameterTotalMade.SqlDbType = SqlDbType.Int;
                parameterTotalMade.Direction = ParameterDirection.Input;
                parameterTotalMade.Value = total;

                command.Parameters.Add(parameterBatchId);
                command.Parameters.Add(parameterNameOf);
                command.Parameters.Add(parameterTotalMade);

                using (System.Data.IDataReader reader = command.ExecuteReader())
                {
                    if (reader.RecordsAffected > 0)
                        updated = true;
                }
            }

            return updated;
        }

        public List<BatchModel> GetAllBatchesFromDb()
        {
            batches = new List<BatchModel>();

            AssuredConnected();
            using (System.Data.IDbCommand command = connection.CreateCommand())
            {
                string text = storedProc_GetAllBatches;
                command.CommandText = text;
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (System.Data.IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        batches.Add(MapBatchesFromDb(reader));
                    }
                }
            }

            return batches;
        }

        public List<InventoryOnHandModel> GetExpiresNextByBatchFromDb()
        {
            inv = new List<InventoryOnHandModel>();

            // TODO.
            AssuredConnected();
            using (System.Data.IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = storedProc_GetExpiresNext;
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (System.Data.IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inv.Add(MapTotalOnHandInvetoryAllToDb(reader));
                    }
                }
            }

            return inv;
        }

        public List<InventoryOnHandModelByType> GetInventoryTotalsByType()
        {
            List<InventoryOnHandModelByType> inv = new List<InventoryOnHandModelByType>();

            // TODO.
            AssuredConnected();
            using (System.Data.IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = storedProc_GetInventoryTotalsByType;
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (System.Data.IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inv.Add(MapTotalOnHandInvetoryAllByTypeToDb(reader));
                    }
                }
            }

            return inv;
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

                BatchModel b = MapBatchesFromDb(reader);
                if (reader.Read())
                {
                    throw new Exception($"Found more than one matching record with name: {id}.");
                }

                return b;
            }
        } 
        #endregion

        #region Db Mappings
        public BatchModel MapBatchesFromDb(System.Data.IDataReader reader)
        {
            // TODO.
            batch = new BatchModel();
            batch.ID = (int)reader["ID"];
            batch.NameOf = (string)reader["nameOf"];
            batch.Total = (int)reader["total"];
            batch.Started = (DateTime)reader["started"];

            return batch;
        }

        public InventoryOnHandModel MapTotalOnHandInvetoryAllToDb(System.Data.IDataReader reader)
        {
            // TODO: Add private.
            InventoryOnHandModel inv = new InventoryOnHandModel();

            inv = new InventoryOnHandModel();
            inv.ID = (int)reader["ID"];
            inv.NameOf = (string)reader["nameOf"];
            inv.Total = (int)reader["total"];
            inv.Completion = (DateTime)reader["completion"];
            inv.Expiration = (DateTime)reader["expire"];
            inv.BatchId = (int)reader["batchId"];

            return inv;
        }

        public InventoryOnHandModelByType MapTotalOnHandInvetoryAllByTypeToDb(System.Data.IDataReader reader)
        {
            // TODO.
            InventoryOnHandModelByType inv = new InventoryOnHandModelByType();

            inv = new InventoryOnHandModelByType();
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
