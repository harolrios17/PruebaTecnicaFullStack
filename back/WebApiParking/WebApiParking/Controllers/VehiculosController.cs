using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiParking.Datos;
using WebApiParking.Models;
using WebApiParking.Repository;

namespace WebApiParking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly VehiculoContext _context;

        public VehiculosController(VehiculoContext context)
        {
            _context = context;
        }

        // GET: api/Vehiculos
        [HttpGet]
        [Route("ListarVehiculo")]
        public async Task<ActionResult<List<Vehiculo>>> GetVehiculo()
        {
            List<Vehiculo> lista = new List<Vehiculo>();

            try
            {

                lista = await _context.Vehiculos.ToListAsync();
                return StatusCode(StatusCodes.Status200OK,
                        new
                        {
                            message = "Se carga data del vehiculo",
                            Response = lista
                        }
                    );
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                new { message = e.Message });
            }
        }

        // GET: api/Vehiculos/BuscarPorPlaca/{placa}
        [HttpGet]
        [Route("BuscarPorPlaca/{placa}")]
        public async Task<ActionResult> GetVehiculoPorPlaca(string placa)
        {
            try
            {
                // Busca el vehículo por la placa
                var vehiculo = await _context.Vehiculos.FirstOrDefaultAsync(v => v.Placa == placa);

                if (vehiculo == null)
                {
                    return NotFound(new { message = "No se encontró un vehículo con la placa proporcionada." });
                }

                // Devuelve el vehículo encontrado
                return Ok(new
                {
                    message = "Vehículo encontrado exitosamente.",
                    Response = vehiculo
                });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpPost]
        [Route("AddVehiculo")]
        public async Task<ActionResult> AddVehiculo([FromBody] AddVehiculo Obj)
        {
            var function = new DatosVehiculo();
            try
            {
                if (Obj.PlacaVehiculo == string.Empty) return BadRequest("El campo placa no puede estar vacio");

                Vehiculo OVeh = new Vehiculo(Obj.TipoVehiculo, Obj.PlacaVehiculo, Obj.MarcaVehiculo)
                {
                    Tipovehiculo = Obj.TipoVehiculo,
                    Placa = Obj.PlacaVehiculo,
                    MarcaVehiculo = Obj.MarcaVehiculo
                    
                };   
                await function.Insert(OVeh);
                return StatusCode(StatusCodes.Status200OK, new { message = "Datos almacenados correctamente" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = e.Message });
            }
        }

        [HttpPut]
        [Route("EditVehiculo/{IdVeh:int}")]
        public async Task<ActionResult> EditVehiculo(int IdVeh, [FromBody] AddVehiculo Obj)
        {
            try
            {
                // Validar que el campo placa no esté vacío
                if (string.IsNullOrWhiteSpace(Obj.PlacaVehiculo))
                {
                    return BadRequest("El campo placa no puede estar vacío.");
                }

                // Validar si el vehículo existe
                var vehiculoExistente = await _context.Vehiculos.FindAsync(IdVeh);
                if (vehiculoExistente == null)
                {
                    return NotFound(new { message = "No se encontró un vehículo con el ID proporcionado." });
                }

                // Mapear los datos del objeto AddVehiculo a Vehiculo
                vehiculoExistente.Tipovehiculo = Obj.TipoVehiculo;
                vehiculoExistente.MarcaVehiculo = Obj.MarcaVehiculo;
                vehiculoExistente.Placa = Obj.PlacaVehiculo;

                // Asignar datos relacionados con el descuento
                vehiculoExistente.Descuento = Obj.Descuento;
                vehiculoExistente.NumFactura = Obj.NumFactura;

                // Crear la hora de salida para el cálculo
                vehiculoExistente.HoraSalida = DateTime.Now;

                // Calcular la tarifa
                var tarifaParqueadero = new CalcularParking();
                vehiculoExistente = tarifaParqueadero.CalcularTarifa(vehiculoExistente);

                // Actualizar el vehículo en la base de datos
                var datosVehiculo = new DatosVehiculo();
                await datosVehiculo.Update(vehiculoExistente, IdVeh);

                return Ok(new { message = "Los datos del vehículo se actualizaron correctamente.", valorTotal = vehiculoExistente.ValorTotal });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteIdVehiculo/{IdVeh:int}")]
        public async Task<ActionResult> DeleteVehiculo(int IdVeh)
        {
            var function = new DatosVehiculo();
            try
            {
                await function.Delete(IdVeh);
                return StatusCode(StatusCodes.Status200OK, new { message = "Se elimino correctamente el id[" + IdVeh + "]" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { message = e.Message });
            }
        }


    }
}
