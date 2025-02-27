import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TareasInterface } from './tareas.interface';
import { Observable, catchError, map, throwError } from 'rxjs';
import { environment } from '../environment/environment';

@Injectable({
  providedIn: 'root'
})
export class ConexionService {
  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) { }

  InsertTask(tarea: TareasInterface): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}`, tarea, { responseType: 'text' as 'json' }).pipe(
      map(response => response),
      catchError(err => {
        return throwError(() => new Error(err.message));
      })
    );
  }

  GetTask(): Observable<TareasInterface[]>{
    return this.http.get<TareasInterface[]>(`${this.baseUrl}`).pipe(
      map(response => response),
      catchError(err => {
        return throwError(() => new Error('Error al cargar lista de tareas' + err));
      })
    )
  } 

  PatchTask(Id: number, Estado: number): Observable<string>{
    return this.http.patch<string>(`${this.baseUrl}/${Id}`, Estado ,{ responseType: 'text' as 'json' }).pipe(
      catchError(err => {
        return throwError(() => new Error('Error al actualizar la tarea' + err));
      })
    )
  }

  DeleteTask(Id: number):Observable<string>{
    return this.http.delete<string>(`${this.baseUrl}/${Id}`,{ responseType: 'text' as 'json' }).pipe(
      catchError(err => {
        return throwError(() => new Error('Error al eliminar la tarea' + err));
      })
    )
  }
}
