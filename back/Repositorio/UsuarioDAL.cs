using back.Modelo;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace back.Repositorio
{
    public class UsuarioDAL
    {
        private readonly string _connectionString;

        public UsuarioDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Usuarios GetUsuario(string username, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = new SqlCommand("SELECT id, usuario, nombre FROM Usuarios WHERE usuario = @usuario AND clave = @clave", connection);
                command.Parameters.AddWithValue("@usuario", username);
                command.Parameters.AddWithValue("@clave", password);

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Usuarios
                    {
                        Id = (int)reader["id"],
                        Usuario = (string)reader["usuario"],
                        Nombre = (string)reader["nombre"]
                    };
                }

                return null;
            }
        }
    }
}
