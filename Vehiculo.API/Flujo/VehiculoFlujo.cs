using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class VehiculoFlujo : IVehiculoFlujo
    {
        private IVehiculoDA _vehiculoDA;

        public VehiculoFlujo(IVehiculoDA vehiculoDA)
        {
            _vehiculoDA = vehiculoDA;
        }

        public Task<Guid> Agregar(VehiculoRequest vehiculo)
        {
            return _vehiculoDA.Agregar(vehiculo);
        }

        public Task<Guid> Editar(Guid ID, VehiculoRequest vehiculo)
        {
           return _vehiculoDA.Editar(ID, vehiculo);
        }

        public Task<Guid> Eliminar(Guid id)
        {
            return _vehiculoDA.Eliminar(id);    
        }

        public Task<IEnumerable<VehiculoResponse>> Obtener()
        {
            return _vehiculoDA.Obtener();
        }

        public Task<VehiculoDetalle> Obtener(Guid id)
        {
           return _vehiculoDA.Obtener(id);
        }
    }
}
