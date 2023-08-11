using AHM.Logistic.Smart.DataAccess.Repositories;
using AHM.Logistic.Smart.Entities.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess.TestRepositories
{
    public class CampaignRepositoryTest
    {
        public tbCampaign Find(Expression<Func<tbCampaign, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.tbCampaign.Where(expression).FirstOrDefault();
        }
        public IEnumerable<View_tbCampaign_List> List()
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.View_tbCampaign_List.ToList();
        }
        public IEnumerable<View_tbCampaignDetails_List> ListCampaignDetails()
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.View_tbCampaignDetails_List.ToList();
        }

        public View_tbCampaign_List CampaignDetails(Expression<Func<View_tbCampaign_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.View_tbCampaign_List.Where(expression).FirstOrDefault();
        }

        public List<View_tbCampaignDetails_List> ListDetails(Expression<Func<View_tbCampaignDetails_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.View_tbCampaignDetails_List.Where(expression).ToList();
        }

        public int Insert(tbCampaign item)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cam_Nombre", item.cam_Nombre, DbType.String, ParameterDirection.Input);
            parameters.Add("@cam_Descripcion", item.cam_Descripcion, DbType.String, ParameterDirection.Input);
            parameters.Add("@cam_Html", item.cam_Html, DbType.String, ParameterDirection.Input);
            parameters.Add("@cam_IdUserCreate", item.cam_IdUserCreate, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCampaign_INSERT, parameters, commandType: CommandType.StoredProcedure);
        }

        public int SendCampaign(tbCampaignDetails details)
        {
            using var db = new LogisticSmartContext("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            var state = db.tbCampaign.Where(x => x.cam_Id == details.cam_Id).FirstOrDefault();
            state.cam_Status = false;
            db.Entry(state).State = EntityState.Modified;
            db.SaveChanges();

            var campaign = db.View_tbCampaign_List
                   .Where(x => x.cam_Id == details.cam_Id)
                   .Select(x => x.cam_Html)
                   .FirstOrDefault();

            List<View_tbCustomers_List> data = new();
            data = db.View_tbCustomers_List.ToList();
            string fromMail = "oneworldlogisticsps@gmail.com";
            string fromPassword = "xbhznmjrxczistpb";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            MailMessage message = new()
            {
                From = new MailAddress(fromMail),
                Subject = "One World Logistic le invita a su nueva promocion",
                Body = campaign,
                IsBodyHtml = true
            };

            foreach (var item in data)
            {
                var modelo = new tbCampaignDetails();
                if (item.cus_receive_email == true)
                {
                    message.To.Add(new MailAddress(item.cus_Email));
                    smtpClient.Send(message);
                    modelo.cus_Id = item.cus_Id;
                    modelo.cam_Id = details.cam_Id;
                    message.To.Clear();
                    db.tbCampaignDetails.Add(modelo);
                    db.SaveChanges();
                }

            }
            return 1;
        }

        public int Delete(int Id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@cam_Id", Id, DbType.Int32, ParameterDirection.Input);
            using var db = new SqlConnection("data source=AHM\\SQLEXPRESS; initial catalog=LOGISTIC_SMART_AHM; user id=jireh0223; password=12345");
            return db.ExecuteScalar<int>(ScriptsDataBase.UDP_tbCampaign_DELETE, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
