using Microsoft.Data.SqlClient;
using System.Data;
using WebApiParking.Connection;
using WebApiParking.Interface;
using WebApiParking.Models;

namespace WebApiParking.Datos
{
    public class DatosVehiculo : ICrud<Vehiculo>
    {
        ConnectionBd conn = new ConnectionBd();
        public async Task Insert(Vehiculo data)
        {          

            using (var sql = new SqlConnection(conn.ConnSql()))
            {
                string Insert = "insert into Vehiculos(Tipovehiculo,MarcaVehiculo,Placa,HoraIngreso,Descuento) ";
                Insert += "values(@tipoV,@marcaV,@placaV,getdate(),@desc);";
                using (var cmd = new SqlCommand(Insert, sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@tipoV", data.Tipovehiculo);
                    cmd.Parameters.AddWithValue("@marcaV", data.MarcaVehiculo);
                    cmd.Parameters.AddWithValue("@placaV", data.Placa);
                    cmd.Parameters.AddWithValue("@desc", false);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task Update(Vehiculo data, int IdVeh)
        {
            using (var sql = new SqlConnection(conn.ConnSql()))
            {
                string Update = "UPDATE Vehiculos ";
                Update += "SET HoraSalida=@horaS, ";
                Update += "ValorTotal=@valorT, ";
                Update += "Descuento=@desc, ";
                Update += "NumFactura=@numF ";
                Update += "WHERE Id=@id AND ";
                Update += "Tipovehiculo=@tipovehiculo AND ";
                Update += "MarcaVehiculo=@marcavehiculo AND ";
                Update += "Placa=@placa";

                using (var cmd = new SqlCommand(Update, sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", IdVeh);
                    cmd.Parameters.AddWithValue("@horaS", data.HoraSalida);
                    cmd.Parameters.AddWithValue("@valorT", data.ValorTotal);
                    cmd.Parameters.AddWithValue("@desc", data.Descuento);
                    cmd.Parameters.AddWithValue("@numF", data.NumFactura);
                    cmd.Parameters.AddWithValue("@tipovehiculo", data.Tipovehiculo);
                    cmd.Parameters.AddWithValue("@marcavehiculo", data.MarcaVehiculo);
                    cmd.Parameters.AddWithValue("@placa", data.Placa);

                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task Delete(int IdVeh)
        {
            using (var sql = new SqlConnection(conn.ConnSql()))
            {
                string Delete = "delete from Vehiculos where Id=@id";
                using (var cmd = new SqlCommand(Delete, sql))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", IdVeh);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
