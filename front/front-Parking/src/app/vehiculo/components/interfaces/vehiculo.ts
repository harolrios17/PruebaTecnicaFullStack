export interface Vehiculo{
id: number;
tipovehiculo : string;
marcaVehiculo : string;
placa : string;
horaIngreso : Date;
horaSalida? : Date;
valorTotal? : number;
descuento? : boolean;
numFactura? : string;
}
