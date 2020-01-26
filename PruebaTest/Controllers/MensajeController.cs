using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaTest.Models;

namespace PruebaTest.Controllers
{
    public class MensajeController : Controller
    {
        public void listarUsuarios()
        {
            List<SelectListItem> listar;
            using(var bd=new BDPruebaTecnicaEntities())
            {
                listar = (from item in bd.Usuarios
                          where item.Habilitado == 1
                          select new SelectListItem
                          {
                              Text = item.Nombre,
                              Value = item.IdUsuario.ToString()
                          }).ToList();

                listar.Insert(0, new SelectListItem { Text = " - Seleccionar -", Value = "" });
                ViewBag.listarUsuarios = listar;
            }
        }
        // GET: Mensaje
        public ActionResult Index()
        {
            listarUsuarios();
            List<MensajeCLS> lista = null;
            using(var bd=new BDPruebaTecnicaEntities())
            {
                lista = (from tablaM in bd.Mensajes
                         //se realiza un join para traer la info de la otra tabla (tablaUsuario)
                         join tablaUsuario in bd.Usuarios
                         on tablaM.IdUsuario equals tablaUsuario.IdUsuario
                         where tablaM.Habilitado == 1
                         select new MensajeCLS
                         {
                             idMensaje = tablaM.IdMensaje,
                             mensaje = tablaM.Mensaje,
                             fechaPublicacion = (DateTime)tablaM.Fecha,
                             nombreU = tablaUsuario.Nombre
                         }).ToList();
            }
            return View(lista);
        }

        public ActionResult Agregar()
        {
            listarUsuarios();
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(MensajeCLS oMensajeCls)
        {
            if (!ModelState.IsValid)
            {
                listarUsuarios();
                return View(oMensajeCls);
            }
            else
            {
                using (var bd = new BDPruebaTecnicaEntities())
                {
                    Mensajes oMensaje = new Mensajes();
                    oMensaje.Mensaje = oMensajeCls.mensaje;
                    oMensaje.Fecha = oMensajeCls.fechaPublicacion;
                    oMensaje.IdUsuario = oMensajeCls.idUsuarioM;
                    oMensaje.Habilitado = 1;
                    bd.Mensajes.Add(oMensaje);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            listarUsuarios();
            MensajeCLS oMensajeCls = new MensajeCLS();
            using(var bd = new BDPruebaTecnicaEntities())
            {
                Mensajes oMensaje = bd.Mensajes.Where(vr => vr.IdMensaje.Equals(id)).First();
                oMensajeCls.idMensaje = oMensaje.IdMensaje;
                oMensajeCls.mensaje = oMensaje.Mensaje;
                oMensajeCls.fechaPublicacion = (DateTime)oMensaje.Fecha;
                oMensajeCls.idUsuarioM = (int)oMensaje.IdUsuario;
            }

            return View(oMensajeCls);
        }

        [HttpPost]
        public ActionResult Editar(MensajeCLS oMensajeCls)
        {
            if (!ModelState.IsValid)
            {
                listarUsuarios();
                return View(oMensajeCls);
            }
            else
            {
                listarUsuarios();
                int idMen = oMensajeCls.idMensaje;
                using(var bd=new BDPruebaTecnicaEntities())
                {
                    Mensajes oMensaje = bd.Mensajes.Where(vr => vr.IdMensaje.Equals(idMen)).First();
                    oMensaje.Mensaje = oMensajeCls.mensaje;
                    oMensaje.Fecha = oMensajeCls.fechaPublicacion;
                    oMensaje.IdUsuario = oMensajeCls.idUsuarioM;
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int idMensaje)
        {
            using (var bd=new BDPruebaTecnicaEntities())
            {
                Mensajes oMensaje = bd.Mensajes.Where(vr => vr.IdMensaje.Equals(idMensaje)).First();
                oMensaje.Habilitado = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}