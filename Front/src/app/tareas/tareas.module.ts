import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TareasRoutingModule } from './tareas-routing.module';
import { TareasComponent } from './tareas.component';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AgregarTareaComponent } from '../agregar-tarea/agregar-tarea.component';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    TareasRoutingModule,
    TareasComponent,
    FormsModule, 
    RouterModule,
    AgregarTareaComponent
  ]
})

export class TareasModule { }
