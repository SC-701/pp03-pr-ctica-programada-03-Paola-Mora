using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class VehiculoDA : IVehiculoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlconnection;

        public VehiculoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlconnection =_repositorioDapper.ObtenerRepositorio();
        }

        public async Task<Guid> Agregar(VehiculoRequest vehiculo)
        {
            string query = @"AgregarVehiculo";
            var resultadoConsulta = await _sqlconnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                Placa = vehiculo.Placa,
                Color = vehiculo.Color,
                Anio = vehiculo.Anio,
                Precio = vehiculo.Precio,
                CorreoPropietario = vehiculo.CorreoPropietario,
                TelefonoPropietario = vehiculo.TelefonoPropietario,
                IdModelo = vehiculo.IdModelo
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid ID, VehiculoRequest vehiculo)
        {
            string query = @"EditarVehiculo";
            await verificarVehiculoExiste(ID);
            var resultadoConsulta = await _sqlconnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = ID,
                Placa = vehiculo.Placa,
                Color = vehiculo.Color,
                Anio = vehiculo.Anio,
                Precio = vehiculo.Precio,
                CorreoPropietario = vehiculo.CorreoPropietario,
                TelefonoPropietario = vehiculo.TelefonoPropietario,
                IdModelo = vehiculo.IdModelo
            });
            return resultadoConsulta;
        }

        private async Task verificarVehiculoExiste(Guid ID)
        {
            VehiculoResponse? resultadoConsultaVehiculo = await Obtener(ID);
            if (resultadoConsultaVehiculo == null)
            {
                throw new Exception("No se encontró el vehículo");
            }
        }

        public async Task<Guid> Eliminar(Guid id)
        {
            string query = @"EliminarVehiculo";
            var resultadoConsulta = await _sqlconnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<VehiculoResponse>> Obtener()
        {
            string query = @"ObtenerVehiculos";
            var resultadoConsulta = await _sqlconnection.QueryAsync<VehiculoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<VehiculoDetalle> Obtener(Guid id)
        {
            string query = @"ObtenerVehiculos";
            var resultadoConsulta = await _sqlconnection.QueryAsync<VehiculoDetalle>(query, new { Id = id});
            return resultadoConsulta.FirstOrDefault();
        }
    }
}
