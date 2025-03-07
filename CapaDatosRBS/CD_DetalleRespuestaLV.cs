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
   public class CD_DetalleRespuestaLV
    {
        public static CD_DetalleRespuestaLV _instancia = null;

        private CD_DetalleRespuestaLV()
        {

        }

        public static CD_DetalleRespuestaLV Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_DetalleRespuestaLV();
                }
                return _instancia;
            }
        }

        public int RegistrarDetalleRespuesta(tbDetalleRespuestaLV oDetalleRespuesta)
        {
            bool respuesta = false;
            int idDetalleRespuesta =0;
            using (SqlConnection oConexion = new SqlConnection(ConexionSqlServer.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_RegistrarDetalleRespuesta", oConexion);
                    cmd.Parameters.AddWithValue("RespuestaID", oDetalleRespuesta.RespuestaID);
                    cmd.Parameters.AddWithValue("PreguntaID", oDetalleRespuesta.PreguntaID);
                    cmd.Parameters.AddWithValue("Estado", oDetalleRespuesta.Estado);
                    cmd.Parameters.AddWithValue("Comentario", oDetalleRespuesta.Comentario);
                    cmd.Parameters.AddWithValue("SubtituloID", oDetalleRespuesta.SubtituloID);
                    cmd.Parameters.AddWithValue("NombreSubtitulo", oDetalleRespuesta.NombreSubtitulo);
                    cmd.Parameters.AddWithValue("DescripcionPregunta", oDetalleRespuesta.DescripcionPregunta);
                    cmd.Parameters.AddWithValue("ReferenciaPregunta", oDetalleRespuesta.ReferenciaPregunta);
                    cmd.Parameters.AddWithValue("UsuarioCrea", oDetalleRespuesta.UsuarioCrea);                    
                    cmd.Parameters.Add("DetalleRespuestaID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    respuesta = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    idDetalleRespuesta = Convert.ToInt32(cmd.Parameters["DetalleRespuestaID"].Value);

                }
                catch (Exception ex)
                {
                    idDetalleRespuesta = 0;
                }
            }
            return idDetalleRespuesta;
        }

    }
}
