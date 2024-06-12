using System.Net.Http.Json;
using Front.Entidades;
using System.Net.Http;
using System.Threading.Tasks;

namespace Front
{
    public class Autorizar
    {
        private readonly HttpClient _httpClient;

        public Autorizar(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Usuario> Login(string username, string password)
        {
            var userData = new { usuario = username, clave = password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7111/api/Login", userData);
                response.EnsureSuccessStatusCode();

                if (response.Content.Headers.ContentLength == 0)
                {
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<Usuario>();
            }
            catch (HttpRequestException httpEx)
            {
                throw new ApplicationException("Error en la solicitud de inicio de sesión.", httpEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en el proceso de inicio de sesión.", ex);
            }
        }
    }
}
