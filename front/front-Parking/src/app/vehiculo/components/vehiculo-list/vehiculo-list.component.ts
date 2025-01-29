import { Component, Inject, inject, OnInit, signal, ViewChild, WritableSignal } from '@angular/core';
import { VehiculoService } from '../../../services/vehiculo.service';
import { Vehiculo } from '../interfaces/vehiculo';
import { CommonModule } from '@angular/common';

//Material
import { MatButtonModule } from '@angular/material/button';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-vehiculo-list',
  standalone: true,
  imports: [CommonModule, MatButtonModule, MatTableModule, MatPaginatorModule],
  templateUrl: './vehiculo-list.component.html',
  styleUrl: './vehiculo-list.component.css'
})
export class VehiculoListComponent implements OnInit{

  private dialog = inject(MatDialog);
  private router = inject(Router);
  private vehiculoService = inject(VehiculoService);
@ViewChild(MatPaginator) paginator!: MatPaginator;

//vehiculos: Vehiculo[] = [];
vehiculos : WritableSignal<Vehiculo[]> = signal<Vehiculo[]>([]);
displayedColumns: string[] = ["id", "tipovehiculo", "marcaVehiculo", "placa", "horaIngreso", "horaSalida", "valorTotal", "descuento", "numFactura", "actions"];
dataSource = new MatTableDataSource<Vehiculo>([]);

ngOnInit(): void {
  this.loadVehiculos();
}

loadVehiculos(): void {
  this.vehiculoService.getVehiculos().subscribe({
    next: (response) => {
      if (response && response.response) {
        this.dataSource.data = response.response;
      } else {
        console.error('La respuesta de la API no contiene la propiedad "response"');
      }
    },
    error: (err) => {
      console.error('Error al cargar vehículos:', err);
    }
  });

}

updateTableData() {
  this.dataSource.data = this.vehiculos();
  this.dataSource.paginator = this.paginator;
}

  editVehiculo(placa? : string){
    const path = placa? `/vehiculo/edit/${placa}` : '/vehiculo/new';
    this.router.navigate([path]);
  }

  deleteVehiculo(id : number){
    const dialogRef = this.dialog.open(ConfirmDialogComponent);
    dialogRef.afterClosed().subscribe((result)=> {
      if (result) {
        this.vehiculoService.deleteVehiculo(id).subscribe(()=> {
          const updatedVehiculo = this.vehiculos().filter((vehiculo)=>vehiculo.id !== id);
          this.vehiculos.set(updatedVehiculo);
          this.updateTableData();
          window.location.reload();
        })
      }
    });
  }
}
