using AHM.Logistic.Smart.Entities.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.DataAccess.Repositories
{
    public class SecurityRepository
    {
        public IEnumerable<View_Screens_List> ListScreens()
        {
            using var db = new LogisticSmartContext();
            return db.View_Screens_List.ToList();
        }

        public List<View_UserPermits_SELECT> ListGetPermissions(Expression<Func<View_UserPermits_SELECT, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.View_UserPermits_SELECT.Where(expression).ToList();
        }
        public tbPersons Find(Expression<Func<tbPersons, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.tbPersons.Where(expression).FirstOrDefault();
        }
        public View_tbPersons_List Find2(Expression<Func<View_tbPersons_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            return db.View_tbPersons_List.Where(expression).FirstOrDefault();
        }
        public int IdByEmail(string Random, tbUsers items, Expression<Func<View_tbPersons_List, bool>> expression = null)
        {
            using var db = new LogisticSmartContext();
            var Perso = db.View_tbPersons_List.Where(expression).FirstOrDefault();
            var User = db.tbUsers.Where(x => x.Per_Id == Perso.per_Id).FirstOrDefault();
  
            User.usu_Password = items.usu_Password;
            User.usu_PasswordSalt = items.usu_PasswordSalt;
            User.usu_Temporal_Password = true;
            db.SaveChanges();
            string plantilla = $"<html xmlns='http://www.w3.org/1999/xhtml' xmlns:o='urn:schemas-microsoft-com:office:office' style='width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0'> <head> <meta charset='UTF-8'> <meta content='width=device-width, initial-scale=1' name='viewport'> <meta name='x-apple-disable-message-reformatting'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <meta content='telephone=no' name='format-detection'> <link href='https://fonts.googleapis.com/css?family=Lato:400,400i,700,700i' rel='stylesheet'> </head> <body style='width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;font-family:lato, helvetica, arial, sans-serif;padding:0;Margin:0'> <div class='es-wrapper-color' style='background-color:#F4F4F4'> <table class='es-wrapper' width='100%' cellspacing='0' cellpadding='0' style='border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top'> <tr class='gmail-fix' height='0' style='border-collapse:collapse'> <td style='padding:0;Margin:0'> <table cellspacing='0' cellpadding='0' border='0' align='center' style='border-collapse:collapse;border-spacing:0px;width:600px'> <tr style='border-collapse:collapse'> <td cellpadding='0' cellspacing='0' border='0' style='padding:0;Margin:0;line-height:1px;min-width:600px' height='0'></td> </tr> </table></td> </tr> <tr style='border-collapse:collapse'> <td valign='top' style='padding:0;Margin:0'> <table cellpadding='0' cellspacing='0' class='es-content' align='center' style='border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%'> <tr style='border-collapse:collapse'> <td align='center' style='padding:0;Margin:0'> <table class='es-content-body' style='border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px' cellspacing='0' cellpadding='0' align='center'> <tr style='border-collapse:collapse'> <td align='left' style='Margin:0;padding-left:10px;padding-right:10px;padding-top:15px;padding-bottom:15px'> <table class='es-left' cellspacing='0' cellpadding='0' align='left' style='border-collapse:collapse;border-spacing:0px;float:left'> <tr style='border-collapse:collapse'> <td align='left' style='padding:0;Margin:0;width:282px'> <a href='https://imgur.com/L1buDm0'><img src='https://i.imgur.com/L1buDm0.png' title='source: imgur.com' height='50' width='150'/></a> <table width='100%' cellspacing='0' cellpadding='0' role='presentation' style='border-collapse:collapse;border-spacing:0px'> <tr style='border-collapse:collapse'> <td class='es-infoblock es-m-txt-c' align='left' style='padding:0;Margin:0;line-height:14px;font-size:12px;color:#CCCCCC'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;font-family:arial, helvetica, sans-serif;line-height:14px;color:#CCCCCC;font-size:12px'><br></p></td> </tr> </table></td> </tr> </table> <table class='es-right' cellspacing='0' cellpadding='0' align='right' style='border-collapse:collapse;border-spacing:0px;float:right'> <tr style='border-collapse:collapse'> <td align='left' style='padding:0;Margin:0;width:278px'> <table width='100%' cellspacing='0' cellpadding='0' role='presentation' style='border-collapse:collapse;border-spacing:0px'> <tr style='border-collapse:collapse'> <td align='right' class='es-infoblock es-m-txt-c' style='padding:0;Margin:0;line-height:14px;font-size:12px;color:#CCCCCC'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;font-family:lato, helvetica, arial, sans-serif;line-height:14px;color:#CCCCCC;font-size:12px'><a href='https://viewstripo.email' class='view' target='_blank' style='-webkit-text-size-adjust:none;-ms-text-size-adjust:none;text-decoration:underline;color:#CCCCCC;font-size:12px;font-family:arial, 'helvetica neue', helvetica, sans-serif'></a></p></td> </tr> </table></td> </tr> </table></td> </tr> </table></td> </tr> </table> <table class='es-header' cellspacing='0' cellpadding='0' align='center' style='border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:#001f52;background-repeat:repeat;background-position:center top'> <tr style='border-collapse:collapse'> <td style='padding:0;Margin:0;background-color:#F4F4F4' bgcolor='#7c72dc' align='center'> <table class='es-header-body' cellspacing='0' cellpadding='0' align='center' style='border-collapse:collapse;border-spacing:0px;background-color:#001f52;width:600px'> <tr style='border-collapse:collapse'> <td align='left' style='Margin:0;padding-bottom:10px;padding-left:10px;padding-right:10px;padding-top:20px'> <table width='100%' cellspacing='0' cellpadding='0' style='border-collapse:collapse;border-spacing:0px'> <tr style='border-collapse:collapse'> <td valign='top' align='center' style='padding:0;Margin:0;width:580px'> <table width='100%' cellspacing='0' cellpadding='0' role='presentation' style='border-collapse:collapse;border-spacing:0px'> <tr style='border-collapse:collapse'> <td align='center' style='Margin:0;padding-left:10px;padding-right:10px;padding-top:25px;padding-bottom:25px;font-size:0'><a href='https://imgur.com/ISsj5HM'><img src='https://i.imgur.com/ISsj5HM.png' title='source: imgur.com' /></a></td> </tr> </table></td> </tr> </table></td> </tr> </table></td> </tr> </table> <table class='es-content' cellspacing='0' cellpadding='0' align='center' style='border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%'> <tr style='border-collapse:collapse'> <td style='padding:0;Margin:0;background-color:#F4F4F4' bgcolor='#7c72dc' align='center'> <table class='es-content-body' style='border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px' cellspacing='0' cellpadding='0' align='center'> <tr style='border-collapse:collapse'> <td align='left' style='padding:0;Margin:0'> <table width='100%' cellspacing='0' cellpadding='0' style='border-collapse:collapse;border-spacing:0px'> <tr style='border-collapse:collapse'> <td valign='top' align='center' style='padding:0;Margin:0;width:600px'> <table style='border-collapse:separate;border-spacing:0px;background-color:#ffffff;border-radius:4px' width='100%' cellspacing='0' cellpadding='0' bgcolor='#ffffff' role='presentation'> <tr style='border-collapse:collapse'> <td align='center' style='padding-bottom:5px;padding-left:30px;padding-right:30px;padding-top:35px'><h1 style='Margin:0;line-height:58px;font-family:lato, helvetica, arial, sans-serif;font-size:48px;font-style:normal;font-weight:normal;color:#111111'>¿Olvidaste tu clave?</h1></td> </tr> <tr style='border-collapse:collapse'> <td bgcolor='#ffffff' align='center' style='padding-top:5px;padding-bottom:5px;padding-left:20px;padding-right:20px;font-size:0'> <table width='100%' height='100%' cellspacing='0' cellpadding='0' border='0' role='presentation' style='border-collapse:collapse;border-spacing:0px'> <tr style='border-collapse:collapse'> <td style='border-bottom:1px solid #ffffff;background:#FFFFFF;height:1px;width:100%;margin:0px'></td> </tr> </table></td> </tr> </table></td> </tr> </table></td> </tr> </table></td> </tr> </table> <table class='es-content' cellspacing='0' cellpadding='0' align='center' style='border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%'> <tr style='border-collapse:collapse'> <td align='center' style='padding:0;Margin:0'> <table class='es-content-body' style='border-collapse:collapse;border-spacing:0px;background-color:#ffffff;width:600px' cellspacing='0' cellpadding='0' bgcolor='#ffffff' align='center'> <tr style='border-collapse:collapse'> <td align='left' style='padding:0;Margin:0'> <table width='100%' cellspacing='0' cellpadding='0' style='border-collapse:collapse;border-spacing:0px'> <tr style='border-collapse:collapse'> <td valign='top' align='center' style='padding:0;Margin:0;width:600px'> <table style='border-collapse:collapse;border-spacing:0px;background-color:#ffffaa' width='100%' cellspacing='0' cellpadding='0' bgcolor='#ffffff' role='presentation'> <tr style='border-collapse:collapse'> <td class='es-m-txt-l' bgcolor='#ffffff' align='left' style='Margin:0;padding-bottom:15px;padding-top:20px;padding-left:30px;padding-right:30px'><p style='Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;font-family:lato, helvetica, arial, sans-serif;line-height:27px;color:#666666;font-size:18px'>{Perso.per_Firstname} {Perso.per_LastNames} ingresa esta clave temporal para poder acceder a tu cuenta y reestablecer tu clave.</p></td> </tr> </table></td> </tr> </table></td> </tr> <tr style='border-collapse:collapse'> <td align='left' style='padding:0;Margin:0;padding-bottom:20px;padding-left:30px;padding-right:30px'> <table width='100%' cellspacing='0' cellpadding='0' style='border-collapse:collapse;border-spacing:0px'> <tr style='border-collapse:collapse'> <td valign='top' align='center' style='padding:0;Margin:0;width:540px'> <table width='100%' cellspacing='0' cellpadding='0' role='presentation' style='border-collapse:collapse;border-spacing:0px'> <tr style='border-collapse:collapse'> <td align='center' style='Margin:0;padding-left:10px;padding-right:10px;padding-top:40px;padding-bottom:40px'><span class='es-button-border' style='border-style:solid;border-color:#001f52;background:#001f52;border-width:1px;display:inline-block;border-radius:2px;width:auto'><a class='' target='_blank' style='text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;color:#FFFFFF;font-size:20px;border-style:solid;border-color:#001f52;border-width:15px 25px 15px 25px;display:inline-block;background:#001f52;border-radius:2px;font-family:helvetica,arial, verdana, sans-serif;font-weight:normal;font-style:normal;line-height:24px;width:auto;text-align:center'>{Random}</a></span></td> </tr> </table></td> </tr> </table></td> </tr> </table></td> </tr> </table> </tr> </table> </div> </body> </html>";
            string fromMail = "oneworldlogisticsps@gmail.com";
            string fromPassword = "xbhznmjrxczistpb";
            string cuerpo = plantilla;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            MailMessage message = new()
            {
                From = new MailAddress(fromMail),
                Subject = "OWL Correo de recuperacion de contraseña",
                Body = cuerpo,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(Perso.per_Email));
            smtpClient.Send(message);
            message.To.Clear();

            return 1;
        }
    
    }
}
