import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TareasComponent } from './tareas.component';
import { AgregarTareaComponent } from '../agregar-tarea/agregar-tarea.component';

const routes: Routes = [
  { path: '', component: TareasComponent },
  { path: 'Agregar', component: AgregarTareaComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TareasRoutingModule {}
