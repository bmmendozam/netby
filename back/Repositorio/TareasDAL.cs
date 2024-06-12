using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace back
{
    public class TareasDAL
    {
        private readonly string _connectionString;

        public TareasDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<TareaPendiente>> GetTareasAsync()
        {
            var tareas = new List<TareaPendiente>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("SELECT * FROM TareasPendientes", connection);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var tarea = new TareaPendiente
                            {
                                Id = (int)reader["Id"],
                                Titulo = (string)reader["Titulo"],
                                Descripcion = (string)reader["Descripcion"],
                                Fecha_Creacion = (DateTime)reader["Fecha_Creacion"],
                                Fecha_Vencimiento = (DateTime)reader["Fecha_Vencimiento"],
                                Completada = (bool)reader["Completada"]
                            };
                            tareas.Add(tarea);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

                
                return null;
            }

            return tareas;
        }

        public async Task<TareaPendiente> GetTareaByIdAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("SELECT * FROM TareasPendientes WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new TareaPendiente
                            {
                                Id = (int)reader["Id"],
                                Titulo = (string)reader["Titulo"],
                                Descripcion = (string)reader["Descripcion"],
                                Fecha_Creacion = (DateTime)reader["Fecha_Creacion"],
                                Fecha_Vencimiento = (DateTime)reader["Fecha_Vencimiento"],
                                Completada = (bool)reader["Completada"]
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                return null;
            }

            return null;
        }

        public async Task<int> Insertar(TareaPendiente tarea)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("INSERT INTO TareasPendientes (Titulo, Descripcion, Fecha_Creacion, Fecha_Vencimiento, Completada) VALUES (@Titulo, @Descripcion, @Fecha_Creacion, @Fecha_Vencimiento, @Completada); SELECT CAST(SCOPE_IDENTITY() AS INT)", connection);
                    command.Parameters.AddWithValue("@Titulo", tarea.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", tarea.Descripcion);
                    command.Parameters.AddWithValue("@Fecha_Creacion", tarea.Fecha_Creacion);
                    command.Parameters.AddWithValue("@Fecha_Vencimiento", tarea.Fecha_Vencimiento);
                    command.Parameters.AddWithValue("@Completada", tarea.Completada);
                    return (int)await command.ExecuteScalarAsync();
                }
            }
            catch (SqlException ex)
            {
                 
                return 1;
            }
        }

        public async Task<bool> Update(TareaPendiente tarea)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("UPDATE TareasPendientes SET Titulo = @Titulo, Descripcion = @Descripcion, Fecha_Vencimiento = @Fecha_Vencimiento, Completada = @Completada WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Titulo", tarea.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", tarea.Descripcion);
                    command.Parameters.AddWithValue("@Fecha_Vencimiento", tarea.Fecha_Vencimiento);
                    command.Parameters.AddWithValue("@Completada", tarea.Completada);
                    command.Parameters.AddWithValue("@Id", tarea.Id);
                    int filasAfectadas = await command.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (SqlException ex)
            {
                 
                return false;
            }
        }

        public async Task<bool> Delete (int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    var command = new SqlCommand("DELETE FROM TareasPendientes WHERE Id = @Id", connection);
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (SqlException ex)
            {
                
                return false;
            }
        }
    }
}