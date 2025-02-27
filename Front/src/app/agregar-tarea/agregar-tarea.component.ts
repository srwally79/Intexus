import { ChangeDetectorRef, Component } from '@angular/core';
import { ConexionService } from '../conexion.service';
import { Router } from '@angular/router';
import { TareasInterface } from '../tareas.interface';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-agregar-tarea',
  imports: [CommonModule, FormsModule],
  templateUrl: './agregar-tarea.component.html',
  styleUrl: './agregar-tarea.component.css'
})
export class AgregarTareaComponent {
  mensaje: string = "";
  tarea: TareasInterface = {} as TareasInterface; 
  error: number = 0;
    constructor(private conexionService: ConexionService, private router: Router, private DetectarCambio: ChangeDetectorRef) {}
    AgregarTarea():void{
      if(this.Validate()){
        this.conexionService.InsertTask(this.tarea).subscribe({
          next: (data) =>{
            this.mensaje = data;
            setTimeout(() => { this.router.navigate(['/tareas']) },3000);
          },
          error: (err) =>{
            this.error = 1;
            this.mensaje = "Ocurrio un error al insertar. Valido los datos ingresados";
          }
        })
      }
    }

    Validate():boolean{
      console.log(this.tarea.nombre);
      if(this.tarea.nombre == undefined || this.tarea.nombre == ""){
        this.error=2;
        this.mensaje="El nombre es obligatorio";
        return false;
      }else if(this.tarea.fechaInicio ==  undefined){
        this.error=2;
        this.mensaje="La fecha de inicio es obligatoria";
        return false;
      }else if(this.tarea.fechaFin == undefined){
        this.error=2;
        this.mensaje="La fecha final es obligatoria";
        return false;
      }else if(this.tarea.completada == undefined){
        this.error=2;
        this.mensaje="indicar si la tarea fue completada es obligatorio";
        return false;
      }else if(this.tarea.fechaFin < this.tarea.fechaFin){
        this.error=2;
        this.mensaje='La fecha final no puede ser menor a la fecha inicial';
      }
      return true;
    }
}
