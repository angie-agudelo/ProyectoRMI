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
    public class PropietariosController : Controller
    {
        private TransitoModel db = new TransitoModel();

        // GET: Propietarios
        public ActionResult Index()
        {
            try
            {
                return View(db.Propietarios.ToList());
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error listando. Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Propietarios", Enums.TipoDeMensaje.Error, ex);
                return View(new List<Propietarios>());
            }
        }

        // GET: Propietarios/Create
        public ActionResult Create()
        {
            SelectList TipoIdentificacion = new SelectList(
            new List<SelectListItem>
            {
                            new SelectListItem { Selected = true, Text = "Seleccione..", Value = ""},
                            new SelectListItem { Selected = false, Text = "CC", Value = "1"},
                            new SelectListItem { Selected = false, Text = "TI", Value = "2"},
                            new SelectListItem { Selected = false, Text = "PA", Value = "3"},
                            new SelectListItem { Selected = false, Text = "CE", Value = "4"},
            }, "Value", "Text");

            ViewBag.tipo_Id = TipoIdentificacion;
            return View();
        }

        // POST: Propietarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,tipo_Id,nombre,direccion")] Propietarios propietarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Propietarios PropietariosExist = db.Propietarios.Find(propietarios.id);
                    if (PropietariosExist == null)
                    {
                        db.Propietarios.Add(propietarios);
                        db.SaveChanges();
                        TempData["UserMessage"] = MensajeUsr.GetMessage("Se creo el propietario " + propietarios.id);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["UserMessage"] = MensajeUsr.GetMessage("Ya exíste un Propietario con la identificación " + propietarios.id, "Propietarios", Enums.TipoDeMensaje.Advertencia);
                    }
                }
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error al crear. Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Propietarios", Enums.TipoDeMensaje.Error, ex);
            }
            SelectList TipoIdentificacion = new SelectList(
           new List<SelectListItem>
           {
                            new SelectListItem { Selected = false, Text = "Seleccione..", Value = ""},
                            new SelectListItem { Selected = false, Text = "CC", Value = "1"},
                            new SelectListItem { Selected = false, Text = "TI", Value = "2"},
                            new SelectListItem { Selected = false, Text = "PA", Value = "3"},
                            new SelectListItem { Selected = false, Text = "CE", Value = "4"},           }, "Value", "Text", propietarios.tipo_Id);
            ViewBag.tipo_Id = TipoIdentificacion;
            return View(propietarios);
        }

        // GET: Propietarios/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Propietarios propietarios = db.Propietarios.Find(id);
                if (propietarios == null)
                {
                    return HttpNotFound();
                }
                SelectList TipoIdentificacion = new SelectList(
               new List<SelectListItem>
               {
                            new SelectListItem { Selected = false, Text = "Seleccione..", Value = ""},
                            new SelectListItem { Selected = false, Text = "CC", Value = "1"},
                            new SelectListItem { Selected = false, Text = "TI", Value = "2"},
                            new SelectListItem { Selected = false, Text = "PA", Value = "3"},
                            new SelectListItem { Selected = false, Text = "CE", Value = "4"},               }, "Value", "Text", propietarios.tipo_Id);
                ViewBag.tipo_Id = TipoIdentificacion;
                return View(propietarios);
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error al mostrando la vista para modificar. Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Propietarios", Enums.TipoDeMensaje.Error, ex);
                return RedirectToAction("Index");
            }
        }

        // POST: Propietarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,tipo_Id,nombre,direccion")] Propietarios propietarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(propietarios).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["UserMessage"] = MensajeUsr.GetMessage("Se modifico el propietario " + propietarios.id);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                string MensajeUsuario = "Ocurrio un error al modificar el propietario " + propietarios.id + ". Revise el log de errores.";
                TempData["UserMessage"] = MensajeUsr.GetMessage(MensajeUsuario, "Propietarios", Enums.TipoDeMensaje.Error, ex);
            }
            SelectList TipoIdentificacion = new SelectList(
           new List<SelectListItem>
           {
                            new SelectListItem { Selected = false, Text = "Seleccione..", Value = ""},
                            new SelectListItem { Selected = false, Text = "CC", Value = "1"},
                            new SelectListItem { Selected = false, Text = "TI", Value = "2"},
                            new SelectListItem { Selected = false, Text = "PA", Value = "3"},
                            new SelectListItem { Selected = false, Text = "CE", Value = "4"},           }, "Value", "Text", propietarios.tipo_Id);
            ViewBag.tipo_Id = TipoIdentificacion;
            return View(propietarios);
        }

    }
}
