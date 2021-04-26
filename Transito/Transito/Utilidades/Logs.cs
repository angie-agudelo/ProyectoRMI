using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace Transito.Utilidades
{
    public class Logs
    {
        public static void GrabarLog(string OrigenError, string MensajeUsuario, bool Informativo = false)
        {
            // Se recorta la cadena si la descripcion es superior a 3200 ya que el sistema no puede almacenar en el Event Viewer cadenas superiores a 3200
            if (!string.IsNullOrEmpty(MensajeUsuario) && MensajeUsuario.Length > 3200)
                MensajeUsuario = MensajeUsuario.Substring(0, 3200);

            string nombreLog = "TransitoPoli";
            try
            {
                EventLogEntryType entryType = EventLogEntryType.Warning;
                if (Informativo)
                {
                    entryType = EventLogEntryType.Information;
                }
                if (!string.IsNullOrEmpty(MensajeUsuario))
                {
                    if (!EventLog.Exists(nombreLog))
                    {
                        EventLog.CreateEventSource(nombreLog, nombreLog);
                    }
                    EventLog log = new EventLog(nombreLog);
                    log.Source = OrigenError;
                    log.WriteEntry(MensajeUsuario, entryType, 2);
                    log.Close();
                    log.Dispose();
                }
            }
            catch (Exception ex)
            {
                string LogException = "No se logro escribir el log en el Event Viwer";
                LogException += "Debido a: " + ex.Message;
                LogException += "Error principal: " + MensajeUsuario;
                WriteToTextFile(LogException);
            }
        }
        public static void GrabarLog(string OrigenError, Exception ExOriginal, string MensajeUsuario = "")
        {
            // Se recorta la cadena si la descripcion es superior a 3200 ya que el sistema no puede almacenar en el Event Viewer cadenas superiores a 3200
            string Descripcion = MensajeUsuario;
            Descripcion += ObtenerMensajeTecnico(ExOriginal);

            if (!string.IsNullOrEmpty(Descripcion) && Descripcion.Length > 3200)
                Descripcion = Descripcion.Substring(0, 3200);

            string nombreLog = "TransitoPoli";
            try
            {
                EventLogEntryType entryType = EventLogEntryType.Error;
                if (!string.IsNullOrEmpty(Descripcion))
                {
                    if (!EventLog.Exists(nombreLog))
                    {
                        EventLog.CreateEventSource(nombreLog, nombreLog);
                    }
                    EventLog log = new EventLog(nombreLog);
                    log.Source = OrigenError;
                    log.WriteEntry(Descripcion, entryType, 1);
                    log.Close();
                    log.Dispose();
                }
            }
            catch (Exception ex)
            {
                string LogException = "No se logro escribir el log en el Event Viwer";
                LogException += "Debido a: " + ex.Message;
                LogException += "Error principal: " + Descripcion;
                WriteToTextFile(LogException);
            }
        }

        private static void WriteToTextFile(string textLog)
        {
            FileStream objFS = null;

            string DirectoryPath = AppDomain.CurrentDomain.BaseDirectory + @"LogTransito\";
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            string strFilePath = DirectoryPath + DateTime.Now.ToString("dd-MM-yyyy_") + "Error.log";
            if (!File.Exists(strFilePath))
            {
                objFS = new FileStream(strFilePath, FileMode.Create);
            }
            else
                objFS = new FileStream(strFilePath, FileMode.Append);

            using (StreamWriter Sr = new StreamWriter(objFS))
            {
                Sr.WriteLine(DateTime.Now.ToShortTimeString() + "---" + textLog);
            }
        }

        private static string ObtenerMensajeTecnico(Exception ex)
        {
            string Descripcion = string.Empty;
            if (ex != null)
            {
                Descripcion += "\n\n Mensaje técnico: \n" + ex.Message;
                if (ex.InnerException != null)
                {
                    Descripcion += "\n InnerException: " + ex.InnerException.Message;
                }
                if (ex.TargetSite != null)
                {
                    Descripcion += "\n TargetSite: " + ex.TargetSite.Name;
                }
                if (ex.StackTrace != null)
                {
                    Descripcion += "\n StackTrace: " + ex.StackTrace;
                }
            }
            return Descripcion;
        }
    }
}