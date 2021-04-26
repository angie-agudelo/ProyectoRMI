using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Transito.Models;
using Transito.Utilidades;

namespace Transito.Controllers
{
    public class VehiculosController : Controller
    {
        private TransitoModel db = new TransitoModel();

        // GET: Vehiculos
        public ActionResult Index()
        {
            try
            {
                var vehiculos = db.Vehiculos.Include(v => v.Propietarios);
                return View(vehiculos.ToList());
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error listando. Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Vehiculos", Enums.TipoDeMensaje.Error, ex);
                return View(new List<Vehiculos>());
            }
        }
        // GET: Vehiculos/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.id_Propietario = new SelectList(db.Propietarios, "id", "nombre");

                SelectList TipoVehiculo = new SelectList(
               new List<SelectListItem>
               {
                   new SelectListItem { Selected = false, Text = "Seleccione..", Value = ""},
                   new SelectListItem { Selected = false, Text = "Automóvil", Value = "Automovil"},
                   new SelectListItem { Selected = false, Text = "Moto", Value = "Moto"},
                   new SelectListItem { Selected = false, Text = "Carro", Value = "Carro"},
               }, "Value", "Text");
                ViewBag.tipo = TipoVehiculo;
            }
            catch (Exception ex)
            {
                ViewBag.id_Propietario = new SelectList(new List<Propietarios>(), "id", "nombre");
                string MensajeUsuario = "Ocurrio un error mostrando la vista de creación. Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Vehiculos", Enums.TipoDeMensaje.Error, ex);
            }
            return View();
        }

        // POST: Vehiculos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "placa,id_Propietario,marca,fecha,tipo")] Vehiculos vehiculos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Vehiculos vehiculoExist = db.Vehiculos.Find(vehiculos.placa);
                    if (vehiculoExist == null)
                    {
                        if(vehiculos.fecha.Date < DateTime.Now.Date)
                        {
                            TempData["UserMessage"] = MensajeUsr.GetMessage("La fecha de matricula no puede ser menor a la actual", "Vehiculos", Enums.TipoDeMensaje.Advertencia);
                        }
                        else
                        {
                            db.Vehiculos.Add(vehiculos);
                            db.SaveChanges();
                            TempData["UserMessage"] = MensajeUsr.GetMessage("Se creo el vehiculo " + vehiculos.placa);
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        TempData["UserMessage"] = MensajeUsr.GetMessage("Ya exíste un vehiculo con la placa " + vehiculos.placa, "Vehiculos", Enums.TipoDeMensaje.Advertencia);
                    }
                }
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error al crear. Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Vehiculos", Enums.TipoDeMensaje.Error, ex);
            }
            ViewBag.id_Propietario = new SelectList(db.Propietarios, "id", "nombre", vehiculos.id_Propietario);

            SelectList TipoVehiculo = new SelectList(
           new List<SelectListItem>
           {
               new SelectListItem { Selected = false, Text = "Seleccione..", Value = ""},
               new SelectListItem { Selected = false, Text = "Automóvil", Value = "Automovil"},
               new SelectListItem { Selected = false, Text = "Moto", Value = "Moto"},
               new SelectListItem { Selected = false, Text = "Carro", Value = "Carro"},
           }, "Value", "Text", vehiculos.tipo);
            ViewBag.tipo = TipoVehiculo;
            return View(vehiculos);
        }

        // GET: Vehiculos/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vehiculos vehiculos = db.Vehiculos.Find(id);
                if (vehiculos == null)
                {
                    return HttpNotFound();
                }
                ViewBag.id_Propietario = new SelectList(db.Propietarios, "id", "nombre", vehiculos.id_Propietario);
                SelectList TipoVehiculo = new SelectList(
               new List<SelectListItem>
               {
               new SelectListItem { Selected = false, Text = "Seleccione..", Value = ""},
               new SelectListItem { Selected = false, Text = "Automóvil", Value = "Automovil"},
               new SelectListItem { Selected = false, Text = "Moto", Value = "Moto"},
               new SelectListItem { Selected = false, Text = "Carro", Value = "Carro"},
               }, "Value", "Text", vehiculos.tipo);
                ViewBag.tipo = TipoVehiculo;
                return View(vehiculos);
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error al mostrando la vista para modificar. Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Vehiculos", Enums.TipoDeMensaje.Error, ex);
                return RedirectToAction("Index");
            }
        }

        // POST: Vehiculos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "placa,id_Propietario,marca,fecha,tipo")] Vehiculos vehiculos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(vehiculos).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["UserMessage"] = MensajeUsr.GetMessage("Se modifico el vehiculo " + vehiculos.placa);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error al modificar el vehiculo " + vehiculos.placa + ". Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Vehiculos", Enums.TipoDeMensaje.Error, ex);
            }
            ViewBag.id_Propietario = new SelectList(db.Propietarios, "id", "nombre", vehiculos.id_Propietario);
            SelectList TipoVehiculo = new SelectList(
           new List<SelectListItem>
           {
               new SelectListItem { Selected = false, Text = "Seleccione..", Value = ""},
               new SelectListItem { Selected = false, Text = "Automóvil", Value = "Automovil"},
               new SelectListItem { Selected = false, Text = "Moto", Value = "Moto"},
               new SelectListItem { Selected = false, Text = "Carro", Value = "Carro"},
           }, "Value", "Text", vehiculos.tipo);
            ViewBag.tipo = TipoVehiculo;
            return View(vehiculos);
        }
    }
}
