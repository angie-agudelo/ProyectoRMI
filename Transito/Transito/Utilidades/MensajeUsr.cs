using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Transito.Utilidades.Enums;

namespace Transito.Utilidades
{
    public class MensajeUsr
    {
        public string NombreClaseCss { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public static string GetMessage(string MensajeUsuario, string Origen = "", TipoDeMensaje TypeMes = TipoDeMensaje.Exito, Exception ex = null)
        {
            MensajeUsr msg = null;
            try
            {
                switch (TypeMes)
                {
                    case TipoDeMensaje.Error:
                        msg = new MensajeUsr() { NombreClaseCss = "alert-danger", Titulo = "Error!", Mensaje = MensajeUsuario };
                        Logs.GrabarLog(Origen, ex, MensajeUsuario);
                        break;
                    case TipoDeMensaje.Advertencia:
                        msg = new MensajeUsr() { NombreClaseCss = "alert-warning", Titulo = "Advertencia!", Mensaje = MensajeUsuario };
                        Logs.GrabarLog(Origen, MensajeUsuario);
                        break;
                    case TipoDeMensaje.Informacion:
                        msg = new MensajeUsr() { NombreClaseCss = "alert-info", Titulo = "Importante!", Mensaje = MensajeUsuario };
                        break;
                    case TipoDeMensaje.Exito:
                    default:
                        msg = new MensajeUsr() { NombreClaseCss = "alert-success", Titulo = "Éxito!", Mensaje = MensajeUsuario };
                        break;
                }
            }
            catch (Exception exx)
            {
                msg = new MensajeUsr() { NombreClaseCss = "alert-danger", Titulo = "Error!", Mensaje = MensajeUsuario + " No se escribio el log original por: " + exx.Message };
            }
            return JsonConvert.SerializeObject(msg);
        }

    }
}