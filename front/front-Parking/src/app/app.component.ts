import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { VehiculoListComponent } from './vehiculo/components/vehiculo-list/vehiculo-list.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    CommonModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'front-Parking';
}
