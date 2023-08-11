using AHM.Logistic.Smart.DataAccess.Repositories;
using AHM.Logistic.Smart.Entities.Entities;
using Newtonsoft.Json;
using Serilog;
using System;

namespace AHM.Logistic.Smart.BusinessLogic.Logger
{
    public class EventLogger
    {
        /// <summary>
        /// El ID del usuario del sistema que realiza el evento.
        /// </summary>
        public static int? UserId { get; set; } = null;

        /// <summary>
        /// La dirección IP del usuario que realiza el evento.
        /// </summary>
        public static string IPAddress { get; set; }

        /// <summary>
        /// El user agent del explorador del usuario que realiza el evento.
        /// </summary>
        public static string UserAgent { get; set; }

        /// <summary>
        /// Hace un registro de evento personalizado utilizando parametros.
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="details"></param>
        /// <param name="previousState"></param>
        /// <param name="newState"></param>

        public static void Write(
            EventLogType eventType = EventLogType.None,
            string details = null,
            object previousState = null,
            object newState = null)
        {
            try
            {
                if (UserId < 1)
                    UserId = null;

                //TODO: Check reference loop, and serialization of nested objects
                var serializeSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CustomJsonResolver(),
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    PreserveReferencesHandling = PreserveReferencesHandling.None
                };

                var eventLog = new tbEventLog
                {
                    event_IpAddress = IPAddress,
                    event_UserAgent = UserAgent,
                    event_Details = details,
                    event_Eventtype = (int)eventType,
                    event_PreviousState = previousState != null ? JsonConvert.SerializeObject(previousState, serializeSettings) : null,
                    event_NewState = newState != null ? JsonConvert.SerializeObject(newState, serializeSettings) : null,
                    event_User = UserId
                };

                var repository = new LlEventLog();
                repository.Insert(eventLog);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(Utilities.GetMessage(ex));
            }
        }

        /// <summary>
        /// Hace un registro de evento para creación de objetos.
        /// </summary>
        /// <param name="details"></param>
        /// <param name="newState"></param>
        public static void Create(string details, object newState = null)
        {
            Write(EventLogType.Create, details, null, newState);
        }

        /// <summary>
        /// Hace un registro de evento para actualización de objetos.
        /// </summary>
        /// <param name="details"></param>
        /// <param name="previousState"></param>
        /// <param name="newState"></param>
        public static void Update(string details, object previousState = null, object newState = null)
        {
            Write(EventLogType.Update, details, previousState, newState);
        }

        /// <summary>
        /// Hace un registro de evento para eliminación de objetos.
        /// </summary>
        /// <param name="details"></param>
        /// <param name="previousState"></param>
        public static void Delete(string details, object previousState = null)
        {
            Write(EventLogType.Delete, details, previousState, null);
        }

        /// <summary>
        /// Reinicia las propiedades de un envento de log, para evitar duplicidad.
        /// </summary>
        public static void ResetFields()
        {
            UserId = null;
        }

        public void InfoUser(string ip, string useragent)
        {
            UserAgent = useragent;
            IPAddress = ip;
        }
    }
}
