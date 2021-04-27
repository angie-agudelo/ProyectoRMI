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
    public class InfraccionesController : Controller
    {
        private TransitoModel db = new TransitoModel();

        // GET: Infracciones
        public ActionResult Index()
        {
            try
            {
                var infracciones = db.Infracciones.Include(i => i.Vehiculos);
                return View(infracciones.ToList());
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error listando. Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Infracciones", Enums.TipoDeMensaje.Error, ex);
                return View(new List<Infracciones>());
            }
        }
        // GET: Infracciones/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.id_vehiculo = new SelectList(db.Vehiculos, "placa", "id_Propietario");
            }
            catch (Exception ex)
            {
                ViewBag.id_vehiculo = new SelectList(new List<Vehiculos>(), "placa", "id_Propietario");
                string MensajeUsuario = "Ocurrio un error mostrando la vista de creación. Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Infracciones", Enums.TipoDeMensaje.Error, ex);
            }
            return View();
        }

        // POST: Infracciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,id_vehiculo,fecha,accionador,observaciones")] Infracciones infracciones)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Infracciones.Add(infracciones);
                    db.SaveChanges();
                    TempData["UserMessage"] = MensajeUsr.GetMessage("Se creo la infracción " + infracciones.id);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error al crear. Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Infracciones", Enums.TipoDeMensaje.Error, ex);
            }
            ViewBag.id_vehiculo = new SelectList(db.Vehiculos, "placa", "id_Propietario", infracciones.id_vehiculo);
            return View(infracciones);
        }

        // GET: Infracciones/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Infracciones infracciones = db.Infracciones.Find(id);
                if (infracciones == null)
                {
                    return HttpNotFound();
                }
                ViewBag.id_vehiculo = new SelectList(db.Vehiculos, "placa", "id_Propietario", infracciones.id_vehiculo);
                List<SelectListItem> Adicionadores =
                new List<SelectListItem>
                {
                new SelectListItem {  Text = "Agente de transito", Value = "1"},
                new SelectListItem {  Text = "Camara", Value = "2"},
                };
                ViewBag.accionador = new SelectList(Adicionadores, "Value", "Text", infracciones.accionador.ToString());
                return View(infracciones);
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error al mostrando la vista para modificar. Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Infracciones", Enums.TipoDeMensaje.Error, ex);
                return RedirectToAction("Index");
            }
        }

        // POST: Infracciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,id_vehiculo,fecha,accionador,observaciones")] Infracciones infracciones)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(infracciones).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["UserMessage"] = MensajeUsr.GetMessage("Se modifico la infracción " + infracciones.id);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error al modificar la infraccion " + infracciones.id + ". Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Infracciones", Enums.TipoDeMensaje.Error, ex);
            }
            ViewBag.id_vehiculo = new SelectList(db.Vehiculos, "placa", "id_Propietario", infracciones.id_vehiculo);
            SelectList Adicionadores = new SelectList(
            new List<SelectListItem>
            {
                new SelectListItem { Selected = false, Text = "Seleccione", Value = ""},
                new SelectListItem { Selected = false, Text = "Agente de transito", Value = "1"},
                new SelectListItem { Selected = false, Text = "Camara", Value = "2"},
            }, "Value", "Text", infracciones.accionador.ToString());

            ViewBag.accionador = Adicionadores;
            return View(infracciones);
        }

    }
}
