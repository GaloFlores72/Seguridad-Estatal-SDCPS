using CapaModeloRBS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatosRBS
{
   public class CD_Pregunta
    {
        public static CD_Pregunta _instancia = null;

        private CD_Pregunta()
        {

        }

        public static CD_Pregunta Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Pregunta();
                }
                return _instancia;
            }
        }

        public List<tbPregunta> ObtenerPreguntasPorSubtitulo(int subtituloID)
        {
            List<tbPregunta> preguntas = new List<tbPregunta>();

            using (var connection = new SqlConnection(ConexionSqlServer.CN))
            {
                var command = new SqlCommand("SELECT * FROM Preguntas WHERE SubtituloID = @SubtituloID", connection);
                command.Parameters.AddWithValue("@SubtituloID", subtituloID);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        preguntas.Add(new tbPregunta
                        {
                            PreguntaID = reader.GetInt32(reader.GetOrdinal("PreguntaID")),
                            SubtituloID = reader.GetInt32(reader.GetOrdinal("SubtituloID")),
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            Referencia = reader.IsDBNull(reader.GetOrdinal("Referencia")) ? null : reader.GetString(reader.GetOrdinal("Referencia")),
                            Estado = reader.GetString(reader.GetOrdinal("Estado")),
                            Estadisticas = reader.IsDBNull(reader.GetOrdinal("Estadisticas")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("Estadisticas")) 
                        });
                    }
                }
            }

            return preguntas;
        }

    }
}
