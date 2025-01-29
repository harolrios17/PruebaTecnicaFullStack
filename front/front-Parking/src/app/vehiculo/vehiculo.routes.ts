import { Routes } from "@angular/router";
import { VehiculoListComponent } from "./components/vehiculo-list/vehiculo-list.component";
import { VehiculoFormComponent } from "./components/vehiculo-form/vehiculo-form.component";

export const VEHICULO_ROUTES: Routes =[
  {path: '', component: VehiculoListComponent},
  {path: 'new', component: VehiculoFormComponent},
  {path: 'edit/:plc', component: VehiculoFormComponent}

]
