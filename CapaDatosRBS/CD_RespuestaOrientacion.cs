using CapaModeloRBS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatosRBS
{
   public class CD_RespuestaOrientacion
    {
        public static CD_RespuestaOrientacion _instancia = null;

        private CD_RespuestaOrientacion()
        {

        }

        public static CD_RespuestaOrientacion Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_RespuestaOrientacion();
                }
                return _instancia;
            }
        }

        public bool RegistrarRespuestaOrientacion(tbRespuestaOrientacion oRespuestaOrientacion)
        {
            bool respuesta = false;
            int idRespuestaOrientacion = 0;
            using (SqlConnection oConexion = new SqlConnection(ConexionSqlServer.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarRespuestaOrientacion", oConexion);
                    cmd.Parameters.AddWithValue("DetalleRespuestaID", oRespuestaOrientacion.DetalleRespuestaID);
                    cmd.Parameters.AddWithValue("OrientacionID", oRespuestaOrientacion.OrientacionID);
                    cmd.Parameters.AddWithValue("EstadoImplementacionID", oRespuestaOrientacion.EstadoImplementacionID);
                    cmd.Parameters.AddWithValue("Comentario", oRespuestaOrientacion.Comentario);
                    cmd.Parameters.AddWithValue("UsuarioCreaId", oRespuestaOrientacion.UsuarioCreaId);
                    cmd.Parameters.AddWithValue("CodigoPeligro", oRespuestaOrientacion.CodigoPeligro);
                    cmd.Parameters.AddWithValue("DescripcionOrientacion", oRespuestaOrientacion.DescripcionOrientacion);                  
                    cmd.Parameters.Add("RespuestaOrientacionID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    idRespuestaOrientacion = Convert.ToInt32(cmd.Parameters["RespuestaOrientacionID"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
