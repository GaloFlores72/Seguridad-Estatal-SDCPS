using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaDatosRBS;
using CapaModeloRBS;

namespace SistemaVigilanciaBasadaEnRiesgos.Controllers
{
    public class PlanificacionController : Controller
    {
        private static tbUsuario SesionUsuario;

        // GET: SeguridadOperacional
        public ActionResult ListasVerificacion()
        {
            List<tbListaDeVerificacion> listaVerificacion = new List<tbListaDeVerificacion>();
            listaVerificacion = CD_ListaDeVerificacion.Instancia.ObtenerListas();
            return View(listaVerificacion);
        }


        public ActionResult Crear()
        {
            SesionUsuario = (tbUsuario)Session["Usuario"];
            List<tbRespuestaLV> olistaRespuesta = new List<tbRespuestaLV>();


            olistaRespuesta = CD_RespuestaLV.Instancia.ObtenerRespuestaCabeceraTodos();
            ViewBag.ListaTipoServicio = SelectTipoServicio();
            ViewBag.ListaSelectOrganizacion = ToSelectListOrganizaciones();
            ViewBag.ListaUsuarios = ToSelectListUsuarios();

            return View(olistaRespuesta);
        }


        [HttpPost]
        public JsonResult GuardarEncabezadoRespuesta(tbRespuestaLV objeto)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            try
            {
                objeto.UsuarioCrea = SesionUsuario.CodigoUsuario;
                if (objeto.RespuestaID == 0)
                {
                    objeto.oListaDeVerificacion = CD_ListaDeVerificacion.Instancia.ObtenerListaVerificacionPorOidXml(objeto.ListaID);
                    if (objeto.oListaDeVerificacion != null)
                    {
                        objeto.NombreLista = objeto.oListaDeVerificacion.Nombre;
                        objeto.DescripcionLista = objeto.oListaDeVerificacion.Descripcion;
                        respuesta = CD_RespuestaLV.Instancia.GrabarRespuestaCabcera(objeto);
                    }
                    else
                    {
                        respuesta = false;
                        mensaje = "No puede grabar ya que no tiene datos de la Lista de Verificación";
                    }
                }
                else
                {
                    respuesta = true; // CD_Cliente.Instancia.ModificarCliente(objeto);
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }



            return Json(new { resultado = respuesta, mensajeError = mensaje }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// hay que quita
        /// </summary>
        /// <param name="listaID"></param>
        /// <returns></returns>
        public ActionResult ListaRespuestas(int listaID)
        {
            List<tbRespuestaLV> listaRespuestas = new List<tbRespuestaLV>();
            tbListaDeVerificacion olistaVerificacion = new tbListaDeVerificacion();
            olistaVerificacion = CD_ListaDeVerificacion.Instancia.ObtenerListaVerificacionPorOid(listaID);
            ViewBag.idLista = olistaVerificacion.ListaID;
            ViewBag.ListaVerificacion = olistaVerificacion.Nombre + " " + olistaVerificacion.Descripcion;
            return View(listaRespuestas);
        }

        public ActionResult Formulario(int idResp, int idListaV)
        {
            tbRespuesta oRespuesta = new tbRespuesta();
            try
            {
                oRespuesta = CD_Respuesta.Instancia.ObtenerRespuestaPorId(idResp);
                foreach (var item in oRespuesta.oInpectores)
                {
                    oRespuesta.NombreInpectores = item.Nombres + " " + item.Apellidos;
                    if (oRespuesta.oInpectores.Count() > 1)
                    {
                        oRespuesta.NombreInpectores = oRespuesta.NombreInpectores  + " - " +   item.Nombres + " " + item.Apellidos;
                    }
                }



                ViewBag.ListaSelectOrganizacion = ToSelectListOrganizaciones();
                ViewBag.ListaUsuarios = ToSelectListUsuarios();

            }
            catch (Exception ex)
            {
                oRespuesta = null;
                throw;
            }


            return View(oRespuesta);
        }

        public JsonResult CambiaOrientacionEstado(int respuestaId, int orientacionId, int estadoId, string comentario)
        {
            bool resupesta = false;

            List<ReporteProducto> lista = CD_Reportes.Instancia.ReporteProductoTienda(idtienda, codigoproducto);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public SelectList ToSelectListOrganizaciones()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var listValores = CD_Organizacion.Instancia.ObtenerOrganizaciones();
            foreach (var item in listValores)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.Nombre.Trim(),
                    Value = item.OrganizacionID.ToString()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "---SELECCIONAR..."
            };
            seleccion.Selected = true;
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        public SelectList SelectTipoServicio()
        {

            List<SelectListItem> list = new List<SelectListItem>();
            var listValores = CD_TipoProveedorServicio.Instancia.ObtenerTipoProveedorServicio();
            foreach (var item in listValores)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.DescripcionTipoProveedor.Trim(),
                    Value = item.IdTipoProveedor.ToString()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "---SELECCIONAR..."
            };
            seleccion.Selected = true;
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        [HttpGet]
        public JsonResult ObtieneTipoProveedorServicioPorOid(int id = 0)
        {
            tbTipoProveedorServicio otipoprovser = new tbTipoProveedorServicio();
            otipoprovser = CD_TipoProveedorServicio.Instancia.ObtenerTipoProveedorServicioPorId(id);

            return Json(otipoprovser, JsonRequestBehavior.AllowGet);
        }

        public SelectList ToSelectListUsuarios()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var listValores = CD_Usuario.Instancia.ObtenerUsuarios();
            foreach (var item in listValores)
            {
                list.Add(new SelectListItem()
                {
                    Text = item.Nombres.Trim(),
                    Value = item.IdUsuario.ToString()
                });
            }
            var seleccion = new SelectListItem()
            {
                Value = "0",
                Text = "---SELECCIONAR..."
            };
            seleccion.Selected = true;
            list.Insert(0, seleccion);

            return new SelectList(list, "Value", "Text");
        }

        [HttpGet]
        public JsonResult ObtieneOrganizacionPorOid(int id = 0)
        {
            tbOrganizacion organizacion = new tbOrganizacion();
            organizacion = CD_Organizacion.Instancia.ObtenerOrganizacion(id);

            return Json(organizacion, JsonRequestBehavior.AllowGet);
        }
    }
}