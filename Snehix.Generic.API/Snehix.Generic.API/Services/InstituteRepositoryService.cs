﻿using MySql.Data.MySqlClient;
using Snehix.Generic.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Snehix.Generic.API.Services
{
    public class InstituteRepositoryService
    {
        MySqlConnection _connection = null;
        public InstituteRepositoryService(string conString)
        {
            _connection = new MySqlConnection(conString);
        }

        public async Task CreateInstitute(InstitutionModel model)
        {
            try
            {
                await _connection.OpenAsync();
                var cmd = new MySqlCommand("Create_Institute", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("InstituteName", model.Name);
                cmd.Parameters.AddWithValue("InstituteBranch", model.BranchName);
                cmd.Parameters.AddWithValue("InstituteDescription", model.Description);
                cmd.Parameters.AddWithValue("BoardId", model.BoardId);
                cmd.Parameters.AddWithValue("TypeId", model.TypeId);
                cmd.Parameters.AddWithValue("CreatedBy", model.Actor);

                cmd.Parameters.AddWithValue("MailingAddLine1", model.MailingAddress.AddressLine1);
                cmd.Parameters.AddWithValue("MailingAddLine2", model.MailingAddress.AddressLine2);
                cmd.Parameters.AddWithValue("MailingAddLine3", model.MailingAddress.AddressLine3);
                cmd.Parameters.AddWithValue("MailingCity", model.MailingAddress.City);
                cmd.Parameters.AddWithValue("MailingState", model.MailingAddress.State);
                cmd.Parameters.AddWithValue("MailingCountry", model.MailingAddress.Country);
                cmd.Parameters.AddWithValue("MailingZip", model.MailingAddress.Zipcode);

                cmd.Parameters.AddWithValue("BillingAddLine1", model.BillingAddress.AddressLine1);
                cmd.Parameters.AddWithValue("BillingAddLine2", model.BillingAddress.AddressLine2);
                cmd.Parameters.AddWithValue("BillingAddLine3", model.BillingAddress.AddressLine3);
                cmd.Parameters.AddWithValue("BillingCity", model.BillingAddress.City);
                cmd.Parameters.AddWithValue("BillingState", model.BillingAddress.State);
                cmd.Parameters.AddWithValue("BillingCountry", model.BillingAddress.Country);
                cmd.Parameters.AddWithValue("BillingZip", model.BillingAddress.Zipcode);

                cmd.Parameters.AddWithValue("LandNumber", model.ContactDetail.LandLineNumber);
                cmd.Parameters.AddWithValue("AltLandline", model.ContactDetail.AltLandLineNumber);
                cmd.Parameters.AddWithValue("MobNumber", model.ContactDetail.MobileNumber);
                cmd.Parameters.AddWithValue("AltMobNumber", model.ContactDetail.AltMobileNumber);
                cmd.Parameters.AddWithValue("Email", model.ContactDetail.EmailId);
                cmd.Parameters.AddWithValue("AltEmail", model.ContactDetail.AltEmailId);
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

        public async Task UpdateInstitute(InstitutionModel model, int ID)
        {
            try
            {
                await _connection.OpenAsync();
                var cmd = new MySqlCommand("Update_User", _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("instituteId", ID);
                cmd.Parameters.AddWithValue("InstituteName", model.Name);
                cmd.Parameters.AddWithValue("InstituteBranch", model.BranchName);
                cmd.Parameters.AddWithValue("InstituteDescription", model.Description);
                cmd.Parameters.AddWithValue("BoardId", model.BoardId);
                cmd.Parameters.AddWithValue("TypeId", model.TypeId);
                cmd.Parameters.AddWithValue("ModifiedBy", model.Actor);               
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


        public async Task<DataTable> GetAllInstitutes()
        {
            DataTable dt = new DataTable();
            await _connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("Get_Institutes", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {

                    sda.Fill(dt);
                }
            }

            return dt;
        }

        public async Task<DataTable> GetInstituteById(int Id)
        {
            DataTable dt = new DataTable();
            await _connection.OpenAsync();
            using (MySqlCommand cmd = new MySqlCommand("Get_InstitutesById", _connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("InstituteId", Id);
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }

            return dt;
        }
       
    }
}
