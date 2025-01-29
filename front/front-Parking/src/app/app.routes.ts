import { Routes } from '@angular/router';
import { VEHICULO_ROUTES } from './vehiculo/vehiculo.routes';

export const APP_ROUTES: Routes = [
  {path:'', redirectTo:'/vehiculo', pathMatch:'full'},
  {path:'vehiculo', children: VEHICULO_ROUTES}
];
