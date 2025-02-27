import { ChangeDetectionStrategy, ChangeDetectorRef, Component } from '@angular/core';
import { TareasInterface } from '../tareas.interface';
import { ConexionService } from '../conexion.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { take } from 'rxjs';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-tareas',
  imports: [CommonModule, FormsModule],
  standalone: true,
  templateUrl: './tareas.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush 
})
export class TareasComponent {
  ListaTareas: TareasInterface[] = [];
  ListaTareasFiltro: TareasInterface[] = [];
  mensaje: string = "";
  error: number = 0;
  seleccion:  number = 0;
  constructor(private conexionService: ConexionService, private router: Router, private DetectarCambio: ChangeDetectorRef) {}

  GetTask():void{
    this.conexionService.GetTask().pipe(take(1)).subscribe({
      next: (data) =>{
        this.ListaTareas = data;
        this.ListaTareasFiltro = this.ListaTareas;
        this.DetectarCambio.markForCheck();
      },
      error: (err) =>{
        this.mensaje = "No se pudo cargar el listado de tareas";
      }
    })
  }

  CompletarTarea(Id: number):void{
    if(confirm("Seguro de finalizar esta tarea?")){
      this.conexionService.PatchTask(Id, 1).subscribe({
        next: (data) => {
          this.mensaje =data;
          this.GetTask();
        },
        error: (err) =>{
          this.mensaje = "No se pudo completar la tarea";
        }
      })
    }
  }

  IniciarTarea(Id: number):void{
    if(confirm("Seguro de iniciar esta tarea?")){
      this.conexionService.PatchTask(Id, 0).subscribe({
        next: (data) => {
          this.mensaje =data;       
          this.GetTask();
        },
        error: (err) =>{
          this.mensaje = "No se pudo completar la tarea";
        }
      })
    }
  }

  EliminarTarea(Id:number):void{
    if(confirm("Seguro de eliminar esta tarea?")){
      this.conexionService.DeleteTask(Id).subscribe({
        next: (data) => {
          this.mensaje =data;
          this.GetTask();
        },
        error: (err) =>{
          this.mensaje = "No se pudo completar la tarea";
        }
      })
    }
  }

  AgregarTarea(): void{
    this.router.navigate(['/tareas/Agregar']);
  }

  Filtra(): void{
    if(this.seleccion == 1){
      this.ListaTareasFiltro = this.ListaTareas.filter(t => t.completada == true);
    }else if(this.seleccion == 2){
      this.ListaTareasFiltro = this.ListaTareas.filter(t => t.completada == false);
    }else{
      this.ListaTareasFiltro = this.ListaTareas;
    }
  }

  ngOnInit(){
    this.GetTask();
  }
}

