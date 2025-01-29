namespace WebApiParking.Repository
{
    public class AddVehiculo
    {
        public string TipoVehiculo { get; set; }

        public string MarcaVehiculo { get; set; }

        public string PlacaVehiculo { get; set; } = null!;
        public int? ValorTotal { get; set; }  
        public bool? Descuento { get; set; }  
        public string? NumFactura { get; set; }  


    }
}
