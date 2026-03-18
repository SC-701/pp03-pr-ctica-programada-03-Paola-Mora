using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Registro;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Text.Json;
using IHttpClientFactory = System.Net.Http.IHttpClientFactory;

namespace Servicios
{
    public class RegistroServicio : IRegistroServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClient;

        public RegistroServicio(IConfiguracion configuracion, IHttpClientFactory httpClient)
        {
            _configuracion = configuracion;
            _httpClient = httpClient;
        }

        public async Task<Propietario> Obtener(string placa)
        {
            var endPoint = _configuracion.ObtenerMetodo("ApiEndPointsRegistro","ObtenerRegistro");
            var servicioRegistro = _httpClient.CreateClient("ServicioRegistro");
            var respuesta = await  servicioRegistro.GetAsync(string.Format(endPoint,placa));
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoDeserializado = JsonSerializer.Deserialize<List<Propietario>>(resultado, opciones);
            return resultadoDeserializado.FirstOrDefault();
        }
    }
}
