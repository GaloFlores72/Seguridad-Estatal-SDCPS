using CapaDatosRBS;
using CapaModeloRBS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaVigilanciaBasadaEnRiesgos.Controllers
{
    public class TipoProveedorServicioController : Controller
    {
        // GET: TipoProveedorServicio
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerTipoProveedorServicio()
        {
            List<tbTipoProveedorServicio> listas = CD_TipoProveedorServicio.Instancia.ObtenerTipoProveedorServicio();

            return Json(new { data = listas }, JsonRequestBehavior.AllowGet);
        }

    }
}