namespace WebApiParking.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public required string Tipovehiculo { get; set; }
        public required string MarcaVehiculo { get; set; }
        public required string Placa {  get; set; } 
        public DateTime HoraIngreso { get; set; }
        public DateTime? HoraSalida { get; set; }
        public int? ValorTotal { get; set; }
        public bool? Descuento {  get; set; }    
        public string? NumFactura { get; set; }

        public Vehiculo() { }
        public Vehiculo(string tipoVehiculo, string placa, string marcaVehiculo)
        {
            Tipovehiculo = tipoVehiculo;
            Placa = placa;
            MarcaVehiculo = marcaVehiculo;
        }
    }
}
