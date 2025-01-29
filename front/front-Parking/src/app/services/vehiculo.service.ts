import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { map, Observable } from 'rxjs';
import { Vehiculo } from '../vehiculo/components/interfaces/vehiculo';

@Injectable({
  providedIn: 'root'
})
export class VehiculoService {

  private http = inject(HttpClient);
  private apiUrl = environment.apiUrl;

  getVehiculos(): Observable<{ message: string; response: Vehiculo[] }> {
    return this.http.get<{ message: string; response: Vehiculo[] }>(`${this.apiUrl}ListarVehiculo`);
  }

  getVehiculosByPlaca(placa: string): Observable<Vehiculo> {
    return this.http.get<{ message: string; response: Vehiculo }>(`${this.apiUrl}BuscarPorPlaca/${placa}`)
      .pipe(
        map(response => response.response)
      );
  }

  createVehiculo(vehiculo: Partial<Vehiculo>) : Observable<Vehiculo>{
    return this.http.post<Vehiculo>(`${this.apiUrl}AddVehiculo`,vehiculo);
  }

  updateVehiculo(id : number, vehiculo: Partial<Vehiculo>) : Observable<Vehiculo>{
    return this.http.put<Vehiculo>(`${this.apiUrl}EditVehiculo/${id}`,vehiculo);
  }

  deleteVehiculo(id : number) : Observable<Vehiculo[]>{
    return this.http.delete<Vehiculo[]>(`${this.apiUrl}DeleteIdVehiculo/${id}`);
  }

}
