using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase, IVehiculoController
    {
        private IVehiculoFlujo _vehiculoFlujo;
        private ILogger<VehiculoController> logger;

        public VehiculoController(IVehiculoFlujo vehiculoFlujo, ILogger<VehiculoController> logger)
        {
            _vehiculoFlujo = vehiculoFlujo;
            this.logger = logger;
        }
        #region Operaciones
        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] VehiculoRequest vehiculo)
        {
            var resultado = await _vehiculoFlujo.Agregar(vehiculo);
            return CreatedAtAction(nameof(Obtener),new {Id = resultado},null);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar([FromRoute] Guid ID, [FromBody] VehiculoRequest vehiculo)
        {
            if(!await VerificarVehiculoExiste(ID))
                return NotFound($"No se encontró un vehículo con el ID {ID}");
            var resultado = await _vehiculoFlujo.Editar(ID, vehiculo);
            return Ok(resultado);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar([FromRoute]Guid id)
        {
            if (!await VerificarVehiculoExiste(id))
                return NotFound($"No se encontró un vehículo con el ID {id}");
            var resultado = await _vehiculoFlujo.Eliminar(id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _vehiculoFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener([FromRoute]Guid id)
        {
            var resultado = await _vehiculoFlujo.Obtener(id);
            return Ok(resultado);
        }
        #endregion Operaciones
        #region Helpers
        private async Task<bool> VerificarVehiculoExiste(Guid ID)
        {
            var resultadoValidacion = false;
            var resultadoVehiculoExiste = await _vehiculoFlujo.Obtener(ID);
            if (resultadoVehiculoExiste != null)
                resultadoValidacion = true;
            return resultadoValidacion;
        }
        #endregion Helpers
    }
}
