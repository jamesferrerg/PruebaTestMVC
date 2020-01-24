using PruebaTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaTest.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            List < UsuarioCLS > listaUsuarios = null;
            try
            {
                using (var bd = new BDPruebaTecnicaEntities())
                {
                    listaUsuarios = (from item in bd.Usuarios
                                     where item.Habilitado == 1
                                     select new UsuarioCLS
                                     {
                                         nombre = item.Nombre,
                                         apellido = item.Apellido,
                                         correo = item.Email,
                                         celular = (long)item.Celular,
                                         telefono = (int)item.Telefono
                                     }).ToList();
                }
        }
            catch (Exception ex)
            {

                return View(listaUsuarios);
    }

            return View(listaUsuarios);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(UsuarioCLS oUsuarioCls)
        {
            if (!ModelState.IsValid)
            {
                return View(oUsuarioCls);
            }
            else
            {
                using(var bd=new BDPruebaTecnicaEntities())
                {
                    Usuarios oUsuario = new Usuarios();
                    oUsuario.Nombre = oUsuarioCls.nombre;
                    oUsuario.Apellido = oUsuarioCls.apellido;
                    oUsuario.Email = oUsuarioCls.correo;
                    oUsuario.Celular = oUsuarioCls.celular;
                    oUsuario.Telefono = oUsuarioCls.telefono;
                    oUsuario.Habilitado = 1;
                    bd.Usuarios.Add(oUsuario);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}