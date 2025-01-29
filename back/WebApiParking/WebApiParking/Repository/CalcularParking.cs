using WebApiParking.Models;

namespace WebApiParking.Repository
{
    public class CalcularParking
    {

        private const int TarifaCarro = 110;
        private const int TarifaMoto = 50;
        private const int TarifaBicicleta = 10;
        private const double DescuentoPorcentaje = 0.30;

        public Vehiculo CalcularTarifa(Vehiculo vehiculo)
        {
            if (vehiculo.HoraIngreso.Equals(null) || !vehiculo.HoraSalida.HasValue)
            {
                throw new ArgumentException("El vehículo debe tener una hora de ingreso y una hora de salida válidas.");
            }

            // Calcular el tiempo total en minutos
            var tiempoEnMinutos = (vehiculo.HoraSalida.GetValueOrDefault() - vehiculo.HoraIngreso).TotalMinutes;

            // Determinar la tarifa por minuto según el tipo de vehículo
            int tarifaPorMinuto = vehiculo.Tipovehiculo.ToLower() switch
            {
                "carro" => TarifaCarro,
                "moto" => TarifaMoto,
                "bicicleta" => TarifaBicicleta,
                _ => throw new ArgumentException("Tipo de vehículo no válido.")
            };

            // Calcular el valor total sin descuento
            int valorTotalSinDescuento = (int)(tiempoEnMinutos * tarifaPorMinuto);

            // Aplicar descuento si corresponde
            int valorFinal = valorTotalSinDescuento;
            if (vehiculo.Descuento.HasValue && vehiculo.Descuento.Value && !string.IsNullOrEmpty(vehiculo.NumFactura))
            {
                valorFinal = (int)(valorTotalSinDescuento * (1 - DescuentoPorcentaje));
            }

            // Asignar el valor calculado al objeto Vehiculo
            vehiculo.ValorTotal = valorFinal;

            return vehiculo;
        }
    }
}
