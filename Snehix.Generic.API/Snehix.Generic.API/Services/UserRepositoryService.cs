using MySql.Data.MySqlClient;
using Snehix.Generic.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Snehix.Generic.API.Services
{
    public class UserRepositoryService
    {
        MySqlConnection _connection = null;
        public UserRepositoryService(string conString)
        {
            _connection = new MySqlConnection(conString);
        }

        public async Task CreateUser(UserModel model)
        {
            try
            {
                await _connection.OpenAsync();
                var cmd = new MySqlCommand("Create_User", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("usrName", model.Username);
                cmd.Parameters.AddWithValue("pssWord", model.Password);
                cmd.Parameters.AddWithValue("frstName", model.FirstName);
                cmd.Parameters.AddWithValue("mdlName", model.MiddleName);
                cmd.Parameters.AddWithValue("lstName", model.LastName);
                cmd.Parameters.AddWithValue("ftrName", model.FatherName);
                cmd.Parameters.AddWithValue("mtrName", model.LastName);
               
                if (model.GuardianId.HasValue)
                    cmd.Parameters.AddWithValue("grdId", model.GuardianId);
                else
                    cmd.Parameters.AddWithValue("grdId",DBNull.Value);
                cmd.Parameters.AddWithValue("usrtypeId", model.UserTypeId);
                cmd.Parameters.AddWithValue("dob", model.DateOfBirth);
                cmd.Parameters.AddWithValue("usrStatusId", model.UserStatusId);
                cmd.Parameters.AddWithValue("crtBy", model.Actor);
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw;
            }
            finally
            {
                //await _connection.col
            }
        }

        public async Task UpdateUser(UserModel model, int ID)
        {
            try
            {
                await _connection.OpenAsync();
                var cmd = new MySqlCommand("Update_User", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("usrId", ID);                
                cmd.Parameters.AddWithValue("frstName", model.FirstName);
                cmd.Parameters.AddWithValue("mdlName", model.MiddleName);
                cmd.Parameters.AddWithValue("lstName", model.LastName);
                cmd.Parameters.AddWithValue("ftrName", model.FatherName);
                cmd.Parameters.AddWithValue("mtrName", model.LastName);
                cmd.Parameters.AddWithValue("grdId", model.GuardianId);
                cmd.Parameters.AddWithValue("usrtypeId", model.UserTypeId);
                cmd.Parameters.AddWithValue("dob", model.DateOfBirth);
                cmd.Parameters.AddWithValue("usrStatusId", model.UserStatusId);
                cmd.Parameters.AddWithValue("modBy", model.Actor);
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

        public async Task<DataTable> GetAllUser()
        {
            DataTable dt = new DataTable();
            await _connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("Get_AllUser", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {

                    sda.Fill(dt);
                }
            }

            return dt;
        }

        public async Task<DataTable> GetUseryById(int userId)
        {
            DataTable dt = new DataTable();
            await _connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("Get_UserById", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("usrId", userId);
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            return dt;
        }

        public async Task<DataTable> GetUseryByUserName(string username)
        {
            DataTable dt = new DataTable();
            await _connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("Get_UserByUserName", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("usrName", username);
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            return dt;
        }
    }
}
