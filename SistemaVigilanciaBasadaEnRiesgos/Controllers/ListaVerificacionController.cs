using CapaDatosRBS;
using CapaModeloRBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaVigilanciaBasadaEnRiesgos.Controllers
{
    public class ListaVerificacionController : Controller
    {
        // GET: ListaVerificacion
        public ActionResult CrearListaVerificacion()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerListaVerificacionTodos()
        {
            List<tbListaDeVerificacion> olista = CD_ListaDeVerificacion.Instancia.ObtenerListas();

            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarListaVerificacion(tbListaDeVerificacion objeto)
        {
            bool respuesta = false;

            if (objeto.ListaID == 0)
            {

                respuesta = CD_ListaDeVerificacion.Instancia.RegistrarListaVerificacion(objeto);
            }
            else
            {
                respuesta = CD_ListaDeVerificacion.Instancia.ModificarListaVerificacion(objeto);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult EliminarListaVerificacion(int id = 0)
        {
            bool respuesta = CD_ListaDeVerificacion.Instancia.EliminarListaVerificacion(id);

            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}