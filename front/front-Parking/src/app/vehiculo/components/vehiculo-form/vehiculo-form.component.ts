import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VehiculoService } from '../../../services/vehiculo.service';

// Angular Material
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

@Component({
  selector: 'app-vehiculo-form',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatButtonModule,
    MatCardModule,
    MatSlideToggleModule,
    CommonModule,
  ],
  templateUrl: './vehiculo-form.component.html',
  styleUrl: './vehiculo-form.component.css',
})
export class VehiculoFormComponent {
  private vehiculoService = inject(VehiculoService);
  private fb = inject(FormBuilder);
  public router = inject(Router);
  private route = inject(ActivatedRoute);
  private snackBar = inject(MatSnackBar);

  vehiculoForm: FormGroup;
  isEditMode: boolean = false;

  options = [
    { value: 'carro', viewValue: 'Carro' },
    { value: 'motocicleta', viewValue: 'Motocicleta' },
    { value: 'bicicleta', viewValue: 'Bicicleta' },
  ];

  constructor() {
    this.vehiculoForm = this.fb.group({
      id: [''],
      tipovehiculo: ['', [Validators.required]],
      marcaVehiculo: ['', [Validators.required]],
      placaVehiculo: ['', [Validators.required]],
      valorTotal: [0],
      descuento: [false],
      numFactura: [{ value: '', disabled: true }],
    });

    this.route.params.subscribe((params) => {
      if (params['plc']) {
        this.isEditMode = true;
        this.loadVehiculo(params['plc']);
      }
    });

    this.onDescuentoChange();
  }

  private loadVehiculo(placa: string): void {
    this.vehiculoService.getVehiculosByPlaca(placa).subscribe({
      next: (data) => {
        const transformedData = {
          ...data,
          placaVehiculo: data.placa
        };
        this.vehiculoForm.patchValue(transformedData);
        console.log(this.vehiculoForm);

        if (data) {
          this.vehiculoForm.patchValue(data);
          if (data.descuento) {
            this.vehiculoForm.get('numFactura')?.enable();
          }
        }
      },
      error: (err) => console.error(err),
    });
  }

  private onDescuentoChange(): void {
    this.vehiculoForm.get('descuento')?.valueChanges.subscribe((value: boolean) => {
      if (value) {
        this.vehiculoForm.get('numFactura')?.enable();
        this.vehiculoForm.get('numFactura')?.setValidators([Validators.required]);
      } else {
        this.vehiculoForm.get('numFactura')?.disable();
        this.vehiculoForm.get('numFactura')?.clearValidators();
      }
      this.vehiculoForm.get('numFactura')?.updateValueAndValidity();
    });
  }

  onSubmit(): void {
    if (this.vehiculoForm.invalid) return;


    const vehiculoData = { ...this.vehiculoForm.value };
    vehiculoData.valorTotal = 0; // Asegurar que siempre se envíe 0
    var vId = vehiculoData.id;
    delete vehiculoData.id;

    if (this.isEditMode) {
      this.vehiculoService.updateVehiculo(vId, vehiculoData).subscribe({
        next: () => {
          this.showSnackBar('Registro actualizado exitosamente');
          this.router.navigate(['/']);
        },
        error: (err) => console.error(err),
      });
    } else {
      delete vehiculoData.id;
      this.vehiculoService.createVehiculo(vehiculoData).subscribe({
        next: () => {
          this.showSnackBar('Registro creado exitosamente');
          this.router.navigate(['/']);
        },
        error: (err) => console.error(err),
      });
    }
  }

  showSnackBar(message: string): void {
    this.snackBar.open(message, 'Cerrar', { duration: 2000 });
  }
}
