using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Snehix.Generic.API.Services
{
    public class EntityRepositoryService
    {

        MySqlConnection _connection = null;
        public EntityRepositoryService(string conString)
        {
            _connection = new MySqlConnection(conString); 
        }
        public async Task<List<string>> myquery()
        {
            try
            {
                await _connection.OpenAsync();
                var command = new MySqlCommand("SELECT name FROM Entity;", _connection);
                var reader = await command.ExecuteReaderAsync();
                List<string> a = new List<string>();
                while (await reader.ReadAsync())
                {
                    var value = reader.GetValue(0).ToString();
                    a.Add(value);
                    // do something with 'value'
                }
                return a;
            }
            catch
            {
                throw ;
            }
            finally
            {
                //await _connection.col
            }
        }


        public async Task CreateEntity(string name, string description,int entityTypeId)
        {
            try
            {
                await _connection.OpenAsync();
                var cmd = new MySqlCommand("Add_Entity", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("EntityName", name);
                cmd.Parameters.AddWithValue("Description", description);
                cmd.Parameters.AddWithValue("TypeId", entityTypeId);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                //await _connection.col
            }
        }

        public async Task UpdateEntity(int id,string name, string description, int entityTypeId)
        {
            try
            {
                await _connection.OpenAsync();
                var cmd = new MySqlCommand("Update_Entity", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("EntityId", id);
                cmd.Parameters.AddWithValue("EntityName", name);
                cmd.Parameters.AddWithValue("Description", description);
                cmd.Parameters.AddWithValue("TypeId", entityTypeId);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                //await _connection.col
            }
        }

        public async Task CreateEntityType(string name, string description)
        {
            try
            {
                await _connection.OpenAsync();
                var cmd = new MySqlCommand("Add_EntityType", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("EntityTypeName", name);
                cmd.Parameters.AddWithValue("Description", description);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                //await _connection.col
            }
        }

        public async Task<DataTable> GetAllEntityType()
        {
            DataTable dt = new DataTable();
            await _connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("Get_AllEntityType", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                    {
                        
                        sda.Fill(dt);                        
                    }
                }
            
            return dt;
        }

        public async Task<DataTable> GetAllEntityByType(int typeId)
        {
            DataTable dt = new DataTable();
            await _connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("Get_AllEntityByTypeId", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("TypeId", typeId);
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            return dt;
        }

        public async Task<DataTable> GetAllEntityById(int id)
        {
            DataTable dt = new DataTable();
            await _connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("Get_EntityById", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("entityId", id);
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            return dt;
        }
    }
}
